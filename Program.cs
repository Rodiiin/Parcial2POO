using Parcial2POO.Interfaces;
using Parcial2POO.Simulacion;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Selecciona el juego de mesa a simular:");
        Console.WriteLine("1. BlackJack");
        Console.WriteLine("2. UNO");

        Console.Write("Ingresa el número del juego: ");
        string opcion = Console.ReadLine(); 
        string tipoJuego;

        switch (opcion)
        {
            case "1":
                tipoJuego = "blackjack";
                break;
            case "2":
                tipoJuego = "uno";
                break;
            default:
                Console.WriteLine("Opción no válida.");
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
            Console.WriteLine($"Error al iniciar el juego: '{ex.Message}'.");
        }
    }
}