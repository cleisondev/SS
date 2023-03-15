using System;
using tabuleiro;
using xadrez;

namespace xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }
        public bool terminada { get;private set; }
        public PartidaDeXadrez()
        {
          tab = new Tabuleiro(8,8);
          turno = 1;
          jogadorAtual = Cor.Branca;
          terminada = false;
          colocarPecas();
        }

  

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incremetarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 8).ToPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 7).ToPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('f', 4).ToPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 2).ToPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 2).ToPosicao());
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            turno++;
            mudaJogador();

        }

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

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroExceptions("Posição de destino inválda");
             };
        }
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

