using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkishDraughts.ConsoleApp.Models; // Damit wir Piece und PieceColor kennen


namespace TurkishDraughts.ConsoleApp.Game;        // Wir sortieren den Code in den Bereich "Game" (Spielteil)
                                                  //ConsoleRenderer ist nur da, um das Spielfeld schön in der Konsole zu zeichnen. 
public static class ConsoleRenderer             // static = nur Hilfsfunktionen, keine Objekte nötig;Man muss kein Objekt erstellen
                                                // wie new ConsoleRenderer()sondern kann direkt schreiben: ConsoleRenderer.Draw(board);
{
    public static void Draw(Board board)        // Zeichnet das Brett in die Konsole; public = andere Dateien dürfen darauf zugreifen
                                                 //Du gibst dieser Funktion ein Brett (board) mit.
                                                 //Die Funktion zeichnet genau das Brett, das du übergibst.


    {   
        Console.Clear();                    //Löscht alles, was vorher im Fenster stand;
                                            //Damit wird das Konsolenfenster komplett leer gemacht, bevor neu gezeichnet wird.

        Console.WriteLine("Türkische Dame");        // Titel 
        Console.WriteLine("W = Weiß, B = Schwarz, . = leer");            // Legende: welche Zeichen was bedeuten
        Console.WriteLine();                         //  Leerzeile für bessere Lesbarkeit


        // ====== Spaltennummern oben ausgeben (0 bis 7) ======
        Console.Write("   ");                   // Abstand links für Zeilennummern;3 Leerzeichen, damit die Zahlen
                                                // oben richtig unter den Spalten stehen, weil links ja noch die Zeilennummern kommen.

        for (int col = 0; col < Board.Size; col++)  // col läuft von 0 bis 7 (Board.Size ist 8)
        {
            Console.Write(col + " ");       // Spaltennummer ausgeben(ohne neue Zeile);Board.Size ist 8 → also 0..7.
        }       
        Console.WriteLine();            // Console.Write schreibt ohne Zeilenumbruch.
                                        //Ergebnis: Oben steht: 0 1 2 3 4 5 6 7

        // ====== Jetzt das Brett Zeile für Zeile ausgeben ======
        for (int row = 0; row < Board.Size; row++)           // row läuft von 0 bis 7;
        {
            Console.Write(row + "  ");              // Links wird die Zeilennummer geschrieben (damit man Koordinaten erkennt).

            for (int col = 0; col < Board.Size; col++)           // Jetzt läuft die innere Schleife über die Spalten 0..7;
                                                                 // in dieser Zeile jede Spalte durchgehen
            {
                Piece? piece = board.GetPiece(row, col);            // Wir fragen das Brett:
                                                                    // Ergebnis ist entweder ein Stein (Piece) oder null (= leer).
                                                                    // Piece? bedeutet: “kann auch null sein”.
                                                                    // Liegt hier ein Stein? (oder ist das Feld leer = null)

                char symbol = '.';          //Standard: Wenn nix da ist, zeigen wir '.' (leeres Feld)

                if (piece != null)           // Wenn piece NICHT null ist, liegt dort ein Stein
                {
                    symbol = (piece.Color == PieceColor.White) ? 'W' : 'B';   // Ist der Stein weiß? Dann 'W', sonst 'B'
                }

                Console.Write(symbol + " ");        // Zeichen ausgeben plus Leerzeichen, damit es wie ein Raster aussieht
                                                    // Symbol drucken + Leerzeichen, damit es wie ein Raster aussieht
            }

            Console.WriteLine();         // Danach geht es in die nächste Zeile, sonst wäre alles in einer Zeile.
        }

        Console.WriteLine();                 // Leerzeile
        Console.WriteLine("Enter drücken zum Beenden...");          // Hinweis
    }
}