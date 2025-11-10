using System;
using System.Collections.Generic;
using Parcial2POO.Interfaces;
using Parcial2POO.Cartas;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Estrategias.Uno;

public class EstrategiaAleatoria : IEstrategiaJugadorUno
{
    private readonly Random _random;

    public EstrategiaAleatoria()
    {
        _random = new Random();
    }

    public int DecidirCartaAJugar(IReadOnlyList<ICarta> mano, CartaUnoClasico cartaEnMesa, ColoresUno colorActual)
    {
        var cartasJugables = new List<int>();

        // Encontrar todas las cartas que se pueden jugar
        for (int i = 0; i < mano.Count; i++)
        {
            if (mano[i] is CartaUnoClasico carta && 
                Reglas.ReglasUnoClasico.EsJugadaValida(carta, cartaEnMesa, colorActual))
            {
                cartasJugables.Add(i);
            }
        }

        // Si no hay cartas jugables, retornar -1
        if (cartasJugables.Count == 0)
            return -1;

        // Elegir una carta al azar entre las jugables
        return cartasJugables[_random.Next(cartasJugables.Count)];
    }

    public ColoresUno ElegirColor(IReadOnlyList<ICarta> mano)
    {
        // Elegir un color al azar (excluyendo Negro)
        var colores = new[] 
        { 
            ColoresUno.Rojo, 
            ColoresUno.Azul, 
            ColoresUno.Verde, 
            ColoresUno.Amarillo 
        };
        
        return colores[_random.Next(colores.Length)];
    }
}
