using System;
using TurkishDraughts.ConsoleApp.Models;
namespace TurkishDraughts.ConsoleApp.Game;

public static class Rules //Wird überprüft ob ein Zug erlaubt ist oder nicht
{
    // -----IsValidSimpleMove prüft einen normalen Zug (kein Schlagen).
    // - true  = Zug ist erlaubt ..... - false = Zug ist nicht erlaubt
    // "out string error" : Wir geben zusätzlich eine Text-Erklärung nach außen.
    public static bool IsValidSimpleMove(Board board, Move move, PieceColor currentPlayer, out string error)  
        //normaler Zug
    {
        error = "";   //error leer.Falls etwas schiefgeht, füllen wir diese Variable.

        //-------------------- 1)Start und Ziel müssen im Brett liegen (0 bis 7) Koordinaten im Brett ?------------------------------------------------
        
        if (!IsInside(move.From) || !IsInside(move.To))  // Wenn Start ODER Ziel außerhalb ist → Fehler... !IsInside(...) bedeutet: „nicht innerhalb“.
        {
            error = "Start oder Ziel liegt außerhalb des Spielfelds (0-7).";
            return false;      
         }
        // ------------------ 2)Auf dem Startfeld muss ein Stein stehen. Startfeld hat Stein?---------------------------------------------

        // Wir holen den Stein vom Startfeld. board.GetPiece(...) liefert entweder:
        // - ein Piece-Objekt (Stein existiert)......... - oder null (Feld ist leer)
        Piece? fromPiece = board.GetPiece(move.From.Row, move.From.Col);

        if (fromPiece == null)        // Wenn Stein null = Startfeld ist leer.
        {
                 error = "Auf dem Startfeld steht kein Stein."; 
                 return false;  
        }
        // ------------------------- 3)Der Stein muss dem aktuellen Spieler gehören. Stein gehört aktuellen dem Spieler?---------------------------------------------

        // fromPiece.Color ist entweder White oder Black
        // currentPlayer ist der Spieler, der gerade dran ist
        if (fromPiece.Color != currentPlayer)  // Wenn sie nicht gleich sind → falscher Spieler nimmt fremden Stein
        {
            error = "Dieser Stein gehört nicht dir (falsche Farbe).";
            return false;
        }
        // -------------------------4)Zielfeld muss leer sein.Ziel ist frei?-------------------------

        Piece? toPiece = board.GetPiece(move.To.Row, move.To.Col);
        
        if (toPiece != null)         // Wenn dort ein Stein ist (also nicht null), ist das Feld belegt.

        {
            error = "Zielfeld ist nicht leer.";
            return false;
        }

        //------------------------- 5)Bewegung darf nur horizontal/vertikal ----------------------mit Unterstützung KI

        int dRow = move.To.Row - move.From.Row;  // Wir berechnen wie weit man sich in Zeilen bewegt.Bsp: von 6 nach 5 → dRow = -1
        int dCol = move.To.Col - move.From.Col;   // wie weit man sich in Spalten bewegt.Bsp: von 0 nach 2 → dCol = 2
        
        bool isHorizontal = (dRow == 0 && dCol != 0);  //horizontal: Zeile bleibt gleich (dRow == 0), aber Spalte ändert sich (dCol != 0)
        bool isVertical = (dCol == 0 && dRow != 0); //vertikal : Spalte bleibt gleich (dCol == 0),aber Zeile ändert sich (dRow != 0)

        if (!isHorizontal && !isVertical)  // Falls es weder horizontal noch vertikal ist,dann ist es diagonal oder gar kein Zug.

        {
            error = "Ungültig: Du darfst nur horizontal oder vertikal ziehen (nicht diagonal).";
            return false;
        }
        // -------------------------6)Normaler Zug: genau 1 Feld weit.ist es genau 1 Feld?-------------------------


        // distance berechnet die Gesamtstrecke.
        // Da bei horizontal ODER vertikal eine Richtung 0 ist,kann man einfach abs(dRow) + abs(dCol) nehmen.
        // Beispiel: dRow=-1, dCol=0 → distance=1
        // Beispiel: dRow=0, dCol=1 → distance=1
        int distance = Math.Abs(dRow) + Math.Abs(dCol);

        if (distance != 1)    // Wenn distance nicht 1 ist, dann ist es mehr als ein Feld.

        {
            error = "Du darfst beim normalen Zug nur 1 Feld weit ziehen.";
             return false;
        }
        // ------------------------7)-Regel für normale Steine (Man):kein rückwärts ziehen -------------------------

        // Nur wenn der Stein ein normaler Stein ist (Man)
        // (nicht King)
        if (fromPiece.Type == PieceType.Man)
        {
            // Für Weiß: vorwärts = nach oben (Row wird kleiner).
            // Rückwärts wäre nach unten: dRow == +1
            if (currentPlayer == PieceColor.White && dRow == 1)
            {
                error = "Weiß darf mit einem normalen Stein nicht rückwärts ziehen.";
                return false;
            }

            // Für Schwarz: vorwärts = nach unten (Row wird größer).
            // Rückwärts wäre nach oben: dRow == -1
            if (currentPlayer == PieceColor.Black && dRow == -1)
            {
                error = "Schwarz darf mit einem normalen Stein nicht rückwärts ziehen.";
                return false;
            }
        }
        return true;      // Wenn wir bis hier gekommen sind, ist keine Regel verletzt → Zug ist gültig.
    }

