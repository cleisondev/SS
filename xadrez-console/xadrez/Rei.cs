using tabuleiro;

namespace xadrez
{
     class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(cor,tab)
        {
        
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }
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


            return mat;

        }
    }
}
