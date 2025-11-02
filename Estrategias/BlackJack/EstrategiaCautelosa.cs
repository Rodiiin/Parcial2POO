using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;

namespace Parcial2POO.Estrategias.BlackJack;

public class EstrategiaCautelosa : IEstrategiaJugadorBlackJack
{
    private readonly int _umbral;

    public int Umbral{ get; }

    //Constructor
    public EstrategiaCautelosa(int umbral = 15)
    {
        if (umbral < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(umbral), "No puedes pedir un umbral menor a 0");
        }

        Umbral = umbral;
    }

    //Métodos
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

        return total < Umbral;
    }
}
