using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TurkishDraughts.ConsoleApp.Models
{

    public class Piece                       // Klasse Spielsteine
    {
        public PieceColor Color { get; }    // Jeder Stein hat eine Farbe.
                                            //get:
                                            //Man darf die Farbe lesen, aber später nicht verändern.
                                            //Ein schwarzer Stein bleibt schwarz.

        public Piece(PieceColor color)   // Konstruktor; wird aufgerufen wenn wir einen neuen Stein erstellen.
        {
            Color = color;              // Hier speichern wir die Farbe im Stein ab.
                                        // Wenn wir sagen new Piece(PieceColor.Black), wird der Stein schwarz.
        }
    }
}
