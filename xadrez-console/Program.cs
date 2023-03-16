
using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Instancia uma nova partida de xadrez
                PartidaDeXadrez partida = new PartidaDeXadrez();
                //Enquanto a partida nao estiver terminada
                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        //Imprime o tabuleiro, as pecas e mais algumas propriedades
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        //Origem recebe uma posicao lida convertida ao formato de tabuleiro
                        Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                        //Ve se é possivel pegar essa origem
                        partida.validarPosicaoDeOrigem(origem);

                        //Mostra as posicoes possiveis a partir desta origem
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();


                        Console.Clear();
                        //Usa dois argumentos um é o tabuleiro e outro uma matriz com posições possiveis
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch(TabuleiroExceptions e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                   
                }
   

            }
            catch (TabuleiroExceptions ex)
            { Console.WriteLine(ex.Message); }
            
        }
    }
}