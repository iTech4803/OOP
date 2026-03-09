using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishDraughts.ConsoleApp.Models
{
    public enum PieceColor    // enum = "Enumeration"= feste Liste von erlaubten Werten
                              // Dadurch können wir nur bestimmte Werte verwenden. In diesem Fall: nur zwei Farben für Spielsteine.
    {
        White,        // Weißer Spieler / weißer Spielstein
        Black        // Schwarzer Spieler / schwarzer Spielstein

    }
}