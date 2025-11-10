using System;
using Microsoft.VisualBasic;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Mazo;

public class MazoUno : IMazoCartas, IMazoConDescarte, IMazoReciclable
{
    private readonly Stack<ICarta> _cartas;
    private readonly Stack<ICarta> _cartasDescartadas;
    private readonly int _cantidadDeMazos;

    public MazoUno(int cantidadDeMazos = 1)
    {
        if(cantidadDeMazos <= 0)
            throw new ArgumentOutOfRangeException(nameof(cantidadDeMazos), "Debe haber al menos un mazo");
        _cantidadDeMazos = cantidadDeMazos;
        _cartas = new Stack<ICarta>();
        _cartasDescartadas = new Stack<ICarta>();
        InicializarCarta();
        BarajarCarta();
    }

    public void InicializarCarta()
    {
        // Cada color tiene cartas del 0 al 9 y cartas especiales: Salta, Reversa, TomaDos
        _cartas.Clear();
        for (int i = 0; i < _cantidadDeMazos; i++)
        {
            foreach (var color in Enum.GetValues<ColoresUno>())
            {
                if (color == ColoresUno.Negro)
                    continue; // Los comodines se agregan después
                    
                // Carta 0 (una por color)
                _cartas.Push(new CartaUnoClasico(color, TiposUno.Numerica, 0));

                // Cartas 1-9 (dos por color)
                for (int valor = 1; valor <= 9; valor++)
                {
                    _cartas.Push(new CartaUnoClasico(color, TiposUno.Numerica, valor));
                    _cartas.Push(new CartaUnoClasico(color, TiposUno.Numerica, valor));
                }

                // Cartas especiales: Salta, Reversa, TomaDos (dos por color)
                _cartas.Push(new CartaUnoClasico(color, TiposUno.Salta));
                _cartas.Push(new CartaUnoClasico(color, TiposUno.Salta));
                _cartas.Push(new CartaUnoClasico(color, TiposUno.Reversa));
                _cartas.Push(new CartaUnoClasico(color, TiposUno.Reversa));
                _cartas.Push(new CartaUnoClasico(color, TiposUno.TomaDos));
                _cartas.Push(new CartaUnoClasico(color, TiposUno.TomaDos));
            }
            // Cartas comodín: Comodín, Comodín Toma Cuatro (4 de cada una)
            for (int j = 0; j < 4; j++)
            {
                _cartas.Push(new CartaUnoClasico(ColoresUno.Negro, TiposUno.Comodin));
                _cartas.Push(new CartaUnoClasico(ColoresUno.Negro, TiposUno.ComodinTomaCuatro));
            }
        }
    }
    // BarajarCarta funciona con el mazo inicial y el descarte
    public void BarajarCarta() => BarajarCarta(_cartas);
    // Sobrecarga que permite pasar el mazo que se quiera barajar (_cartas o _cartasDescartadas)
    public void BarajarCarta(Stack<ICarta> mazo)
    {
        var lista = new List<ICarta>(mazo);
        var random = new Random();
        mazo.Clear();

        while(lista.Count > 0)
        {
            int index = random.Next(lista.Count);
            mazo.Push(lista[index]);
            lista.RemoveAt(index);
        }
    }

    public int CartasDescartadas()
    {
        return _cartasDescartadas.Count;
    }

    public int CartasRestantes()
    {
        return _cartas.Count;
    }

    public void DescartarCarta(ICarta carta)
    {
        if (carta == null) throw new ArgumentNullException(nameof(carta));
        _cartasDescartadas.Push(carta);
    }

    public void ReciclarDescarte()
    {
        // Si ya hay cartas en el mazo no es necesario reciclar
        if (_cartas.Count > 0) return;

        // No hay suficientes descartes para reciclar (se debe dejar la última descartada como tope)
        if (_cartasDescartadas.Count <= 1) return;

        // Guardar la última carta descartada (tope) y tomar el resto para mezclar
        var ultima = _cartasDescartadas.Pop();
        var paraReciclar = new List<ICarta>(_cartasDescartadas);

        // Vaciar el descarte y dejar sólo la última
        _cartasDescartadas.Clear();
        _cartasDescartadas.Push(ultima);

        // Barajar lista (Fisher–Yates)
        var rng = new Random();
        for (int i = paraReciclar.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            var tmp = paraReciclar[j];
            paraReciclar[j] = paraReciclar[i];
            paraReciclar[i] = tmp;
        }

        // Pasar al mazo principal
        foreach (var c in paraReciclar)
            _cartas.Push(c);
    }

    public ICarta SacarCarta()
    {
        if (_cartas.Count == 0)
            ReciclarDescarte();

        if (_cartas.Count == 0)
            throw new InvalidOperationException("No hay cartas disponibles para sacar.");

        return _cartas.Pop();
    }
}
