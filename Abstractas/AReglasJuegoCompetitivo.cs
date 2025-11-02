using System;

namespace Parcial2POO.Abstractas;

public abstract class AReglasJuegoCompetitivo<TJugador>
{
    public abstract bool HaGanado(TJugador jugador, TJugador oponente);
    public abstract bool HaPerdido(TJugador jugador, TJugador oponente);

    public virtual bool EsEmpate(TJugador jugador, TJugador oponente)
    {
        return false; // por defecto, no hay empate
    }

}
