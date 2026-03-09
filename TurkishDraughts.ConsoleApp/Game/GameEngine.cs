using System;
using TurkishDraughts.ConsoleApp.Models;
namespace TurkishDraughts.ConsoleApp.Game;

// GameEngine ist die „Steuerzentrale“ des Spiels.
// Sie verbindet: Anzeige (ConsoleRenderer) + Regeln (Rules) + Spielfeld (Board)
public class GameEngine 
{
    private readonly Board _board;// _board speichert das Spielfeld (wo stehen die Steine?);
                                  // _board zeigt immer auf dasselbe Board-Objekt
    private PieceColor _currentPlayer; //aktueller Spieler Weiss oder Schwarz 

    // Konstruktor: wird einmal ausgeführt, wenn du new GameEngine(...) machst
    public GameEngine(Board board)
    {
        _board = board;        // Wir speichern das übergebene Board in _board, damit wir im Spiel damit arbeiten können.
        _currentPlayer = PieceColor.White;     // Startspieler festlegen: Weiß beginnt
    }

    public void Run()   // Run() startet die eigentliche Spielschleife.
    {
        // Endlosschleife: läuft bis wir "break" machen (z.B. bei 'q' oder bei Spielende)
        while (true)         // Solange while(true) läuft, läuft das Spiel.
        {
            // 1) Brett zeichnen (aktueller Stand wird angezeigt)
            ConsoleRenderer.Draw(_board);
            Console.WriteLine();

            // 3) Anzeigen, welcher Spieler dran ist. Wenn _currentPlayer White ist → Text „Weiß (W)“, sonst „Schwarz (B)“
            Console.WriteLine($"Aktueller Zug : {(_currentPlayer == PieceColor.White ? "Weiß (W)" : "Schwarz (B)")}");
            Console.WriteLine("Eingabe: StarteReihe StartSpalte ZielReihe ZielSpalte");
            Console.WriteLine("Beispiel: 6 0 5 0 (wichtig: Leerzeichen zwischen den Zahlen!)"); //Beispiel für Eingabe 
            Console.WriteLine("Beenden: q");// Hinweis, wie man das Spiel beendet
            Console.Write("--> ");            // Prompt-Zeichen (damit man sieht: jetzt soll ich tippen)


            // 5) Eingabe lesen
            // Console.ReadLine() liest eine komplette Zeile aus der Konsole
            string? input = Console.ReadLine();

            if (input == null)            // Wenn input null ist, dann starten wir die Schleife neu

                continue;
                input = input.Trim();     // Wir entfernen Leerzeichen am Anfang/Ende (z.B. wenn jemand " 6 0 5 0 " schreibt)


            // 6) Abbruch, wenn der Nutzer q eingibt (Groß/Kleinschreibung egal)
            if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
                break;

            // 7) Eingabe in Teile splitten, getrennt durch Leerzeichen-----------*****mit Hilfe KI 
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);            // RemoveEmptyEntries sorgt dafür, dass doppelte Leerzeichen nicht stören

            // Wir erwarten genau 4 Werte (StartRow, StartCol, ZielRow, ZielCol)
            if (parts.Length != 4)
            {
                ShowError("Bitte genau 4 Zahlen eingeben, z.B. 6 0 5 0."); // Wenn nicht 4 Werte, Fehlermeldung zeigen und neue Eingabe verlangen
                continue;
            }

            // 8) 4 Texte in Zahlen umzuwandeln
            if (!int.TryParse(parts[0], out int fromRow) ||
                !int.TryParse(parts[1], out int fromCol) ||
                !int.TryParse(parts[2], out int toRow) ||
                !int.TryParse(parts[3], out int toCol))
            {
                // Wenn irgendeine Zahl nicht funktioniert, Fehler ausgeben
                ShowError("Ungültige Eingabe: Bitte nur Zahlen benutzen!");
                continue;
            }
            // 9) Aus den Zahlen bauen wir Position-Objekt
            Position from = new Position(fromRow, fromCol);
            Position to = new Position(toRow, toCol);

            // 10) Jetzt prüfen wir: ist dieser Zug ein Schlag?
            // - 2 Felder weit horizontal oder vertikal (nicht diagonal)
            int dRow = to.Row - from.Row; // Zeilen-Differenz
            int dCol = to.Col - from.Col; // Spalten-Differenz

            // isCapture = true, wenn genau 2 Felder weit in einer Richtung
            bool isCapture =
                (Math.Abs(dRow) == 2 && dCol == 0) ||   // 2 Felder vertikal
                (Math.Abs(dCol) == 2 && dRow == 0);     // 2 Felder horizontal

            // 11) Move-Objekt erstellen:
            // Es enthält Start, Ziel und ob es ein Schlag ist.
            Move move = new Move(from, to, isCapture);

            // 12) Pflichtschlagen prüfen:
            bool mustCapture = Rules.PlayerHasAnyCapture(_board, _currentPlayer);

            // Wenn ein Schlag möglich ist, aber der Spieler einen normalen Zug machen will:
            if (mustCapture && !isCapture)
            {
                ShowError("Du MUSST schlagen, weil ein Schlag möglich ist. (Pflichtschlagen)");
                continue;
            }

            // 13) Jetzt prüfen wir den Zug mit den Regeln
            // Je nachdem ob es ein Schlag ist oder nicht, nutzen wir eine andere Regelprüfung.
            if (isCapture)
            {
                // Schlagzug prüfen
                if (!Rules.IsValidCaptureMove(_board, move, _currentPlayer, out string error))
                {
                    ShowError(error);
                    continue;
                }
            }
            else
            {
                // Normalzug prüfen
                if (!Rules.IsValidSimpleMove(_board, move, _currentPlayer, out string error))
                {
                    ShowError(error);
                    continue;
                }
            }

            // 14) Wenn die Regeln OK sagen, führen wir den Zug aus:
            // Das Board verschiebt den Stein und entfernt ggf. den geschlagenen Stein.
            _board.ApplyMove(move);

            // 15) Spielende prüfen: Hat einer keine Steine mehr?
            // Wenn Weiß keine Steine mehr hat, gewinnt Schwarz.
            if (!_board.HasPieces(PieceColor.White))
            {
                ConsoleRenderer.Draw(_board);
                Console.WriteLine("Schwarz hat gewonnen!");
                Console.ReadLine();
                break;
            }

            // Wenn Schwarz keine Steine mehr hat, gewinnt Weiß.
            if (!_board.HasPieces(PieceColor.Black))
            {
                ConsoleRenderer.Draw(_board);
                Console.WriteLine("Weiß hat gewonnen!");
                Console.ReadLine();
                break;
            }

            // 16) Mehrfachschlag:
            // Wenn es ein Schlag war, prüfen wir ob derselbe Stein direkt nochmal schlagen kann.
            // Wenn ja, muss der Spieler weiter schlagen → Spielerwechsel passiert dann NICHT.
            if (move.IsCapture)
            {
                // Prüfen, ob von der neuen Position noch ein Schlag möglich ist
                if (Rules.HasAnotherCapture(_board, move.To, _currentPlayer))
                {
                    Console.WriteLine("Du musst weiter schlagen! (Mehrfachschlag)");
                    Console.ReadLine();

                    // continue = wir springen an den Anfang der while-Schleife
                    // Der aktuelle Spieler bleibt dran.
                    continue;
                }
            }

            // 17) Spieler wechseln:
            // Wenn Weiß dran war → jetzt Schwarz, sonst umgekehrt
            _currentPlayer = (_currentPlayer == PieceColor.White)
                ? PieceColor.Black
                : PieceColor.White;
        }
    }

    // Diese Methode zeigt eine Fehlermeldung an und wartet auf Enter.
    // static bedeutet: Sie braucht keine Daten aus der Klasse (nur Text anzeigen).
    private static void ShowError(string message)
    {
        Console.WriteLine();
        Console.WriteLine("FEHLER: " + message);
        Console.WriteLine("Enter drücken...");
        Console.ReadLine();        // Warten, bis der Benutzer Enter drückt

    }
}

