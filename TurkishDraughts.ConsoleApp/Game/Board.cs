using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TurkishDraughts.ConsoleApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TurkishDraughts.ConsoleApp.Game;

// Die Klasse Board speichert den Zustand des Spielfelds.
// Also: Wo stehen welche Steine? Welche Felder sind leer?
public class Board
{
    public const int Size = 8; // Size ist eine Konstante. Sie ändert sich nie,deswegen const.Das Spielfeld ist immer 8x8.

    // _cells ist das eigentliche Spielfeld:
    // Ein 2D-Array (Tabelle) mit 8 Zeilen und 8 Spalten.
    // Piece?= Es kann ein Stein dort stehen (Piece) ODER es kann leer sein (null).
    // readonly: Das Array selbst wird nicht durch ein anderes ersetzt (es bleibt dasselbe)
    private readonly Piece?[,] _cells = new Piece?[Size, Size];

    // ----------------GetPiece gibt zurück, was auf einem Feld steht.
    public Piece? GetPiece(int row, int col) // - Piece  -> wenn ein Stein da steht;   - null -> wenn das Feld leer ist
    {
        return _cells[row, col];  // Auslesen aus der Tabelle das Feld [row, col] .
    }
    // ----------------SetPiece setzt einen Stein auf ein Feld ODER macht es leer.
    public void SetPiece(int row, int col, Piece? piece)   //Wenn piece = null, wird das Feld geleert.
    {
        _cells[row, col] = piece;  // Wir schreiben in die Tabelle an Position [row, col] einen Stein oder null.
    }
    //------------------Clear leert das komplette Spielfeld.
    public void Clear()   
    {
        for (int row = 0; row < Size; row++)      // Äußere Schleife: geht alle Zeilen durch (0..7)
        
            for (int col = 0; col < Size; col++)    // Innere Schleife: geht alle Spalten durch (0..7)
                _cells[row, col] = null;                // Jedes Feld wird auf null gesetzt = Feld ist leer.
    }
    // --------------SetupInitialPosition setzt die Startaufstellung.ERstellen der Startaufstellung
    public void SetupInitialPosition()   
                                          // Am Anfang sollen oben schwarz und unten weiß stehen.
    {
        Clear();        // Erst das Brett leeren, damit wir sicher bei "0" anfangen.

        // Schwarz oben: Reihen 0 und 1
        for (int row = 0; row <= 1; row++)        // row läuft von 0 bis 1 (also genau 2 Reihen).
      
            for (int col = 0; col < Size; col++)      // col läuft von 0 bis 7 (alle Spalten).
       
            SetPiece(row, col, new Piece(PieceColor.Black));  // Wir setzen auf jedes Feld in diesen Reihen einen schwarzen Stein.    
                                                                // new Piece(...) erstellt ein neues Piece-Objekt.
        // Weiß unten: Reihen 6 und 7
        for (int row = 6; row <= 7; row++)        // row läuft von 6 bis 7 (auch genau 2 Reihen).
   
            for (int col = 0; col < Size; col++)        // wieder alle Spalten 0 bis 7
                SetPiece(row, col, new Piece(PieceColor.White));   // Wir setzen auf jedes Feld in diesen Reihen einen weißen Stein.

    }
    //----------- ApplyMove führt einen Zug wirklich aus.//***Mit Unterstürtzung KI
    public void ApplyMove(Move move)  // Stein vom Startfeld nehmen und auf das Zielfeld setzen.
    {                                 // Wenn es ein Schlagzug ist, wird zusätzlich ein gegnerischer Stein entfernt.

        Piece? piece = GetPiece(move.From.Row, move.From.Col); // Wir holen den Stein vom Startfeld.
                                                               // move.From ist die Startposition (Row/Col).
        if (piece == null) return;  // Wenn auf dem Startfeld kein Stein ist,kann man keinen Zug ausführen -> wir brechen ab.


        // 1) Startfeld leer machen. Der Stein wird dort entfernt (null bedeutet "leer").
        SetPiece(move.From.Row, move.From.Col, null);

        // 2) Wenn es ein Schlagzug ist:
        // Dann springt der Stein 2 Felder weit und in der Mitte steht der Gegner,der entfernt werden muss.
        if (move.IsCapture)
        {
            // Wir berechnen wie weit sich der Zug in Zeilen bewegt (meist +2 oder -2).
            int dRow = move.To.Row - move.From.Row;

            // Wir berechnen wie weit sich der Zug in Spalten bewegt (meist +2 oder -2).
            int dCol = move.To.Col - move.From.Col;

            // Der geschlagene Stein steht genau in der Mitte:
            // Beispiel: From (6 , 0) -> To (4 , 0)
            // dRow = -2, dRow/2 = -1
            // capturedRow = 6 + (-1) = 5 (Mitte)
            int capturedRow = move.From.Row + (dRow / 2);             //Reihe
            int capturedCol = move.From.Col + (dCol / 2);            // Gleiches Prinzip für Spalte

            SetPiece(capturedRow, capturedCol, null);            // Der Gegner in der Mitte wird entfernt (Feld wird leer gemacht).

        }

        // 3) Ziel setzen:
        SetPiece(move.To.Row, move.To.Col, piece);// Der Stein, den wir am Anfang vom Startfeld genommen haben,
                                                  // wird jetzt auf das Zielfeld gesetzt


        // 4) Umwandlung prüfen:
        PromoteIfNeeded(move.To.Row, piece);     // Wenn ein normaler Stein ganz hinten ankommt,wird er zur Dame (King).
    }

    // PromoteIfNeeded prüft, ob ein Stein zur Dame werden muss.//***Mit Unterstürtzung KI
    private void PromoteIfNeeded(int newRow, Piece? piece)   // newRow ist die neue Zeile, wo der Stein nach dem Zug steht.
    {
        // Wenn piece null ist, gibt es nichts zu prüfen.
        if (piece == null) return;

        // Wenn der Stein schon eine Dame ist, muss man ihn nicht nochmal umwandeln.
        if (piece.Type != PieceType.Man) return;

        // Weiß wird Dame, wenn er oben ankommt (Zeile 0).
        if (piece.Color == PieceColor.White && newRow == 0)
            piece.Type = PieceType.King;

        // Schwarz wird Dame, wenn er unten ankommt (Zeile 7).
        if (piece.Color == PieceColor.Black && newRow == 7)
            piece.Type = PieceType.King;
    }

    // HasPieces prüft, ob ein Spieler noch mindestens einen Stein hat.
    // Wird benutzt, um das Spielende festzustellen.

    //-----------Überprüft ob ein Spieler noch Steine besitzt.
    public bool HasPieces(PieceColor color)
    {
        // Wir durchsuchen alle Zeilen.
        for (int row = 0; row < Size; row++)
        {
            // Wir durchsuchen in jeder Zeile alle Spalten.
            for (int col = 0; col < Size; col++)
            {
                // Wir holen den Stein (oder null) aus dem Feld.
                Piece? piece = GetPiece(row, col);

                // Wenn ein Stein da ist UND die Farbe passt,
                // dann hat der Spieler noch Steine.
                if (piece != null && piece.Color == color)
                    return true; // sofort true zurückgeben -> fertig
            }
        }

        // Wenn wir das ganze Brett durchsucht haben und nichts gefunden haben,
        // hat der Spieler keine Steine mehr.
        return false;
    }
}
