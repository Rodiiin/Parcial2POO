using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Interfaces;

public interface IMazoCartas
{
    ICarta SacarCarta();
    int CartasRestantes();
}
