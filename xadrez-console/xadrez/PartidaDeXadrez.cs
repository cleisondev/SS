using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;

namespace xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }
        public bool terminada { get;private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public PartidaDeXadrez()
        {
          tab = new Tabuleiro(8,8);
          turno = 1;
          jogadorAtual = Cor.Branca;
          terminada = false;
          pecas = new HashSet<Peca>();
          capturadas = new HashSet<Peca>();
          colocarPecas();
          xeque = false;
        }


  //A funcao coloca no P a funcao de retirar a peca origem, dps incrementa, reitra a peca na posição destino
  //Dps poe a peca p na posicao destino
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incremetarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        //Incrementa no conjunto as pecas capturadas de acordo com a cor
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if (x.Cor == cor) { aux.Add(x);  }
            }
            return aux;
        }

        //Ele mostra as pecas em jogo caso precisa ver quais movimentos possiveis, tirando as pecas já capturadas.
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor) { aux.Add(x); }
            }
            aux.ExceptWith(pecasCapturadas(cor)) ;
            return aux;
        }

        //Essa posicao é a base pra colocar as pecas, recebe um char coluna e linha que vão ser convertidos, e adiciona ao conjunto pecas.
        public void colocarNovaPeca(char  coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        //Essa função auxilia na colocada de pecas.
        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if(pecaCapturada != null) { 
                
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        //Essa função realiza uma jogada, executando o movimento de origem para destino, incrementando o turno e mudando o jogador.
        public void realizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExceptions("Você não pode se colocar em xeque");
            };

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque= false;
            }

            turno++;
            mudaJogador();

        }

        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if(x is Rei) { return x; }
                
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null)
            {
                throw new Exception("Não tem rei da cor no tabuleiro");
            }

            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        //Pra validar algumas jogadas, criando excessões;
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroExceptions("Não existe peça na posição de origem escolhida");

            }
            if(jogadorAtual != tab.peca(pos).Cor) 
            {
                throw new TabuleiroExceptions("Peça de origem escolhida não é sua");
            }
            if(!tab.peca(pos).existeMovimentosPossiveis()) 
            {
                throw new TabuleiroExceptions("Não há movimentos possiveis pra peça escolhida");    
            }
        }

        //Funcao que verifica se origem e destino não é valida e lança esse erro.
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroExceptions("Posição de destino inválda");
             };
        }

        //Essa funcao verifica se o jogador atual for o Branco, ela seta como o preto. Trocando os turnos.
        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        }

    }

