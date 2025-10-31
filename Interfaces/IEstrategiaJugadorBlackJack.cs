using System;

namespace Parcial2POO.Cartas;

public interface IEstrategiaJugadorBlackJack
{    
    //representa una lógica externa que decide si se debe pedir carta, basada en la mano.
    bool DebePedirCarta(List<ICarta> mano);   

}
