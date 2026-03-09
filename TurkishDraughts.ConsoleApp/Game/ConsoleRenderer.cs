using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkishDraughts.ConsoleApp.Models; 
namespace TurkishDraughts.ConsoleApp.Game;       
public static class ConsoleRenderer             
{
    public static void Draw(Board board)        // Zeichnet das Brett in die Konsole; public = andere Dateien dürfen darauf zugreifen
                                                 //Dieser Funktion gibt man ein Brett (board) mit.
    {   
        Console.Clear();                    //Löscht alles, was vorher im Fenster stand;
                                            //Damit wird das Konsolenfenster komplett leer gemacht, bevor neu gezeichnet wird.
        Console.WriteLine("Türkische Dame Old Style");     
        Console.WriteLine("W = Weiß, B = Schwarz, . = leer");            
        Console.WriteLine();                     

        // ====== Spaltennummern oben ausgeben (0 bis 7) ======
        Console.Write("   ");                   // Abstand links für Zeilennummern;3 Leerzeichen, damit die Zahlen
                                                // oben richtig unter den Spalten stehen, weil links ja noch die Zeilennummern kommen.

        for (int col = 0; col < Board.Size; col++)  // col läuft von 0 bis 7 (Board.Size ist 8)
        {
            Console.Write(col + " ");       // Spaltennummer ausgeben(ohne neue Zeile);Board.Size ist 8 → also 0..7.
        }       
        Console.WriteLine();             //Ergebnis: Oben steht: 0 1 2 3 4 5 6 7


        // ====== Zeilennumer.Jetzt das Brett Zeile für Zeile ausgeben ======
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

        Console.WriteLine();                 
        Console.WriteLine("Enter drücken zum Beenden...");          // Hinweis
    }
}