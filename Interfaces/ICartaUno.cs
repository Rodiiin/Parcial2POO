using System;
using Parcial2POO.Cartas;
using Parcial2POO.Abstractas;

namespace Parcial2POO.Interfaces;

public interface ICartaUno: ICarta
{
    ColoresUno Color { get; }
    TiposUno Tipo { get; }
}
