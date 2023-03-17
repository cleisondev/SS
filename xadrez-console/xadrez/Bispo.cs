using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "B";
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

            //NO
            pos.definirValores(posicao.linha - 1, posicao.coluna -1);
            while (Tab.posValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) 
                {
                    break;
                }
                pos.definirValores(pos.linha -1, pos.coluna -1);
            }

            //NE
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (Tab.posValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }

            //SO
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (Tab.posValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }


            return mat;

        }
    }
}
