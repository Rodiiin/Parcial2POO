using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Interfaces;

public interface IMazoConDescarte
{
    void DescartarCarta(ICarta carta);
    int CartasDescartadas();
}
