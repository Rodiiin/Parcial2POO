using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Interfaces;

public interface IMazoCartas
{
    void InicializarCarta();
    void BarajarCarta();
    ICarta SacarCarta();
    int CartasRestantes();
}
