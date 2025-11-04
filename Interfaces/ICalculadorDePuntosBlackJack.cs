using System;

namespace Parcial2POO.Cartas;

public interface ICalculadorDePuntosBlackJack
{
    int CalcularPuntos(List<ICarta> mano);
    bool TieneBlackjack(List<ICarta> mano);
    bool SePaso(List<ICarta> mano);
}



