using System;
using Parcial2POO.Cartas;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Interfaces;

public interface ICartaPoker : ICarta
{
    Colores Color { get; }
    Figura Figura { get; }
    TipoCarta TipoCarta { get; }
}
