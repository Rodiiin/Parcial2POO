using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;

namespace Parcial2POO.Estrategias.BlackJack;

public class EstrategiaCautelosa : IEstrategiaJugadorBlackJack
{
    public int Umbral { get; }
    private readonly ICalculadorDePuntosBlackJack _calculador;

    //Constructor
    public EstrategiaCautelosa(ICalculadorDePuntosBlackJack calculador, int umbral = 15)
    {
        if (umbral < 0)
        {
            throw new ArgumentOutOfRangeException("No puedes pedir un umbral menor a 0");
        }

        Umbral = umbral;
        _calculador = calculador ?? throw new ArgumentNullException(nameof(calculador));
    }

    //Métodos
    public bool DeseaOtraCarta(List<ICarta> mano)
    {
        return _calculador.CalcularPuntos(mano) < Umbral;
    }
}
