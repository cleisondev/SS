using System.Diagnostics.Tracing;
using tabuleiro;

namespace xadrez
{
     class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(cor,tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        //Retorna true se a posicao for nula ou a cor em questão for diferente da cor do parametro
        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QteMovimentos == 0;
           
        }

        //Cria uma matriz booleana com falses e trues, uma nova posicao 0,0
        //Ver aquela posicao de tal peca mais os calculos
        //Se a posicao for valida, e se pode mover, retorna a matriz com a posição possivel.
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao(0,0);

            //Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if(Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna +1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna -1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Noroeste
            pos.definirValores(posicao.linha -1 , posicao.coluna - 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //ROQUE - PEQUENO
            if(QteMovimentos == 0 && !partida.xeque)
            {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);

                    if (Tab.peca(p1) == null && Tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2]  = true;
                    }
                }

            }

            //ROQUE - GRANDE
            if (QteMovimentos == 0 && !partida.xeque)
            {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }

            }

            return mat;

        }
    }
}
