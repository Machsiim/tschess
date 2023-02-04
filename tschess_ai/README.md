# Python Chess AI

In *main.py* ist der Flask Webserver mit der Schach AI. Für die Entwicklung wird diese Datei
lokal mit der Python Umgebung in Windows ausgeführt.

## Erstellen des Containers

Damit das Programm auf anderen Rechnern und in der Cloud (AWS, Azure, ...) gestartet werden kann, gibt es auch ein
*[Dockerfile](Dockerfile)*. Sonst müssten die ganzen Python Pakete, die Umgebung, ... von Hand installiert werden.
Das Dockerfile erstellt einen Container mit Python3, installiert alle Pakete und kopiert die
Dateien aus dem Ordner *src* in den Container.

Das Image muss zuerst mit *docker build* erzeugt werden. Dafür gehe in das Verzeichnis *tschess_ai*,
wo auch die Datei *Dockerfile* ist. Danach werden in der Konsole die folgenden Befehle ausgeführt:

```
docker rm -f python_chessai
docker build -t python_chessai .
docker run -d -p 5002:80 --name python_chessai python_chessai
```

In Docker Desktop läuft nun der Container *python_chessai*. Das Portmapping weist den Port 5002 den
internen Port 80 (Parameter *-d 5002:80*) zu. Im Browser kann daher mit folgenden URLs der Server
aufgerufen werden:

- http://localhost:5002/api/game/new
- http://localhost:5002/api/game/674066f-354c-4651-9dd7-5e29d3ff5ced/move?from=A1&to=A2
- http://localhost:5002/api/game/674066f-354c-4651-9dd7-5e29d3ff5ced/finish

### Zum Debuggen: Anzeigen des Logs

Ausgaben erscheinen in docker logs. Dafür klicke in Docker Desktop auf den Namen des Containers
und schaue unter *Logs*.

## Das Dockerfile

Ein Dockerfile ist einfach eine Abfolge von Befehlen, die nach dem Laden des Grundimages ausgeführt
werden. Es ist wie wenn du eine leere Ubuntu Distribution installierst und danach in der Konsole
diese Befehle eingibst.

Das vorhandene [Dockerfile](Dockerfile) installiert einige Module über *pip install* und kopiert
danach die Sourcecode Dateien aus dem *src* ordner. Zum Schluss wird der Server mit
*flask --app main run --host=0.0.0.0 --port=80*
gestartet. Die Parameter müssen bei *CMD* als Array (statt space) geschrieben werden.

```dockerfile
FROM python:3.11.1

RUN python3 -m pip install --upgrade pip
RUN pip install --upgrade python-chess
RUN pip install --upgrade Flask
COPY src /home/app
WORKDIR /home/app
CMD ["flask", "--app", "main", "run", "--host=0.0.0.0", "--port=80"]

```