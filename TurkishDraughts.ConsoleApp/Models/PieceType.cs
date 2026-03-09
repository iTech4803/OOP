using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishDraughts.ConsoleApp.Models
{
    public enum PieceType     // enum = "Enumeration" = eine feste Liste von möglichen Werten.Hier wird festgelegt, welche Arten von Spielsteinen es geben kann.

    {
        Man,// Man = normaler Spielstein.Jeder Stein startet am Anfang des Spiels als "Man".
        King // King = Dame. Ein Stein wird zum King, wenn er die gegnerische Grundreihe erreicht.Eine Dame hat später mehr Bewegungsmöglichkeiten.
    }
}