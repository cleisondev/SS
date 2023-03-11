
namespace tabuleiro
{
     class Posicao
    {
        public int coluna { get; set; }
        public int linha { get; set; }

        public Posicao(int linha, int coluna) { this.linha = linha; this.coluna = coluna; }


        public override string ToString()
        {
            return linha
                + ", "
                + coluna;
        }
    }
        
}

            