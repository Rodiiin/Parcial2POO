using System;

namespace Parcial2POO.Interfaces;

public interface IJuegoCartas
{
    void IniciarJuego();
    void EjecutarTurno();
    void FinalizarRonda();
    bool HaFinalizado();
}
