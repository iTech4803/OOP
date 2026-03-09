using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishDraughts.ConsoleApp.Models;

public class Move// Die Klasse Move beschreibt einen einzelnen Spielzug.Stein bewegt sich von Position (6,0) nach (5,0).       
{
    public Position From {get;} // From speichert die Startposition des Steins.Position ist eine eigene Klasse mit zwei Werten: Row (Zeile) und Col (Spalte).
                                   // { get; } bedeutet: Der Wert kann gelesen werden,aber nur im Konstruktor gesetzt werden.
    public Position To {get;}   // To speichert die Zielposition des Steins. Das ist das Feld, auf das der Stein gezogen wird.
    public bool IsCapture {get;}    // IsCapture speichert, ob der Zug ein Schlagzug ist.True = geschlagen, False=normaler Zug

    public Move(Position from, Position to, bool isCapture = false) // Konstruktor der Klasse Move.
    {
        From = from;        // Die übergebene Startposition wird gespeichert.
        To = to;           // Die übergebene Zielposition wird gespeichert.
        IsCapture = isCapture;  // Hier wird gespeichert, ob der Zug ein Schlagzug ist.Wenn nichts angegeben wird, bleibt der Wert automatisch "false".

    }
}