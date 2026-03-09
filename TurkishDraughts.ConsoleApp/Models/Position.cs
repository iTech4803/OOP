using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishDraughts.ConsoleApp.Models
{
    // Die Struktur Position beschreibt eine Koordinate auf dem Spielfeld.
    // Beispiel: Row = 6, Col = 0 bedeutet:
    // Zeile 6, Spalte 0 auf dem 8x8 Spielfeld.
    public readonly struct Position     // struct = eine Datenstruktur für kleine Datenobjekte.
                                        // readonly : Nachdem die Position erstellt wurde,können ihre Werte nicht mehr verändert werden.
    {
        public int Row {get;}        // Row speichert die Zeile des Feldes. Beispiel: Row = 6 bedeutet Zeile 6.
        public int Col {get;}        // Col speichert die Spalte des Feldes.Beispiel: Col = 0 bedeutet Spalte 0.


        // Wird automatisch aufgerufen, wenn eine neue Position erstellt wird.
        public Position(int row, int col) // Konstruktor der Struktur Position. Wird automatisch aufgerufen,
                                          // wenn eine neue Position erstellt wird.
        {
            Row = row;            // Hier wird der übergebene Wert in der Variable Row gespeichert.
            Col = col;            // Hier wird der übergebene Wert in der Variable Col gespeichert.

        }
    }
}