using System;
using Parcial2POO.Cartas;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Roles;

public class JugadorBlackJack : IJugador,
 IJugadorConMano,
 IJugadorQuePuedePedirCarta,
 IJugadorConPuntaje

{
    private readonly List<ICarta> _mano = new(); //<-- aqui inicializo?
    private readonly ICalculadorDePuntosBlackJack _calculador;
    private readonly IEstrategiaJugadorBlackJack _estrategia;

    public string Id { get; }
    public string Nombre { get; }

    public JugadorBlackJack(string id, string nombre, ICalculadorDePuntosBlackJack calculador, IEstrategiaJugadorBlackJack estrategia)
    {
        this.Id = id;
        this.Nombre = nombre;
        _calculador = calculador;
        _estrategia = estrategia;

    }

    // --- IJugadorConMano ---
    public void RecibirCarta(ICarta carta)
    {
        _mano.Add(carta);
        Console.WriteLine($"🃏 {this.Nombre} recibió: {carta}");
    }

    public List<ICarta> ObtenerMano()
    {
        return _mano;
    }

    // --- IJugadorQuePuedePedirCarta ---
    public bool DeseaOtraCarta()
    {
        bool decision = _estrategia.DeseaOtraCarta(_mano);
        Console.WriteLine($"🤔 {this.Nombre} {(decision ? "pide otra carta" : "se planta")} con {_calculador.CalcularPuntos(_mano)} puntos.");
        return _estrategia.DeseaOtraCarta(_mano);
    }

    // --- IJugadorConPuntaje ---
    public int ObtenerPuntos()
    {
        return _calculador.CalcularPuntos(_mano);
    }

    public void LimpiarMano()
    {
        _mano.Clear();
    }



  

}
