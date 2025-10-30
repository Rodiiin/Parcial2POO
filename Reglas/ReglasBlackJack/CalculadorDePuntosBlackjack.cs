using System;
using System;
using System.Collections.Generic;
using Parcial2POO.Interfaces;
using Parcial2POO.Cartas;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;

namespace Parcial2POO.Reglas.ReglasBlackJack;

public class CalculadorDePuntosBlackjack : ICalculadorDePuntosBlackJack
{
        public int CalcularPuntos(List<ICarta> mano)
        {
            int total = 0;
            int cantidadAses = 0;

            foreach (var carta in mano)
            {
                if (carta is CartaBlackJack cb)
                {
                    if (cb.TipoCarta == TipoCarta.As)
                    {
                        cantidadAses++;
                        total += 11;
                    }
                    else
                    {
                        total += cb.Puntos;
                    }
                }
            }

            while (total > 21 && cantidadAses > 0)
            {
                total -= 10; // convierte un As de 11 a 1
                cantidadAses--;
            }

            return total;
        }

        public bool TieneBlackjack(List<ICarta> mano)
        {
            return mano.Count == 2 && CalcularPuntos(mano) == 21;
        }

        public bool SePaso(List<ICarta> mano)
        {
            return CalcularPuntos(mano) > 21;
        }
    }