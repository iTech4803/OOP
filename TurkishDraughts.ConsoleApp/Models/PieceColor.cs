using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishDraughts.ConsoleApp.Models
{
    public enum PieceColor   // eine feste Liste von erlaubten Werten.
                            //Wir wollen nur zwei Farben erlauben – nichts anderes.
    {
        White,               // Weißer Spieler
        Black               // Schwarzer Spieler
    }                       //Ein Spielstein darf nur Weiß oder Schwarz sein =Das verhindert Fehler wie „Rot“ oder „Blau“
}





