using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Juegos;

public class JuegoBlackJack : IJugador
{
    //Atributos
    private readonly List<ICarta> _mano;
    private readonly ICalculadorDePuntosBlackJack _calculador;
    private readonly IEstrategiaJugadorBlackJack _estrategia;
    public string Nombre { get; }

    //Constructor
    public JuegoBlackJack(string nombre, ICalculadorDePuntosBlackJack calculador, IEstrategiaJugadorBlackJack estrategia)
    {
        this.Nombre = nombre;
        this._mano = new List<ICarta>();
        this._calculador = calculador;
        this._estrategia = estrategia;
    }

    //Metodos generales
    public void RecibirCarta(ICarta carta)
    {
        _mano.Add(carta);
    }

    public List<ICarta> ObtenerMano()
    {
        return _mano;
    }

    

    //Metodos especificos de BlackJack
    public int ObtenerPuntos()
    {
        return _calculador.CalcularPuntos(_mano);
    }
    public bool TieneBlackjack()
    {
        return _calculador.TieneBlackjack(_mano);
    }

    public bool SePaso()
    {
        return _calculador.SePaso(_mano);
    }

    //Métodos especificos de Estrategia 
    public bool DebePedirCarta()
    {
        return _estrategia.DebePedirCarta(_mano);
    }
}
