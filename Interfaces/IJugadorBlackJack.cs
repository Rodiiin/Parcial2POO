using System;
using Parcial2POO.Cartas;

namespace Parcial2POO.Interfaces;

public interface IJugadorBlackJack : IJugador,
                                    IJugadorConMano,
                                    IJugadorQuePuedePedirCarta,
                                    IJugadorConPuntaje,
                                    IJugadorLimpiarMano
{

}
