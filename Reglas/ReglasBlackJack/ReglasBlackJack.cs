using System;
using Parcial2POO.Abstractas;
using Parcial2POO.Cartas;
using Parcial2POO.Roles;

namespace Parcial2POO.Reglas.ReglasBlackJack;

public class ReglasBlackJack : AReglasJuegoCompetitivo<JugadorBlackJack>
{

  private readonly ICalculadorDePuntosBlackJack _calculador;

  public ReglasBlackJack(ICalculadorDePuntosBlackJack calculador)
  {
    _calculador = calculador;
  }

  public bool TieneBlackjack(JugadorBlackJack jugador)
  {
    return _calculador.TieneBlackjack(jugador.ObtenerMano());
  }

  public bool SePaso(JugadorBlackJack jugador)
  {
    return _calculador.SePaso(jugador.ObtenerMano());
  }


  public override bool HaGanado(JugadorBlackJack jugador, JugadorBlackJack dealer)
  {
    if (SePaso(jugador)) return false;
    else if (SePaso(dealer)) return true;
    else if (TieneBlackjack(jugador) && !TieneBlackjack(dealer)) return true;

    return jugador.ObtenerPuntos() > dealer.ObtenerPuntos();
  }


  public override bool HaPerdido(JugadorBlackJack jugador, JugadorBlackJack dealer)
  {
    if (SePaso(jugador)) return true;
    if (!SePaso(dealer) && dealer.ObtenerPuntos() > jugador.ObtenerPuntos()) return true;
    return false;
  }

  public override bool EsEmpate(JugadorBlackJack jugador, JugadorBlackJack dealer)
  {
    if (!SePaso(jugador) && !SePaso(dealer) && jugador.ObtenerPuntos() == dealer.ObtenerPuntos())
    {
      return true;
    }
    return false;
  }


  

}
  
    
    
    

  
    

