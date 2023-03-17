using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

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
        public PartidaDeXadrez partida;
        public bool xeque { get; private set; }

        public Peca vulneravelEnPassant { get; private set; }
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
          vulneravelEnPassant = null;
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
            //Jogada especial - ROQUE PEQUENO
            if(p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca t = tab.retirarPeca(origemTorre);
                t.incremetarQtdMovimentos();
                tab.colocarPeca(t, destinoTorre); 
            }

            //Jogada especial - ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna -1);
                Peca t = tab.retirarPeca(origemTorre);
                t.incremetarQtdMovimentos();
                tab.colocarPeca(t, destinoTorre);
            }

            //Jogada especial - EN PASSANT
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.Cor == Cor.Branca) 
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else {

                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada  = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
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

            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
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

            //Jogada especial - ROQUE PEQUENO
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca t = tab.retirarPeca(destinoTorre);
                t.decrementarQtdMovimentos();
                tab.colocarPeca(t, origemTorre);
            }
            //Jogada especial - ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca t = tab.retirarPeca(destinoTorre);
                t.decrementarQtdMovimentos();
                tab.colocarPeca(t, origemTorre);
            }

            //EN PASSANT
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna - 1 && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if(p.Cor == Cor.Branca) 
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }

                    tab.colocarPeca(peao, posP);
                }
            }
        }
           


        //Essa função realiza uma jogada, executando o movimento de origem para destino, incrementando o turno e mudando o jogador.
        public void realizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExceptions("Você não pode se colocar em xeque");
            }

            Peca p = tab.peca(destino);

            //#Jogada especial PROMOCAO
            if(p is Peao)
            {
                if((p.Cor == Cor.Branca && destino.linha == 0) || (p.Cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.Cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }


            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque= false;
            }

            if (testeXequeMate(adversaria(jogadorAtual))){
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
           

            //EN PASSANT
            if(p is Peao && (destino.linha == origem.linha -  2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
          

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


        public bool testeXequeMate(Cor cor) 
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }


            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i=0; i< tab.linhas; i++)
                {
                    for(int j = 0; j< tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecacapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecacapturada);
                            if(!testeXeque)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
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
            if (!tab.peca(origem).movimentoPossivel(destino))
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

