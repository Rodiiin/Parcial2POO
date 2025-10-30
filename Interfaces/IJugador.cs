using System;

namespace Parcial2POO.Cartas;

public interface IJugador
{
    string Nombre { get; } 
    void RecibirCarta(ICarta carta); //Para que el jugado pueda recibir carta
    List<ICarta> ObtenerMano(); //Para que el jugaodr vea su mano.
    bool DebePedirCarta(); //Decisión de pedir carta.
}
