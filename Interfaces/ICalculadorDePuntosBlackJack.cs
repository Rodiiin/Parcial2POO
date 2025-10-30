using System;

namespace Parcial2POO.Cartas;

public interface ICalculadorDePuntosBlackJack
{
    //Falta ver donde crear la clase de calculor de puntos e implementar el calculo de puntos para blackjack
    int CalcularPuntos(List<ICarta> mano);
    bool TieneBlackjack(List<ICarta> mano);
    bool SePaso(List<ICarta> mano);
}



