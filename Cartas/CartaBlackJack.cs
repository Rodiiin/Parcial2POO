using System;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Cartas;

public class CartaBlackJack : ACartaPoker
{
    //Atributos
    public int Puntos
    {
        get
        {
            if (TipoCarta == TipoCarta.Numerica)
            {
                return ValorNumerico;
            }
            else if (TipoCarta == TipoCarta.Jack || TipoCarta == TipoCarta.Queen || TipoCarta == TipoCarta.King)
            {
                return 10;
            }
            else
            {
                throw new InvalidOperationException("Tipo de carta no válido para Blackjack.");
            }

        
        }
    }
    //Constructores
    public CartaBlackJack(Colores color, Figura figura, TipoCarta tipoCarta, int? valorNumerico = null )
    :base(color, figura, tipoCarta, valorNumerico)
    {
        // Validación adicional para que no paso un valor numerico si la carta será de tipo no numerico. 
        if (tipoCarta != TipoCarta.Numerica && valorNumerico is not null)
        {
            throw new ArgumentException("Las figuras no deben tener valor numérico en Blackjack.");
        }
    }
}
