using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos) 
        {
            return Tab.peca(pos) == null;
        }


        //Cria uma matriz booleana com falses e trues, uma nova posicao 0,0
        //Ver aquela posicao de tal peca mais os calculos
        //Se a posicao for valida, e se pode mover, retorna a matriz com a posição possivel.
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if(Cor == Cor.Branca) 
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (Tab.posValida(pos) && livre(pos)) { mat[pos.linha, pos.coluna] = true; }

                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (Tab.posValida(pos) && livre(pos) && QteMovimentos == 0) { mat[pos.linha, pos.coluna] = true; }

                pos.definirValores(posicao.linha - 1, posicao.coluna -1);
                if (Tab.posValida(pos) && existeInimigo(pos)) { mat[pos.linha, pos.coluna] = true; }

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (Tab.posValida(pos) && existeInimigo(pos)) { mat[pos.linha, pos.coluna] = true; }


            }
            else
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (Tab.posValida(pos) && livre(pos)) { mat[pos.linha, pos.coluna] = true; }

                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (Tab.posValida(pos) && livre(pos) && QteMovimentos == 0) { mat[pos.linha, pos.coluna] = true; }

                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (Tab.posValida(pos) && existeInimigo(pos)) { mat[pos.linha, pos.coluna] = true; }

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (Tab.posValida(pos) && existeInimigo(pos)) { mat[pos.linha, pos.coluna] = true; }
            }


            return mat;

        }
    }
}
