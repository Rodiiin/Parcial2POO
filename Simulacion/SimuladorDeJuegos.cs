using System;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Simulacion;

public class SimuladorDeJuegos
{

    private readonly IJuegoCartas _juego;

    public SimuladorDeJuegos(IJuegoCartas juego)
    {
        _juego = juego;
    }
    public void Ejecutar()
    {
        _juego.IniciarJuego();  // Una sola vez
        
        while (!_juego.HaFinalizado())
        {
            _juego.EjecutarTurno();
            _juego.FinalizarRonda();
        }

        Console.WriteLine("\nEl juego ha finalizado.");
    }

}
