using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;
using System.Collections.Generic;

namespace Parcial2POO.Interfaces;

public interface IEstrategiaJugadorUno
{
    // Decide qué carta jugar basado en el estado actual del juego
    int DecidirCartaAJugar(IReadOnlyList<ICarta> mano, CartaUnoClasico cartaEnMesa, ColoresUno colorActual);

    // Elige un color cuando se juega un comodín
    ColoresUno ElegirColor(IReadOnlyList<ICarta> mano);
}
