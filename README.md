# Türkische Dame – Konsolenanwendung in C#

## Projektbeschreibung

Dieses Projekt ist eine Konsolenanwendung in **C#**, in der das Brettspiel **Türkische Dame** umgesetzt wurde.  
Das Spiel wird vollständig über die **Konsole** gespielt und basiert auf den grundlegenden Regeln der türkischen Dame.

Ziel des Projekts war es, die **Grundlagen der objektorientierten Programmierung (OOP)** anzuwenden und ein strukturiertes Programm mit mehreren Klassen zu entwickeln.

---

## Spielfunktionen

Das Programm unterstützt folgende Funktionen:

- Darstellung eines **8×8 Spielfelds** in der Konsole
- Bewegung der Spielsteine über **Koordinaten-Eingabe**
- **Normale Züge** (horizontal und vertikal)
- **Schlagen gegnerischer Steine**
- **Pflichtschlagen**
- **Mehrfachschlagen**
- **Umwandlung eines Steins zur Dame (King)**
- **Spielende**, wenn ein Spieler keine Steine mehr besitzt

---

## Spielregeln (Kurzfassung)

Das Spiel basiert auf den Regeln der **Türkischen Dame**.

- Das Spielfeld besteht aus **8 × 8 Feldern**
- Jeder Spieler startet mit **16 Spielsteinen**
- Steine bewegen sich **horizontal oder vertikal**
- **Diagonale Bewegungen sind nicht erlaubt**
- Wenn ein gegnerischer Stein übersprungen werden kann, muss dieser **geschlagen werden (Pflichtschlagen)**
- Nach einem Schlag kann ein **Mehrfachschlag** folgen
- Wenn ein Stein die gegenüberliegende Seite erreicht, wird er zur **Dame (King)**
- Das Spiel endet, wenn ein Spieler **keine Steine mehr besitzt**

---

## Projektstruktur

Das Projekt ist objektorientiert aufgebaut und besteht aus mehreren Klassen:

| Klasse | Beschreibung |
|------|------|
| **Program** | Startpunkt der Anwendung |
| **GameEngine** | Steuert den gesamten Spielablauf |
| **Board** | Verwaltet das Spielfeld |
| **Rules** | Überprüft die Spielregeln |
| **ConsoleRenderer** | Zeigt das Spielfeld in der Konsole an |
| **Move** | Beschreibt einen Spielzug |
| **Piece** | Repräsentiert einen Spielstein |
| **Position** | Speichert eine Position auf dem Spielfeld |
| **PieceColor** | Definiert die Farbe eines Steins |
| **PieceType** | Definiert den Typ eines Steins (normal / Dame) |

---

## Projektziele

Das Projekt sollte folgende Lernziele erfüllen:

- Anwendung von **Objektorientierung in C#**
- Strukturierung eines Projekts in mehrere Klassen
- Umsetzung von **Spiellogik und Regeln**
- Verarbeitung von **Benutzereingaben**
- Darstellung eines Spiels in der **Konsole**

---

## Starten des Programms

1. Projekt in **Visual Studio öffnen**
2. Projekt als **Startprojekt festlegen**
3. Programm starten

Das Spiel läuft vollständig in der **Konsole** und fragt die Spieler nach ihren Zügen.

Beispiel für eine Eingabe:


6 0 5 0


Dies bewegt einen Stein von Position **(6,0)** nach **(5,0)**.

---

## Projektstatus

Aktueller Stand des Projekts:

✔ Spielfeld und Darstellung  
✔ Spiellogik  
✔ Normale Züge  
✔ Schlaglogik  
✔ Pflichtschlagen  
✔ Mehrfachschläge  
✔ Umwandlung zur Dame  
✔ Gewinnerkennung  

---

Projekt erstellt von:

**Süleyman Sevimli**

