# Projekt Spengerchess

User der HTL Spengergasse sollen gegeneinander oder gegen eine KI Schach spielen können. Das
Projekt lässt sich wie folgt aufteilen:

### Anpassen der Python Chessengine

Lade von https://github.com/thomasahle/sunfish die Schachengine Sunfish. Das Pythonprogramm
ist für Eingaben von der Konsole geschrieben. Das muss geändert werden:
- Erstelle eine leere Applikation mit Phython und [Flask](https://flask.palletsprojects.com/en/2.2.x/).
  Flask ist ein Webserver. Das Programm soll auf folgende Routen reagieren:
  - **GET /api/game/new** liefert als Antwort eine UUID.
  - **GET /api/game/(game_uuid)/move/?from=(field)&to=(field)** wird aufgerufen, wenn der Mensch eine
    Figur auf dieses Feld zieht. Als Antwort sendet der Server den Zug der KI mit *{from: (field), from: (to)}*.
  - **GET /api/game/(game_uuid)/finish** wird aufgerufen, wenn das Spiel beendet werden soll.

### Erstellen der Spielelogik in JavaScript

Erstelle ein Modul in JavaScript in der Datei *chessService.js*. Dieses Service speichert das
Spielfeld als Array. Eine Methode *move()* bewegt eine Figur. Dabei ist zu prüfen, ob der Zug gültig
ist.

### Erstellen eines Websocket Clients für die Übertragung der Spielzüge

Erstelle eine Vue.js Komponente *ChessBoard*. Sie zeichnet das Spielfeld und die Spielfiguren.
Mit drag & drop kann eine Figur bewegt werden. Der Zug wird über den Websocket an alle verbundenen
Clients übertragen. Dort soll der Zug auch sichtbar sein.

### Erstellen der Webapp für die Spielverwaltung

Nach einem Login über das Schul-AD kommt der User in einen Warteraum. Dort kann er einen anderen
User zum Spielen einladen. Der andere User bekommt dann eine Notification und die Frage, ob er spielen
möchte. Alternativ kann der User auch gegen die KI spielen.

