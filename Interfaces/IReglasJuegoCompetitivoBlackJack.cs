using System;
using Parcial2POO.Roles;

namespace Parcial2POO.Interfaces;

public interface IReglasJuegoCompetitivoBlackJack : 
    IReglasJuegoCompetitivo<JugadorBlackJack>,
    IReglasJuegoConEmpate<JugadorBlackJack>
{
    bool SePaso(JugadorBlackJack jugador); //Esto solo aplica a BlackJack
    bool TieneBlackJack(JugadorBlackJack jugador);
}
