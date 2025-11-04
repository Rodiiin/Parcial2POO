
using Parcial2POO.Interfaces;
using Parcial2POO.Simulacion;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Selecciona el juego de mesa a simular:");
        Console.WriteLine("1. BlackJack");
        // Console.WriteLine("2. Uno");

        Console.Write("Ingresa el número del juego: ");
        string opcion = Console.ReadLine().Trim(); //para que sirve trim?

        string tipoJuego = opcion switch
        {
            "1" => "blackjack",
            //"2" => "uno"
            _ => "desconocido"
        };

        if (tipoJuego == "desconocido")
        {
            Console.WriteLine("Opción invalida");
            return;
        }

        try
        {
            IJuegoCartas juego = FabricaDeJuegos.CrearJuego(tipoJuego);
            var simulador = new SimuladorDeJuegos(juego);
            simulador.Ejecutar();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al iniciar el juego {ex.Message} ");
        }
    }
}