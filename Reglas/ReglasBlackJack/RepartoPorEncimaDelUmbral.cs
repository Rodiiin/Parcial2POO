using System;
using Parcial2POO.Interfaces;
using Parcial2POO.Roles;

namespace Parcial2POO.Reglas.ReglasBlackJack;

public class RepartoPorEncimaDelUmbral : IRepartoPorUmbralBlackJack
{
    public void ConfiguradorManoInicial(JugadorBlackJack jugador, IMazoCartas mazo)
    {
        while (jugador.DeseaOtraCarta())
        {
            if (mazo is IMazoConDescarte mazoConDescarte)
            {
                foreach (var carta in jugador.ObtenerMano())
                    mazoConDescarte.DescartarCarta(carta);
            }
    
            jugador.LimpiarMano();
            jugador.RecibirCarta(mazo.SacarCarta());
            jugador.RecibirCarta(mazo.SacarCarta());
        }
    }
}
