using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;

namespace Parcial2POO.Estrategias.BlackJack;

public class EstrategiaTemeraria : IEstrategiaJugadorBlackJack
{

    public bool DebePedirCarta(List<ICarta> mano)
    {
        int total = 0;
        int ases = 0;

        foreach (var carta in mano)
        {
            if (carta is CartaBlackJack cb)
            {
                if (cb.TipoCarta == TipoCarta.As)
                {
                    ases += 1;
                    total += 11;
                }
                else
                {
                    total += cb.Puntos;
                }
            }
        }

        while (total > 21 && ases > 0)
        {
            total -= 10;
            ases -= 1;
        }

        return total < 21;
    }

}
