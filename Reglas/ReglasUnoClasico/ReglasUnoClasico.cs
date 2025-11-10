using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;

namespace Parcial2POO.Reglas;

public class ReglasUnoClasico
{
    public record EfectoResultado(int CartasARecibir = 0, bool Salta = false, bool Reversa = false, bool EsComodin = false, bool CambiaColor = false);

    // Valida si 'jugada' puede jugarse sobre 'topeMesa' considerando el color actual en mesa.
    public static bool EsJugadaValida(CartaUnoClasico jugada, CartaUnoClasico topeMesa, ColoresUno colorActual)
    {
        if (jugada == null) throw new ArgumentNullException(nameof(jugada));
        if (topeMesa == null) throw new ArgumentNullException(nameof(topeMesa));

        // Comodines siempre válidos (el motor de juego preguntará el color)
        if (jugada.Tipo == TiposUno.Comodin || jugada.Tipo == TiposUno.ComodinTomaCuatro)
            return true;

        // Coincide con el color actual
        if (jugada.Color == colorActual)
            return true;

        // Misma acción (Salta, Reversa, TomaDos)
        if (jugada.Tipo == topeMesa.Tipo && jugada.Tipo != TiposUno.Numerica)
            return true;

        // Mismo número si ambas son numéricas
        if (jugada.Tipo == TiposUno.Numerica && topeMesa.Tipo == TiposUno.Numerica)
            return jugada.Valor == topeMesa.Valor;

        return false;
    }

    // Evalúa el efecto de la carta jugada; no modifica estado, solo describe el efecto para que Partida lo aplique.
    public static EfectoResultado EvaluarEfecto(CartaUnoClasico carta)
    {
        if (carta == null) throw new ArgumentNullException(nameof(carta));

        return carta.Tipo switch
        {
            TiposUno.TomaDos => new EfectoResultado(CartasARecibir: 2),
            TiposUno.Salta => new EfectoResultado(Salta: true),
            TiposUno.Reversa => new EfectoResultado(Reversa: true),
            TiposUno.Comodin => new EfectoResultado(EsComodin: true, CambiaColor: true),
            TiposUno.ComodinTomaCuatro => new EfectoResultado(CartasARecibir: 4, EsComodin: true, CambiaColor: true),
            _ => new EfectoResultado()
        };
    }
}
