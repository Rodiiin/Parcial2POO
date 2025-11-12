using System;
using System.Collections.Generic;
using Parcial2POO.Interfaces;
using Parcial2POO.Cartas;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Roles;

public class JugadorUno : IJugador, IJugadorConMano
{
    private readonly List<ICarta> _mano;
    private readonly string _nombre;
    private readonly IEstrategiaJugadorUno _estrategia;

    public JugadorUno(string nombre, IEstrategiaJugadorUno estrategia)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío", nameof(nombre));
        if (estrategia == null)
            throw new ArgumentNullException(nameof(estrategia));

        _nombre = nombre;
        _estrategia = estrategia;
        _mano = new List<ICarta>();
    }

    public string Nombre => _nombre;
    public int CantidadCartas => _mano.Count;
    public IReadOnlyList<ICarta> Mano => _mano.AsReadOnly();

    public void AgregarCarta(ICarta carta)
    {
        if (carta == null)
            throw new ArgumentNullException(nameof(carta));
        _mano.Add(carta);
    }

    public ICarta JugarCarta(int indice)
    {
        if (indice < 0 || indice >= _mano.Count)
            throw new ArgumentOutOfRangeException(nameof(indice), "Índice de carta inválido");

        var carta = _mano[indice];
        _mano.RemoveAt(indice);
        return carta;
    }

    public int DecidirCartaAJugar(CartaUnoClasico cartaEnMesa, ColoresUno colorActual)
    {
        if (cartaEnMesa == null)
            throw new ArgumentNullException(nameof(cartaEnMesa));

        return _estrategia.DecidirCartaAJugar(_mano, cartaEnMesa, colorActual);
    }

    public ColoresUno ElegirColor()
    {
        return _estrategia.ElegirColor(_mano);
    }

    // Implementación de IJugadorConMano
    List<ICarta> IJugadorConMano.ObtenerMano()
    {
        return new List<ICarta>(_mano);
    }

    void IJugadorConMano.RecibirCarta(ICarta carta)
    {
        AgregarCarta(carta);
    }

    public override string ToString()
    {
        return $"{_nombre} ({_mano.Count} cartas)";
    }
}
