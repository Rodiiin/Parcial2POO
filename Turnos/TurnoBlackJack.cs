using System;
using Parcial2POO.Interfaces;
using Parcial2POO.Reglas.ReglasBlackJack;
using Parcial2POO.Roles;

namespace Parcial2POO.Turnos;

public class TurnoBlackJack : ITurno
{
    private readonly IJugadorBlackJack _jugador;
    private readonly IMazoCartas _mazo;
    private readonly IReglasJuegoCompetitivoBlackJack _reglas;

    public TurnoBlackJack(IJugadorBlackJack jugador, IMazoCartas mazo, IReglasJuegoCompetitivoBlackJack reglas)
    {
        _jugador = jugador;
        _mazo = mazo;
        _reglas = reglas;
    }

    public void Ejecutar()
    {
        Console.WriteLine($"\nTurno de {_jugador.Nombre}");
        while (!_reglas.SePaso(_jugador) && _jugador.DeseaOtraCarta())
        {
            var carta = _mazo.SacarCarta();
            _jugador.RecibirCarta(carta);
        }
        if (_reglas.SePaso(_jugador))
            Console.WriteLine($"{_jugador.Nombre} se pasó con {_jugador.ObtenerPuntos()} puntos.");
        else

            Console.WriteLine($"{_jugador.Nombre} termina turno con {_jugador.ObtenerPuntos()} puntos.");
    }
}
