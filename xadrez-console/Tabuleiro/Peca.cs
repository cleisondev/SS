﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace tabuleiro
{
     abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get;protected set; }
        public Tabuleiro Tab { get;protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.Cor = cor;
            this.Tab = tab;
            this.QteMovimentos = 0;
        }

        public void incremetarQtdMovimentos()
        {
            QteMovimentos++;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for(int i = 0; i < Tab.linhas; i++) 
            {
                for(int j = 0; j < Tab.colunas; j++)
                {
                    if (mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }
        public abstract bool[,] movimentosPossiveis();
    }
}
