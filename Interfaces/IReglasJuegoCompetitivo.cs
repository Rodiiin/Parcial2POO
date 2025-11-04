using System;

namespace Parcial2POO.Interfaces;

public interface IReglasJuegoCompetitivo<TJugador>
{
    bool HaGanado(TJugador jugador, TJugador oponente);
    bool HaPerdido(TJugador jugador, TJugador oponente);
}
