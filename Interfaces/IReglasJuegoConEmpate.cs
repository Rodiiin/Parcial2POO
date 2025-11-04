using System;

namespace Parcial2POO.Interfaces;

public interface IReglasJuegoConEmpate<TJugador>
{
    bool EsEmpate(TJugador jugador, TJugador oponente);
}