    private static bool IsInside(Position p) // Hilfsmethode: Prüft ob Position innerhalb des 8x8 Bretts ist.
                                             // private = nur innerhalb dieser Datei/Klasse nutzbar.
    {
            
        return p.Row >= 0 && p.Row < Board.Size &&    // Row muss 0..7 sein und Col muss 0..7 sein.
                p.Col >= 0 && p.Col < Board.Size;    // Wenn beides stimmt → true, sonst false.
    }



//---------------------Pflichtschlagen
    public static bool PlayerHasAnyCapture(Board board, PieceColor player)
    {
        // Wir gehen alle Reihen durch (0 bis 7)
        for (int row = 0; row < Board.Size; row++)
        {
            // In jeder Reihe gehen wir alle Spalten durch (0 bis 7)
            for (int col = 0; col < Board.Size; col++)
            {
                // Wir schauen, ob auf diesem Feld ein Stein steht
                Piece? piece = board.GetPiece(row, col);

                // Wenn kein Stein da ist -> weiter mit dem nächsten Feld
                if (piece == null) continue;

                // Wenn der Stein nicht dem aktuellen Spieler gehört -> weiter
                if (piece.Color != player) continue;

                // Prüfen: Kann genau dieser Stein von hier aus schlagen?
                if (HasAnyCaptureFrom(board, new Position(row, col), piece))
                    return true; // Sobald EIN Schlag möglich ist -> Pflichtschlagen gilt
            }
        }

        // Wenn wir das ganze Brett durchsucht haben und kein Schlag möglich war:
        return false;
    }
    // //---------------------Schlagzug prüfen. Schlag bedeutet: 2 Felder weit springen (horizontal/vertikal) und in der Mitte muss ein gegnerischer Stein stehen.
    public static bool IsValidCaptureMove(Board board, Move move, PieceColor currentPlayer, out string error)
    {
        // Start: keine Fehlermeldung
        error = "";

        // 1) Prüfen ob Start/Ziel im Brett sind
        if (!IsInside(move.From) || !IsInside(move.To))
        {
            error = "Start oder Ziel liegt außerhalb des Spielfelds.";
            return false;
        }

        // 2) Startfeld muss einen Stein haben
        Piece? fromPiece = board.GetPiece(move.From.Row, move.From.Col);

        if (fromPiece == null)
        {
            error = "Auf dem Startfeld steht kein Stein.";
            return false;
        }

        // 3) Der Stein muss dem Spieler gehören, der dran ist
        if (fromPiece.Color != currentPlayer)
        {
            error = "Dieser Stein gehört nicht dir.";
            return false;
        }

        // 4) Zielfeld muss leer sein
        if (board.GetPiece(move.To.Row, move.To.Col) != null)
        {
            error = "Zielfeld ist nicht leer.";
            return false;
        }

        // 5) Wir berechnen, wie weit der Zug geht
        int dRow = move.To.Row - move.From.Row;
        int dCol = move.To.Col - move.From.Col;

        // ok = true, wenn der Zug GENAU 2 Felder geht (nur horizontal oder vertikal)
        bool ok =
            (Math.Abs(dRow) == 2 && dCol == 0) ||   // 2 Felder vertikal
            (Math.Abs(dCol) == 2 && dRow == 0);     // 2 Felder horizontal

        // Wenn nicht ok -> es ist kein gültiger Schlag
        if (!ok)
        {
            error = "Schlagen geht nur 2 Felder horizontal oder vertikal.";
            return false;
        }

        // 6) Der geschlagene Stein steht genau in der Mitte
        int middleRow = move.From.Row + dRow / 2;
        int middleCol = move.From.Col + dCol / 2;

        // Wir holen den Stein aus der Mitte
        Piece? middlePiece = board.GetPiece(middleRow, middleCol);

        // Wenn in der Mitte gar kein Stein ist -> kann man nicht schlagen
        if (middlePiece == null)
        {
            error = "Kein Gegner in der Mitte.";
            return false;
        }

        // Wenn in der Mitte ein eigener Stein steht -> darf man ihn nicht schlagen
        if (middlePiece.Color == currentPlayer)
        {
            error = "Du kannst keinen eigenen Stein schlagen.";
            return false;
        }

        // Wenn alles passt -> Schlag ist gültig
        return true;
    }

