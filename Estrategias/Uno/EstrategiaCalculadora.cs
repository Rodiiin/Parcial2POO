using System;
using System.Collections.Generic;
using System.Linq;
using Parcial2POO.Interfaces;
using Parcial2POO.Cartas;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Estrategias.Uno;

public class EstrategiaCalculadora : IEstrategiaJugadorUno
{
    private readonly Random _random;

    public EstrategiaCalculadora()
    {
        _random = new Random();
    }

    public int DecidirCartaAJugar(IReadOnlyList<ICarta> mano, CartaUnoClasico cartaEnMesa, ColoresUno colorActual)
    {
        var cartasJugables = new List<(int indice, CartaUnoClasico carta)>();

        // Encontrar todas las cartas jugables y convertirlas
        for (int i = 0; i < mano.Count; i++)
        {
            if (mano[i] is CartaUnoClasico carta && 
                Reglas.ReglasUnoClasico.EsJugadaValida(carta, cartaEnMesa, colorActual))
            {
                cartasJugables.Add((i, carta));
            }
        }

        if (cartasJugables.Count == 0)
            return -1;

        // Priorizar cartas en este orden:
        // 1. Comodín Toma Cuatro si tenemos pocas cartas
        // 2. Cartas de acción (TomaDos, Reversa, Salta)
        // 3. Comodín normal
        // 4. Cartas numéricas del color actual

        // Si tenemos pocas cartas (<= 3), usar comodines
        if (mano.Count <= 3)
        {
            var comodinTomaCuatro = cartasJugables
                .FirstOrDefault(x => x.carta.Tipo == TiposUno.ComodinTomaCuatro);
            if (comodinTomaCuatro.carta != null)
                return comodinTomaCuatro.indice;
        }

        // Buscar cartas de acción
        var cartaAccion = cartasJugables
            .FirstOrDefault(x => x.carta.Tipo == TiposUno.TomaDos || 
                               x.carta.Tipo == TiposUno.Reversa ||
                               x.carta.Tipo == TiposUno.Salta);
        if (cartaAccion.carta != null)
            return cartaAccion.indice;

        // Buscar comodín normal
        var comodin = cartasJugables
            .FirstOrDefault(x => x.carta.Tipo == TiposUno.Comodin);
        if (comodin.carta != null)
            return comodin.indice;

        // Priorizar cartas del color actual
        var cartaColorActual = cartasJugables
            .FirstOrDefault(x => x.carta.Color == colorActual);
        if (cartaColorActual.carta != null)
            return cartaColorActual.indice;

        // Si no hay nada especial, jugar la primera carta válida
        return cartasJugables[0].indice;
    }

    public ColoresUno ElegirColor(IReadOnlyList<ICarta> mano)
    {
        // Contar cartas por color (excluyendo negras)
        var conteoColores = mano
            .OfType<CartaUnoClasico>()
            .Where(c => c.Color != ColoresUno.Negro)
            .GroupBy(c => c.Color)
            .ToDictionary(g => g.Key, g => g.Count());

        // Si no hay cartas de color, elegir uno al azar
        if (!conteoColores.Any())
        {
            var colores = new[] 
            { 
                ColoresUno.Rojo, 
                ColoresUno.Azul, 
                ColoresUno.Verde, 
                ColoresUno.Amarillo 
            };
            return colores[_random.Next(colores.Length)];
        }

        // Elegir el color del que más cartas tengamos
        return conteoColores.MaxBy(kvp => kvp.Value).Key;
    }
}
