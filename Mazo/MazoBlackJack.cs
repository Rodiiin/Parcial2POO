using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Mazo;

public class MazoBlackJack : IMazoCartas, IMazoConDescarte, IMazoReciclable
{
    private readonly Stack<ICarta> _cartas;
    private readonly List<ICarta> _cartasDescartadas;
    private readonly int _cantidadDeMazos;


    public MazoBlackJack(int cantidadDeMazos = 6)
    {
        if (cantidadDeMazos <= 0)
            throw new ArgumentOutOfRangeException(nameof(cantidadDeMazos), "Debe haber al menos un mazo");
        
        _cantidadDeMazos = cantidadDeMazos;
        _cartas = new Stack<ICarta>();
        _cartasDescartadas = new List<ICarta>();
        InicializarCarta();
        BarajarCarta();

    }
    public void InicializarCarta()
    {
        //Cada palo tiene 13 cartas:
        _cartas.Clear();

        for (int i = 0; i < _cantidadDeMazos; i++)
        {
            foreach (var figura in Enum.GetValues<Figura>())
            {
                Colores color = figura == Figura.Corazon || figura == Figura.Diamante
                                ? Colores.Rojo
                                : Colores.Negro;

                // 9 numéricas (2–10)
                for (int valor = 2; valor <= 10; valor++)
                {
                    _cartas.Push(new CartaBlackJack(color, figura, TipoCarta.Numerica, valor));
                }

                // 4 con figuras: Jack (J), Queen (Q), King (K), Ace (A)
                _cartas.Push(new CartaBlackJack(color, figura, TipoCarta.Jack));
                _cartas.Push(new CartaBlackJack(color, figura, TipoCarta.Queen));
                _cartas.Push(new CartaBlackJack(color, figura, TipoCarta.King));
                _cartas.Push(new CartaBlackJack(color, figura, TipoCarta.As));

                //Haciendo un total de 4 + 9 = 13 cartas por palo.
            }
        }  
    }

    public void BarajarCarta()
    {
        var lista = new List<ICarta>(_cartas);
        var random = new Random();
        _cartas.Clear();  

        while (lista.Count > 0)
        {
            int index = random.Next(lista.Count);
            _cartas.Push(lista[index]);
            lista.RemoveAt(index);
        }
    }

    public ICarta SacarCarta()
    {
        if (_cartas.Count == 0)
        {
            if (_cartasDescartadas.Count == 0)
                throw new InvalidOperationException("No hay más cartas en el mazo ni en el descarte.");

            ReciclarDescarte();
        }

        return _cartas.Pop();
    }

    public void DescartarCarta(ICarta carta)
    {
        if (carta == null)
            throw new ArgumentNullException(nameof(carta), "La carta no puede ser nula.");

        _cartasDescartadas.Add(carta);
    }

    public void ReciclarDescarte()
    {
        foreach (var carta in _cartasDescartadas)
        {
            _cartas.Push(carta);
        }

        _cartasDescartadas.Clear();
        BarajarCarta();
    }

    public int CartasRestantes()
    {
        return _cartas.Count;
    }

    public int CartasDescartadas()
    {
        return _cartasDescartadas.Count;
    }



}
