using System;
using System.Collections.Generic;
using System.Linq;
using Parcial2POO.Mazo;
using Parcial2POO.Roles;
using Parcial2POO.Turnos;
using Parcial2POO.Cartas;
using Parcial2POO.Reglas;
using Parcial2POO.Interfaces;
using Parcial2POO.Estrategias.Uno;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Juegos;

public class JuegoUnoClasico : IJuegoCartas
{
    private readonly MazoUno _mazo;
    private readonly List<JugadorUno> _jugadores;
    private readonly TurnoUno _turnos;
    private CartaUnoClasico _cartaEnMesa;
    private ColoresUno _colorActual;
    private bool _juegoActivo;
    private int _rondaActual;
    private int _turnoActual;
    private JugadorUno _ganador;

    public JuegoUnoClasico(List<JugadorUno> jugadores, MazoUno mazo)
    {
        if (jugadores == null || jugadores.Count < 2)
            throw new ArgumentException("Se necesitan al menos 2 jugadores", nameof(jugadores));
        if (mazo == null)
            throw new ArgumentNullException(nameof(mazo));

        _mazo = mazo;
        _jugadores = jugadores;
        _turnos = new TurnoUno(jugadores.Count);
        _juegoActivo = false;
        _rondaActual = 0;
        _turnoActual = 0;
        _ganador = null;
    }

    public string Nombre => "UNO Clásico";
    public string Descripcion => "Juego de cartas UNO para 2-10 jugadores";
    public bool JuegoActivo => _juegoActivo;
    public int RondaActual => _rondaActual;
    public CartaUnoClasico CartaEnMesa => _cartaEnMesa;
    public ColoresUno ColorActual => _colorActual;
    public JugadorUno JugadorActual => _jugadores[_turnos.JugadorActual];
    public JugadorUno Ganador => _ganador;
    public IReadOnlyList<JugadorUno> Jugadores => _jugadores.AsReadOnly();

    public void IniciarJuego()
    {
        _juegoActivo = true;
        _rondaActual = 0;
        _ganador = null;

        // Distribuir 7 cartas a cada jugador
        for (int i = 0; i < _jugadores.Count; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                _jugadores[i].AgregarCarta(_mazo.SacarCarta());
            }
        }

        // Sacar carta inicial para la mesa
        _cartaEnMesa = _mazo.SacarCarta() as CartaUnoClasico;
        _colorActual = _cartaEnMesa.Color == ColoresUno.Negro 
            ? ColoresUno.Rojo 
            : _cartaEnMesa.Color;

