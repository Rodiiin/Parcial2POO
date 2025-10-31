using System;

namespace Parcial2POO.Interfaces;

public interface IJugadorQuePuedePedirCarta
{
    //representa una decisión interna del jugador, que puede delegar esa lógica a una estrategia.
    bool DebePedirCarta();
}