    //Mehrfachschlagen. Prüft: Nach einem Schlag -> kann derselbe Stein direkt nochmal schlagen?
    // Das ist wichtig für "Mehrfachschlag".
    public static bool HasAnotherCapture(Board board, Position pos, PieceColor player)
    {
        // Wir holen den Stein, der gerade gezogen/geschlagen hat
        Piece? piece = board.GetPiece(pos.Row, pos.Col);

        // Wenn da kein Stein ist -> kann er nicht weiterschlagen
        if (piece == null) return false;

        // Wenn der Stein nicht dem Spieler gehört -> auch nicht weiterschlagen
        if (piece.Color != player) return false;

        // Wir nutzen die Hilfsfunktion und prüfen, ob irgendwo ein weiterer Schlag möglich ist
        return HasAnyCaptureFrom(board, pos, piece);
    }

    // Hilfsfunktion: Prüft für EINEN Stein auf EINER Position,ob er einen Schlag machen kann.
    // Diese Methode wird intern benutzt (deshalb private).
    private static bool HasAnyCaptureFrom(Board board, Position from, Piece piece)
    {
        // Wir definieren alle möglichen Schlag-Ziele:
        // 2 Felder nach rechts, links, runter oder hoch
        Position[] targets =
        {
        new Position(from.Row, from.Col + 2), // rechts
        new Position(from.Row, from.Col - 2), // links
        new Position(from.Row + 2, from.Col), // runter
        new Position(from.Row - 2, from.Col)  // hoch
    };

        // Wir testen jedes Ziel nacheinander
        foreach (Position to in targets)
        {
            // Ziel muss innerhalb des Bretts sein
            if (!IsInside(to)) continue;

            // Das Zielfeld muss leer sein (sonst kann man nicht landen)
            if (board.GetPiece(to.Row, to.Col) != null) continue;

            // Bewegung berechnen
            int dRow = to.Row - from.Row;
            int dCol = to.Col - from.Col;

            // Mittelpunkt bestimmen (da steht der Gegner, den man schlagen würde)
            int middleRow = from.Row + dRow / 2;
            int middleCol = from.Col + dCol / 2;

            // Stein in der Mitte holen
            Piece? middle = board.GetPiece(middleRow, middleCol);

            // Wenn kein Stein in der Mitte -> kein Schlag möglich
            if (middle == null) continue;

            // Wenn Stein in der Mitte gleiche Farbe hat -> eigener Stein -> darf nicht schlagen
            if (middle.Color == piece.Color) continue;

            // Wenn wir bis hier kommen -> Schlag ist möglich
            return true;
        }

        // Kein Ziel hat funktioniert -> kein Schlag möglich
        return false;
    }


}