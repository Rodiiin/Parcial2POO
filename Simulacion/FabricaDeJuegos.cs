using System;
using System.Collections.Generic;
using Parcial2POO.Cartas;
using Parcial2POO.Estrategias.BlackJack;
using Parcial2POO.Estrategias.Uno;
using Parcial2POO.Interfaces;
using Parcial2POO.Juegos;
using Parcial2POO.Mazo;
using Parcial2POO.Reglas.ReglasBlackJack;
using Parcial2POO.Roles;

namespace Parcial2POO.Simulacion;

public class FabricaDeJuegos
{
    public static IJuegoCartas CrearJuego(string tipo)
    {
        if (tipo.ToLower() == "blackjack")
        {
            return CrearBlackJack();
        }
        else if (tipo.ToLower() == "uno")
        {
            return CrearUno();
        }
        else
        {
            throw new ArgumentException("Tipo de juego no reconocido.");
        }
    }

    private static IJuegoCartas CrearBlackJack()
    {
        var mazo = new MazoBlackJack();
        var calculador = new CalculadorDePuntosBlackjack();
        var reglas = new ReglasBlackJack(new CalculadorDePuntosBlackjack());

        var dealer = new JugadorBlackJack("1", "Dealer", calculador, new EstrategiaDealer(calculador));
        var jugadores = new List<IJugadorBlackJack>
        {
            new JugadorBlackJack("1J","J Cauteloso", calculador, new EstrategiaCautelosa(calculador)),
            new JugadorBlackJack("2J","J Temerario", calculador, new EstrategiaTemeraria(calculador))
        };

        return new JuegoBlackJack(mazo, reglas, dealer, jugadores);
    }

    private static IJuegoCartas CrearUno()
    {
        var mazo = new MazoUno(cantidadDeMazos: 1);
        var estrategiaAleatoria = new EstrategiaAleatoria();
        var estrategiaCalculadora = new EstrategiaCalculadora();

        var jugadores = new List<JugadorUno>
        {
            new JugadorUno("Jugador 1", estrategiaAleatoria),
            new JugadorUno("Jugador 2", estrategiaCalculadora),
            new JugadorUno("Jugador 3", estrategiaAleatoria),
            new JugadorUno("Jugador 4", estrategiaCalculadora)
        };

        return new JuegoUnoClasico(jugadores, mazo);
    }
}
