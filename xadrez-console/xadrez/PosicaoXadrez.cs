using tabuleiro;

namespace xadrez
{
     class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha) 
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        //Retorna uma posicao no formato do tabuleiro
        public Posicao ToPosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');   
        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
