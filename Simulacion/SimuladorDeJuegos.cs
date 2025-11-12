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
        int ronda = 1;
        while (!_juego.HaFinalizado())
        {
            Console.WriteLine($"\n🔁 Comenzando ronda {ronda}");
            _juego.IniciarJuego();
            Console.WriteLine("Ejecutando turnos:");
            _juego.EjecutarTurno();
            Console.WriteLine("Finalizando ronda:");
            _juego.FinalizarRonda();
            ronda++;
        }

        Console.WriteLine("\nEl juego ha finalizado.");
    }

}
