using System;

namespace Parcial2POO.Interfaces;

public interface IJuegoConPuntuacion
{
    int ObtenerPuntajeJugador(string idJugador);
    bool JugadorHaPerdido(string idJugador);
}
