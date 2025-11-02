using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;

namespace Parcial2POO.Estrategias.BlackJack;

public class EstrategiaTemeraria : IEstrategiaJugadorBlackJack
{

    private readonly ICalculadorDePuntosBlackJack _calculador;

    public EstrategiaTemeraria(ICalculadorDePuntosBlackJack calculador)
    {
        _calculador = calculador ?? throw new ArgumentNullException(nameof(calculador));
    }
    public bool DebePedirCarta(List<ICarta> mano)
    {
        return _calculador.CalcularPuntos(mano) < 21;
    }

}
