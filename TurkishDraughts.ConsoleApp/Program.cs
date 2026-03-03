using TurkishDraughts.ConsoleApp.Game; // Wir brauchen Board und ConsoleRenderer

namespace TurkishDraughts.ConsoleApp; // Projekt-Namespace

internal class Program           // Hauptklasse
{
    static void Main(string[] args)             // Startpunkt des Programms
    {
        Board board = new Board();          // Neues Brett erstellen
        board.SetupInitialPosition();            // Startaufstellung setzen

        ConsoleRenderer.Draw(board);            // Brett zeichnen

        Console.ReadLine();             // Warten, damit Fenster offen bleibt
    }
}