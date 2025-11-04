using System;
using Parcial2POO.Cartas;
using Parcial2POO.Estrategias.BlackJack;
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

        var dealer = new JugadorBlackJack("1","Dealer", calculador,new EstrategiaDealer(calculador));
        var jugadores = new List<JugadorBlackJack>
        {
            new JugadorBlackJack("1J","J Cauteloso", calculador, new EstrategiaCautelosa(calculador)),
            new JugadorBlackJack("2J","J Temerario", calculador,new EstrategiaTemeraria(calculador))
        };

        return new JuegoBlackJack(mazo, reglas, dealer, jugadores);
    }
}
