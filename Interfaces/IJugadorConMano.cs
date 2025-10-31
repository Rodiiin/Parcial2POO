using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Interfaces;

public interface IJugadorConMano
{
    void RecibirCarta(ICarta carta); //Para recibir su mazo inicial de cartas
    List<ICarta> ObtenerMano();     // Para ver su mazo
}
