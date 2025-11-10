using System;
using System.Collections.Generic;

namespace Parcial2POO.Turnos;

public class TurnoUno
{
    private readonly List<int> _jugadores;
    private int _indiceActual;
    private bool _direccionNormal; // true = sentido horario, false = antihorario

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
        ActualizarIndice(); // Salta el siguiente
        ActualizarIndice(); // Avanza al subsiguiente
    }

    private void ActualizarIndice()
    {
        if (_direccionNormal)
        {
            _indiceActual = (_indiceActual + 1) % _jugadores.Count;
        }
        else
        {
            _indiceActual = (_indiceActual - 1 + _jugadores.Count) % _jugadores.Count;
        }
    }
}
