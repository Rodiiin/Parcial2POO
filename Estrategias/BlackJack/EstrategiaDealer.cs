using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Estrategias.BlackJack;

public class EstrategiaDealer : IEstrategiaJugadorBlackJack
{
    private readonly ICalculadorDePuntosBlackJack _calculador;

    public EstrategiaDealer(ICalculadorDePuntosBlackJack calculador)
    {
        _calculador = calculador ?? throw new ArgumentNullException(nameof(calculador));
    }
    public bool DeseaOtraCarta(List<ICarta> mano)
    {
        return  _calculador.CalcularPuntos(mano) < 17;
    }
}

