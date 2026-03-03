using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


using TurkishDraughts.ConsoleApp.Models;             // Damit Piece und PieceColor benutzt werden können

namespace TurkishDraughts.ConsoleApp.Game;          //Alles was zur Spiellogik gehört, kommt in "Game"

public class Board                          // Klasse Spielfeld
{
    public const int Size = 8;                  // const bedeutet: Diese Zahl ändert sich nie.
                                                //  Brett ist immer 8 Felder breit und 8 Felder hoch

    private readonly Piece?[,] _cells = new Piece?[Size, Size]; //Piece=Spielstein; Piece?=Ein Spielstein oder null
                                                                //:Es kann ein Spielstein dort stehen – oder nichts(null).
                                                                //[,] bedeutet: zweidimensionales Array.Also: Ein Raster wie ein Schachbrett.
                                                                //_cells = new Piece?[Size, Size]; = erstelle eine Tabelle mit 8x8 Feldern
     //private Weil niemand von außen direkt an _cells zugreifen und manipulieren soll ->Nur über unsere Methoden GetPiece / SetPiece.
    //readonly= Weil _cells immer dieselbe Tabelle bleiben soll.Wir ändern Inhalt, aber nicht die Tabelle selbst.
    public Piece? GetPiece(int row, int col)        //GetPiece = „Gib mir den Stein an Position …“ ;
                                                    //int row, int col = Zeile und Spalte
                                                    //return = gibt den Wert zurück.
                                                    //Wenn dort ein Stein ist → du bekommst Piece
                                                    //Wenn dort nichts ist → du bekommst null
    {
        return _cells[row, col];                     // Rückgabe des Inhalts Stein oder null, einfach aus der Tabelle das Feld rauslesen
    }

    public void SetPiece(int row, int col, Piece? piece) // Diese Methode setzt einen Stein auf ein Feld (oder löscht ihn mit null)

    //void:keine Rückgabe; piece= was ich setzen will;
    //Wenn du piece = null übergibst → Feld wird leer gemacht
    //Wenn du einen Stein übergibst → Stein wird gesetzt

    {
        _cells[row, col] = piece;                     // wir schreiben in die Tabelle rein: Stein oder null
    }

    public void Clear()                 // Löscht das komplette Brett,alles leeren
    {
        for (int row = 0; row < Size; row++)        // Erste Schleife läuft row = 0..7 (alle Zeilen)
                                                    // Wir gehen jede Zeile durch
        {
            for (int col = 0; col < Size; col++)        //Zweite Schleife läuft col = 0..7 (alle Spalten)
                                                        // In jeder Zeile gehen wir jede Spalte durch
            {
                _cells[row, col] = null;            // Feld wird leer gemacht (kein Stein)
            }
        }
    }

    public void SetupInitialPosition()          // Setzt die Startaufstellung: oben Schwarz, unten Weiß
    {
        Clear();                    // Erst alles löschen, dann neu aufbauen

        // Schwarz oben: Reihe 0 und 1 komplett (2 Reihen × 8 Spalten = 16 schwarze Steine)
        for (int row = 0; row <= 1; row++)              // row <= 1 bedeutet: nur Zeile 0 und 1
        {
            for (int col = 0; col < Size; col++)            //jede Spalte 0..7 bekommt einen Stein
            {
                SetPiece(row, col, new Piece(PieceColor.Black)); // new Piece(PieceColor.Black) baut einen neuen schwarzen Stein
            }
        }

        // WEISS: Reihen 6 und 7 (unten) ( 2 Reihen × 8 Spalten = 16 weiße Steine)
        for (int row = 6; row <= 7; row++)          //Zeile 6 und 7 bekommen weiße Steine,nur Zeile 6 und 7
        {       
            for (int col = 0; col < Size; col++)            // alle Spalten 0..7
            {
                SetPiece(row, col, new Piece(PieceColor.White));            // Setzt einen neuen weißen Stein auf dieses Feld
            }
        }
    }
}