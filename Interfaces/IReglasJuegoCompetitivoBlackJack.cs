using System;
using Parcial2POO.Roles;

namespace Parcial2POO.Interfaces;

public interface IReglasJuegoCompetitivoBlackJack : 
    IReglasJuegoCompetitivo<IJugadorBlackJack>,
    IReglasJuegoConEmpate<IJugadorBlackJack>
{
    bool SePaso(IJugadorBlackJack jugador); //Esto solo aplica a BlackJack
    bool TieneBlackJack(IJugadorBlackJack jugador);
}
