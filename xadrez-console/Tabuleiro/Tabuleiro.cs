

namespace tabuleiro
{
     class Tabuleiro
    {
        public int linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linha, int colunas)
        {
            this.linhas = linha;
            this.Colunas = colunas;
            pecas = new Peca[linha,colunas];
        }
    }
}
