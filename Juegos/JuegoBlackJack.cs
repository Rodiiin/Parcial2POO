using System;
using Parcial2POO.Cartas;
using Parcial2POO.Interfaces;
using Parcial2POO.Reglas.ReglasBlackJack;
using Parcial2POO.Roles;
using Parcial2POO.Turnos;

namespace Parcial2POO.Juegos;

public class JuegoBlackJack : IJuegoBlackJack
{
    private readonly IMazoCartas _mazo;
    private readonly IMazoConDescarte? _mazoConDescarte;
    private readonly IMazoReciclable? _mazoReciclable;
    private readonly ReglasBlackJack _reglas;
    private readonly JugadorBlackJack _dealer;
    private readonly List<JugadorBlackJack> _jugadores;
    private int _rondasJugadas = 0;
    private readonly int _maxRondas = 1;
    public JuegoBlackJack(IMazoCartas mazo, ReglasBlackJack reglasBlackJack, JugadorBlackJack dealer, List<JugadorBlackJack> jugadores)
    {
        _mazo = mazo;
        _mazoConDescarte = mazo as IMazoConDescarte;
        _mazoReciclable = mazo as IMazoReciclable;
        _reglas = reglasBlackJack;
        _dealer = dealer;
        _jugadores = jugadores;

    }

    public void IniciarJuego()
    {
        RepartirCartas();
    }

    public void EjecutarTurno()
    {
        foreach (var jugador in _jugadores)
        {
            var turno = new TurnoBlackJack(jugador, _mazo, _reglas);
            turno.Ejecutar();
        }

        JugarBanca();
    }

    public void FinalizarRonda()
    {
        foreach (var jugador in _jugadores)
        {
            foreach (var carta in jugador.ObtenerMano())
                _mazoConDescarte?.DescartarCarta(carta);

            jugador.LimpiarMano();
        }

        foreach (var carta in _dealer.ObtenerMano())
            _mazoConDescarte?.DescartarCarta(carta);

        _dealer.LimpiarMano();
         _rondasJugadas++;
    }
    public bool HaFinalizado()
    {
        return _rondasJugadas >= _maxRondas;
    }

    public void RepartirCartas()
    {
        foreach (var jugador in _jugadores)
        {
            jugador.RecibirCarta(_mazo.SacarCarta());
            jugador.RecibirCarta(_mazo.SacarCarta());
        }

        _dealer.RecibirCarta(_mazo.SacarCarta());
        _dealer.RecibirCarta(_mazo.SacarCarta());
    }

    public void JugarBanca()
    {
        while (!_reglas.SePaso(_dealer) && _dealer.DeseaOtraCarta())
        {
            _dealer.RecibirCarta(_mazo.SacarCarta());
        }
    }

    public int ObtenerPuntajeJugador(string idJugador)
    {
        foreach (var jugador in _jugadores)
        {
            if (jugador.Id == idJugador)
            {
                return jugador.ObtenerPuntos();
            }
        }
        throw new ArgumentException($"No se encontró el jugador con ID: {idJugador}");
    }

    public bool JugadorHaPerdido(string idJugador)
    {
        foreach (var jugador in _jugadores)
        {
            if (jugador.Id == idJugador)
            {
                return _reglas.HaPerdido(jugador, _dealer);
            }
        }

        throw new ArgumentException($"No se encontró el jugador con ID: {idJugador}");
    }



}
