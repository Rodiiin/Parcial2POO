using System;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Cartas;

public class CartaUnoClasico : ACartaUno
{
    public int Valor { get; set; }

    //Constructor
    public CartaUnoClasico(ColoresUno color, TiposUno tipo, int? valorNumerico = null)
    :base(color, tipo, valorNumerico)
    {
        // Validación adicional para que no paso un valor numerico si la carta será de tipo no numerico. 
        if (tipo != TiposUno.Numerica && valorNumerico is not null)
        {
            throw new ArgumentException("Las figuras no deben tener valor numérico en Uno.");
        }
    }
}
