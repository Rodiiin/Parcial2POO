using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;
using Parcial2POO.Interfaces;
using Parcial2POO.Roles;

namespace Parcial2POO.Reglas.ReglasBlackJack;

public class ReglasBlackJack : IReglasJuegoCompetitivoBlackJack
{

  private readonly ICalculadorDePuntosBlackJack _calculador;

  public ReglasBlackJack(ICalculadorDePuntosBlackJack calculador)
  {
    _calculador = calculador;
  }

  public bool TieneBlackJack(IJugadorBlackJack jugador)
  {
    return _calculador.TieneBlackjack(jugador.ObtenerMano());
  }

  public bool SePaso(IJugadorBlackJack jugador)
  {
    return _calculador.SePaso(jugador.ObtenerMano());
  }


  public bool HaGanado(IJugadorBlackJack jugador, IJugadorBlackJack dealer)
  {
    if (SePaso(jugador)) return false;
    else if (SePaso(dealer)) return true;
    else if (TieneBlackJack(jugador) && !TieneBlackJack(dealer)) return true;

    return jugador.ObtenerPuntos() > dealer.ObtenerPuntos();
  }


  public bool HaPerdido(IJugadorBlackJack jugador, IJugadorBlackJack dealer)
  {
    if (SePaso(jugador)) return true;
    if (!SePaso(dealer) && dealer.ObtenerPuntos() > jugador.ObtenerPuntos()) return true;
    return false;
  }

  public bool EsEmpate(IJugadorBlackJack jugador, IJugadorBlackJack dealer)
  {
    if (!SePaso(jugador) && !SePaso(dealer) && jugador.ObtenerPuntos() == dealer.ObtenerPuntos())
    {
      return true;
    }
    return false;
  }


  

}
  
    
    
    

  
    

