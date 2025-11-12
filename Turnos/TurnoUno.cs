using System;
using System.Collections.Generic;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Turnos;

public class TurnoUno : ITurno
{
    private readonly List<int> _jugadores;
    private int _indiceActual;
    private bool _direccionNormal; // true = horario, false = antihorario

    public TurnoUno(int cantidadJugadores)
    {
        if (cantidadJugadores < 2)
            throw new ArgumentException("Se necesitan al menos 2 jugadores", nameof(cantidadJugadores));

        _jugadores = new List<int>();
        for (int i = 0; i < cantidadJugadores; i++)
            _jugadores.Add(i);

        _indiceActual = 0;
        _direccionNormal = true;
    }

    public int JugadorActual => _jugadores[_indiceActual];

    // Nueva propiedad para que el juego pueda saber la dirección actual (si la necesitas externamente)
    public bool DireccionNormal => _direccionNormal;

    // Nuevo método: devuelve el índice (no el Id) del siguiente jugador sin avanzar el turno
    public int VerSiguiente()
    {
        if (_direccionNormal)
            return (_indiceActual + 1) % _jugadores.Count;
        else
            return (_indiceActual - 1 + _jugadores.Count) % _jugadores.Count;
    }

    public int ObtenerSiguiente()
    {
        ActualizarIndice();
        return JugadorActual;
    }

    public void CambiarDireccion()
    {
        _direccionNormal = !_direccionNormal;
    }

    public void SaltarSiguiente()
    {
        // Avanza al “siguiente” (que se salta) y luego al subsiguiente que jugará
        ActualizarIndice(); // jugador saltado
        ActualizarIndice(); // jugador que sí jugará
    }

    private void ActualizarIndice()
    {
        if (_direccionNormal)
            _indiceActual = (_indiceActual + 1) % _jugadores.Count;
        else
            _indiceActual = (_indiceActual - 1 + _jugadores.Count) % _jugadores.Count;
    }

    public void Ejecutar()
    {
        ObtenerSiguiente();
    }
}
