using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "C";
        }

        //Retorna true se a posicao for nula ou a cor em questão for diferente da cor do parametro
        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        //Cria uma matriz booleana com falses e trues, uma nova posicao 0,0
        //Ver aquela posicao de tal peca mais os calculos
        //Se a posicao for valida, e se pode mover, retorna a matriz com a posição possivel.
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna -2);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Nordeste
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Direita
            pos.definirValores(posicao.linha -2, posicao.coluna + 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Sudeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna +2);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Sudoeste
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Esquerda
            pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            //Noroeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
            if (Tab.posValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }


            return mat;

        }
    }
}