        // Mostrar carta inicial con número si es numérica
        string cartaInicialTexto = _cartaEnMesa.Tipo == TiposUno.Numerica
            ? $"{_cartaEnMesa.ValorNumerico} {_cartaEnMesa.Color}"
            : $"{_cartaEnMesa.Tipo} {_cartaEnMesa.Color}";
        Console.WriteLine($"Carta inicial en mesa: {cartaInicialTexto}");
    }

    public void EjecutarTurno()
    {
        if (!_juegoActivo) return;

        var jugadorActual = JugadorActual;
        int indiceCartaAJugar = jugadorActual.DecidirCartaAJugar(_cartaEnMesa, _colorActual);

        Console.WriteLine($"\n--- Turno {_turnoActual + 1} (Ronda {_rondaActual + 1}) ---");
        Console.WriteLine($"Turno de: {jugadorActual.Nombre}");
        
        // Mostrar carta en mesa con número si es numérica
        string cartaMesaTexto = _cartaEnMesa.Tipo == TiposUno.Numerica
            ? $"{_cartaEnMesa.ValorNumerico} {_cartaEnMesa.Color}"
            : $"{_cartaEnMesa.Tipo} {_cartaEnMesa.Color}";
        Console.WriteLine($"Carta en mesa: {cartaMesaTexto}");
        Console.WriteLine($"Color actual: {_colorActual}");

        if (indiceCartaAJugar == -1)
        {
            var cartaRobada = _mazo.SacarCarta();
            jugadorActual.AgregarCarta(cartaRobada);
            Console.WriteLine($"{jugadorActual.Nombre} roba una carta. Cartas: {jugadorActual.CantidadCartas}");
            _turnos.ObtenerSiguiente();
            IncrementarTurno();
            return;
        }

        var cartaJugada = jugadorActual.JugarCarta(indiceCartaAJugar);
        _cartaEnMesa = cartaJugada as CartaUnoClasico;
        
        // Mostrar carta jugada con número si es numérica
        string cartaJugadaTexto = _cartaEnMesa.Tipo == TiposUno.Numerica
            ? $"{_cartaEnMesa.ValorNumerico} {_cartaEnMesa.Color}"
            : $"{_cartaEnMesa.Tipo} {_cartaEnMesa.Color}";
        Console.WriteLine($"{jugadorActual.Nombre} juega: {cartaJugadaTexto}");
        Console.WriteLine($"Cartas restantes: {jugadorActual.CantidadCartas}");

        var efecto = ReglasUnoClasico.EvaluarEfecto(_cartaEnMesa);

        if (efecto.CambiaColor)
        {
            _colorActual = jugadorActual.ElegirColor();
            Console.WriteLine($"{jugadorActual.Nombre} elige color: {_colorActual}");
        }
        else
        {
            _colorActual = _cartaEnMesa.Color;
        }

        if (efecto.Reversa)
        {
            _turnos.CambiarDireccion();
            Console.WriteLine("¡REVERSA! La dirección cambia.");
            _turnos.ObtenerSiguiente();
        }
        else if (efecto.Salta)
        {
            // Identificar al jugador que será saltado antes de modificar el turno
            int indiceSaltado = _turnos.VerSiguiente();
            Console.WriteLine($"¡SALTA! {_jugadores[indiceSaltado].Nombre} pierde el turno.");
            _turnos.SaltarSiguiente();
        }
        else
        {
            _turnos.ObtenerSiguiente();
        }

        if (efecto.CartasARecibir > 0)
        {
            var siguienteJugador = _jugadores[_turnos.JugadorActual];
            for (int i = 0; i < efecto.CartasARecibir; i++)
            {
                siguienteJugador.AgregarCarta(_mazo.SacarCarta());
            }
            Console.WriteLine($"{siguienteJugador.Nombre} roba {efecto.CartasARecibir} cartas. Cartas: {siguienteJugador.CantidadCartas}");
        }

        IncrementarTurno();
    }

    private void IncrementarTurno()
    {
        _turnoActual++;
        // Sumar ronda cuando todos los jugadores jugaron
        if (_turnoActual % _jugadores.Count == 0)
        {
            _rondaActual++;
        }
    }

    public void FinalizarRonda()
    {
        //  Verificar ganador después de ejecutar turno
        foreach (var jugador in _jugadores)
        {
            if (jugador.CantidadCartas == 0)
            {
                _juegoActivo = false;
                _ganador = jugador;
                Console.WriteLine($"\n¡¡¡{_ganador.Nombre} GANA en la ronda {_rondaActual}!!!");
                return;
            }
        }
    }

    public bool HaFinalizado()
    {
        return !_juegoActivo || _ganador != null;
    }

    public string ObtenerResultado()
    {
        if (_ganador != null)
            return $"{_ganador.Nombre} gana en la ronda {_rondaActual}";
        return $"Partida sin ganador después de {_rondaActual} rondas";
    }

    public void Simular()
    {
        Console.WriteLine("=== SIMULACIÓN DE UNO CLÁSICO ===\n");
        Console.WriteLine($"Jugadores: {string.Join(", ", _jugadores.Select(j => j.Nombre))}\n");
        
        IniciarJuego();
        const int maxRondas = 10000;

        while (!HaFinalizado() && _rondaActual < maxRondas)
        {
            EjecutarTurno();
            FinalizarRonda();
        }

        Console.WriteLine($"\n=== RESULTADO FINAL ===");
        Console.WriteLine(ObtenerResultado());
    }
}
