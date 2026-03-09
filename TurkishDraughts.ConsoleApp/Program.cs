
using TurkishDraughts.ConsoleApp.Game;
namespace TurkishDraughts.ConsoleApp;
internal class Program
{

    static void Main(string[] args)
    {
        // 1) Wir erstellen ein neues Spielfeld.
        // new Board() erstellt ein Board-Objekt.
        Board board = new Board();

        // 2) Die Startaufstellung wird gesetzt.
        // Dadurch werden die weißen und schwarzen Steine auf dem Brett platziert.
        board.SetupInitialPosition();

        // 3) Wir erstellen die GameEngine.
        // Die GameEngine steuert den gesamten Spielablauf.
        // Das Board wird dabei an die Engine übergeben.
        GameEngine engine = new GameEngine(board);

        // 4) Das Spiel wird gestartet.
        // Die Run()-Methode startet die Spielschleife.
        // Dort werden Züge eingegeben, geprüft und ausgeführt.
        engine.Run();
    }
}