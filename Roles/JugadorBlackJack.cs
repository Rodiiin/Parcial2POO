using System;
using Parcial2POO.Cartas;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Roles;

public class JugadorBlackJack : IJugador,
 IJugadorConMano,
 IJugadorQuePuedePedirCarta,
 IJugadorConPuntaje,
 IJugadorConCondicionesDeVictoria

{
    private readonly List<ICarta> _mano;
    private readonly ICalculadorDePuntosBlackJack _calculador;
    private readonly IEstrategiaJugadorBlackJack _estrategia;

    public string Nombre { get; }

    public JugadorBlackJack(string nombre, ICalculadorDePuntosBlackJack calculador, IEstrategiaJugadorBlackJack estrategia)
    {
        this.Nombre = nombre;
        _calculador = calculador;
        _estrategia = estrategia;

    }

    // --- IJugadorConMano ---
    public void RecibirCarta(ICarta carta)
    {
        _mano.Add(carta);
    }

    public List<ICarta> ObtenerMano()
    {
        return _mano;
    }

    // --- IJugadorQuePuedePedirCarta ---
    public bool DebePedirCarta()
    {
        return _estrategia.DebePedirCarta(_mano);
    }

    // --- IJugadorConPuntaje ---
    public int ObtenerPuntos()
    {
        return _calculador.CalcularPuntos(_mano);
    }

    // --- IJugadorConCondicionesDeVictoria ---
    public bool HaGanado()
    {
        // return _reglas.EvaluarVictoria(this);
    throw new NotImplementedException("La evaluación de victoria será delegada a ReglasBlackJack.");        
    }

    public bool HaPerdido()
    {
        // return _reglas.EvaluarDerrota(this);
    throw new NotImplementedException("La evaluación de derrota será delegada a ReglasBlackJack.");        
    }

  

}
