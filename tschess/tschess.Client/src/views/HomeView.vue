<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
</script>


<template>
  <main>
    <!DOCTYPE html>
    <html>
      <header>
        <h1>Willkommen auf unserer Schach-Homepage</h1>
        <p>
          Hier finden Sie Informationen über das Schachspiel, die Schachregeln,
          Schachstrategien, Schachnotation und vieles mehr.
        </p>
        <p>
          Wir bieten auch eine Plattform zum Spielen von Schach online gegen
          andere Schüler an.
        </p>
      </header>
      <body>
        <div class="container">
          <div class="fb">
            <div class="card">
              <div class="card2 hover-div">
                <img
                  id="springers"
                  src="./1.jpg"
                  alt="Your image"
                  class="blur-image"
                />
                <div class="card-text">
                  <h2>Springer</h2>
                  <p>
                    Der Springer, oder Pferd genannt, darf zwei unterschiedliche
                    Züge machen: Entwe‐ der zieht er ein Feld geradeaus und zwei
                    Felder nach links oder rechts. Oder er zieht zwei Felder
                    geradeaus und ein Feld nach links oder rechts.
                  </p>
                </div>
              </div>
            </div>
          </div>

          <div class="fb">
            <div class="card">
              <div class="card2 hover-div">
                <img src="./chess1.png" alt="Your image" class="blur-image" />
                <div class="card-text">
                  <h2>Bauer</h2>
                  <p>
                    Der Bauer ist die einzige Figur, die nicht rückwärts ziehen
                    kann. Ebenso ist der Bauer die einzige Figur, die anders
                    schlägt als zieht: er schlägt immer diagonal, zieht aber
                    gerade.
                  </p>
                </div>
              </div>
            </div>
          </div>

          <div class="fb">
            <div class="card">
              <div class="card2 hover-div">
                <img src="./chess2.png" alt="Your image" class="blur-image" />
                <div class="card-text">
                  <h2>Läufer</h2>
                  <p>
                    darf auf jedes freie Feld derselben Linie, Reihe oder
                    Diagonale ziehen, ohne jedoch über andere Figuren zu
                    springen und vereint somit die Wirkung eines Turms und eines
                    Läufers in sich. Damit ist die Dame die beweglichste aller
                    Figuren.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="container">
          <div class="card">
            <div class="card2 hover-div">
              <img
                id="turm"
                src="./turm.jpg"
                alt="Your image"
                class="blur-image"
              />
              <div class="card-text">
                <h2>Turm</h2>
                <p>
                  Ein Turm kann sich sowohl horizontal als auch vertikal über
                  eine beliebige Anzahl von Feldern bewegen. Er darf auf jedes
                  freie Feld in jeder Richtung linear ziehen, ohne jedoch über
                  andere Figuren zu springen. Die einzige Ausnahme davon bildet
                  die Rochade, in deren Verlauf der Turm einmalig über den König
                  springt.
                </p>
              </div>
            </div>
          </div>
          <div class="card">
            <div class="card2 hover-div">
              <img src="./queen.png" alt="Your image" class="blur-image" />
              <div class="card-text">
                <h2>Königin</h2>
                <p>
                  Die Königin ist die stärkste Figur im Schach. Ihre Gangart
                  kombiniert Turm und Läufer. Sie kann beliebig weit in alle
                  Richtungen ziehen, darf allerdings keine Figuren überspringen.
                </p>
              </div>
            </div>
          </div>

          <div class="card">
            <div class="card2 hover-div">
              <img src="./könig.jpg" alt="Your image" class="blur-image" />
              <div class="card-text">
                <h2>König</h2>
                <p>
                  Der König kann jeweils ein Feld in jede Richtung gehen. Damit
                  kann er alle Felder des Schachbretts erreichen. Wegen seiner
                  kleinen Reichweite benötigt er dazu aber viele Züge. Der König
                  darf kein bedrohtes Feld betreten.
                </p>
              </div>
            </div>
          </div>
        </div>
      </body>

      <footer>
        <p>&copy; 2023 Schach Homepage</p>
      </footer>
    </html>
  </main>
</template>




<script>
</script>


<style>
main {
  font-family: "Poppins", sans-serif;
}

body {
  margin: 0;
  padding: 0;
  font-family: "Poppins", sans-serif;
}

header {
  text-align: center;
  color: white;
}

footer {
  background-color: #222;
  color: #fff;
  padding: 20px;
  text-align: center;
  font-size: 0.8em;
  clear: both;
}

.card {
  width: 300px;
  height: 300px;
  background-image: linear-gradient(163deg, #00ff75 0%, #3700ff 100%);
  border-radius: 20px;
  transition: all 0.3s;
  margin-left: 20px;
  margin-top: 20px;
}

.card2 {
  position: relative;
  border-radius: 10px;
  width: 300px;
  height: 300px;
  background-color: rgb(71, 70, 70);
  border-radius: 20px;
  transition: all 0.2s;
}

.card2:hover {
  transform: scale(0.98);
  border-radius: 20px;
}

.card-image {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.blur-image {
  filter: blur(0);
  transition: filter 0.5s ease-in-out;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.card:hover .blur-image {
  filter: blur(5px);
}

.card:hover .card-text {
  display: inline-block;
}

.card-text {
  position: absolute;
  bottom: 0;
  left: 0;
  top: 0;
  padding: 20px;
  width: 25 0px;
  background-color: rgba(0, 0, 0, 0.5);
  border-radius: 20px;
  color: #fff;
  display: none;
  transition: opacity 0.5s ease-in-out;
}

.container {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}


</style>
