using System;
using Parcial2POO.Interfaces;
using Parcial2POO.Reglas.ReglasBlackJack;
using Parcial2POO.Roles;

namespace Parcial2POO.Turnos;

public class TurnoBlackJack : ITurno
{
    private readonly JugadorBlackJack _jugador;
    private readonly IMazoCartas _mazo;
    private readonly ReglasBlackJack _reglas;

    public TurnoBlackJack(JugadorBlackJack jugador, IMazoCartas mazo, ReglasBlackJack reglas)
    {
        _jugador = jugador;
        _mazo = mazo;
        _reglas = reglas;
    }

    public void Ejecutar()
    {
        Console.WriteLine($"\n🎯 Turno de {_jugador.Nombre}");
        while (!_reglas.SePaso(_jugador) && _jugador.DeseaOtraCarta())
        {
            var carta = _mazo.SacarCarta();
            _jugador.RecibirCarta(carta);
        }
        if (_reglas.SePaso(_jugador))
            Console.WriteLine($"💥 {_jugador.Nombre} se pasó con {_jugador.ObtenerPuntos()} puntos.");
        else

            Console.WriteLine($"✅ {_jugador.Nombre} termina turno con {_jugador.ObtenerPuntos()} puntos.");
    }
}
