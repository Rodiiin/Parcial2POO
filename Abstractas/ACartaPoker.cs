using System;

using Parcial2POO.Interfaces;



namespace Parcial2POO.Abstractas;

public enum Colores
{
    Rojo,
    Negro
}

public enum Figura
{
    Espada,
    Corazon,
    Diamante,
    Trebol
}

public enum TipoCarta
{
    Numerica,
    Jack,
    Queen,
    King,
    As
}
public abstract class ACartaPoker : ICartaPoker
{
    //Atributos
    public Colores Color{ get; protected set; }
    public Figura Figura{ get; protected set; }
    public TipoCarta TipoCarta { get; protected set; }

    private int _valorNumerico;
    public int ValorNumerico
    {
        get { return _valorNumerico; }
        set
        {
            if (TipoCarta == TipoCarta.Numerica && value >= 2 && value <= 10)
            {
                _valorNumerico = value;
            }
            else
            {
                throw new ArgumentException("Solo las cartas numéricas pueden tener valor entre 2 y 10.");
            }
        }
    }

    //Constructor 

    protected ACartaPoker(Colores color, Figura figura, TipoCarta tipoCarta, int? valorNumerico = null)
    {
        this.Color = color;
        this.Figura = figura;
        this.TipoCarta = tipoCarta;

        if (tipoCarta == TipoCarta.Numerica)
        {
            if (valorNumerico is null)
                throw new ArgumentException("Las cartas numéricas deben tener un valor entre 2 y 10.");

            ValorNumerico = valorNumerico.Value;
        }
        else if (valorNumerico is not null)
        {
            throw new ArgumentException("Solo las cartas numéricas pueden tener valor asignado.");
        }
    
    }


    //Métodos
    public override string ToString()
    {
        string descripcion = "";

        if (TipoCarta == TipoCarta.Numerica)
        {
            descripcion = $"{ValorNumerico} de {Figura} ({Color})";
        }
        else
        {
            descripcion = $"{TipoCarta} de {Figura} ({Color})";
        }
        return descripcion;
    }
    
    


}
