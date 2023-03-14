
namespace tabuleiro
{
     class Tabuleiro
    {
        public int linhas { get;private set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas,colunas];
        }

        public bool existePeca(Posicao pos) 
        {
            ValidarPosicao(pos);
            return peca(pos) != null;     
        }

        public Peca peca(int linha, int coluna) 
        {
            return pecas[linha,coluna];
        }


        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        //Se a função existe peça for true, imprime esse erro na tela
        //Caso false, a peca recebe os parametros do pos que foi colocado no programa .
        public void colocarPeca(Peca p, Posicao pos) 
        {
            if(existePeca(pos)) { throw new TabuleiroExceptions("Já existe uma peça nessa posição"); }

            pecas[pos.linha,pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca( Posicao pos)
        {
            if (peca(pos) == null) { return null; }
            Peca aux = peca(pos);
            aux.posicao = null;
            pecas[pos.linha, pos.coluna] = null;
            return aux;


        }

        //Retorna se a posição estiver fora do tabuleiro ou não.
        public bool posValida(Posicao pos) 
        {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }
         
        //Se a posição realmente estiver fora dos parametros do tabuleiro a função imprime esse erro na tela.
        public void ValidarPosicao(Posicao pos)
        {
            if (!posValida(pos))
            {
                throw new TabuleiroExceptions("Posição inválida");
            }
        }
    }
}
