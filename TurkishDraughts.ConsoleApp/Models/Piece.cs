using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishDraughts.ConsoleApp.Models
{
    public class Piece     // Die Klasse Piece beschreibt einen einzelnen Spielstein mit zwei Eigenschaften:
                           // Farbe w/s; Typ normal oder Dame/King
    {
        public PieceColor Color {get;}      // Color speichert die Farbe des Spielsteins. PieceColor ist ein Enum (z.B. White oder Black).
        public PieceType Type {get; set;}   // Type speichert den Typ des Steins.PieceType ist ebenfalls ein Enum (Man oder King).
                                              // - get  -> Wert kann gelesen werden
                                              // - set  -> Wert kann verändert werden
                                              // wichtig, weil ein normaler Stein später zur Dame werden kann.
        public Piece(PieceColor color)   // Konstruktor der Klasse Piece.Automatisch ausfegührt,
                                         // wenn ein neuer Spielstein erstellt wird.

        {
            Color = color; //Farbe
            Type = PieceType.Man;       // Jeder Stein startet als normaler Stein (Man).

        }
    }
}