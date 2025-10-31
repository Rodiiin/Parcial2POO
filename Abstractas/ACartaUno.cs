using System;
using Parcial2POO.Interfaces;

namespace Parcial2POO.Abstractas;

public enum ColoresUno
{
    Rojo,
    Azul,
    Verde,
    Amarillo
}

public enum TiposUno
{
    Numerica,
    Salta,
    Reversa,
    TomaDos,
    Comodin,
    ComodinTomaCuatro
}
public class ACartaUno : ICartaUno
{
    // Atributos
    public ColoresUno Color { get; protected set; }
    public TiposUno Tipo { get; protected set; }

    public int _valorNumerico;
    public int ValorNumerico
    {
        get { return _valorNumerico; }
        set
        {
            if (Tipo == TiposUno.Numerica && value >= 0 && value <= 9)
            {
                _valorNumerico = value;
            }
            else
            {
                throw new ArgumentException("Solo las cartas numéricas pueden tener valor entre 0 y 9.");
            }
        }
    }

    // Constructor
    protected ACartaUno(ColoresUno color, TiposUno tipo, int? valorNumerico = null)
    {
        this.Color = color;
        this.Tipo = tipo;

        if (tipo == TiposUno.Numerica && valorNumerico.HasValue)
        {
            this.ValorNumerico = valorNumerico.Value;
        }
    }

    public override string ToString()
    {
        if (Tipo == TiposUno.Numerica)
        {
            return $"{ValorNumerico} de {Color}";
        }
        else
        {
            return $"{Tipo} de {Color})";
        }
    }
}
