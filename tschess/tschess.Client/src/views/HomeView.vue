<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
</script>


<template>
  <main>
    <!DOCTYPE html>
    <html>
      <section>
        <h1>Willkommen auf unserer Schach-Homepage</h1>
        <p>
          Hier finden Sie Informationen über das Schachspiel, die Schachregeln,
          Schachstrategien, Schachnotation und vieles mehr.
        </p>
        <p>
          Wir bieten auch eine Plattform zum Spielen von Schach online gegen
          andere Schüler an.
        </p>
      </section>
      <body>
        <div class="carousel">
          <img :src="currentImage" alt="" />
          <button @click="prev">&#8678;</button>
          <button @click="next">&#8680;</button>
        </div>

        <div class="flexbox">
          <div class="fb">
            <div class="card">
              <div class="card2 hover-div">
                <img src="./1.jpg" alt="Your image" class="blur-image" />
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
                  <h2>Dame</h2>
                  <p>
                    Die Dame darf auf jedes freie Feld derselben Linie, Reihe
                    oder Diagonale ziehen, ohne jedoch über andere Figuren zu
                    springen und vereint somit die Wirkung eines Turms und eines
                    Läufers in sich. Damit ist die Dame die beweglichste aller
                    Figuren.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="card">
          <div class="card2 hover-div">
            <img src="./chess1.png" alt="Your image" class="blur-image" />
            <div class="card-text">
              <h2>Bauer</h2>
              <p>
                Der Bauer ist die einzige Figur, die nicht rückwärts ziehen
                kann. Ebenso ist der Bauer die einzige Figur, die anders schlägt
                als zieht: er schlägt immer diagonal, zieht aber gerade.
              </p>
            </div>
          </div>
        </div>
        <div class="card">
          <div class="card2 hover-div">
            <img src="./chess1.png" alt="Your image" class="blur-image" />
            <div class="card-text">
              <h2>Bauer</h2>
              <p>
                Der Bauer ist die einzige Figur, die nicht rückwärts ziehen
                kann. Ebenso ist der Bauer die einzige Figur, die anders schlägt
                als zieht: er schlägt immer diagonal, zieht aber gerade.
              </p>
            </div>
          </div>
        </div>
        <div class="card">
          <div class="card2 hover-div">
            <img src="./chess1.png" alt="Your image" class="blur-image" />
            <div class="card-text">
              <h2>Bauer</h2>
              <p>
                Der Bauer ist die einzige Figur, die nicht rückwärts ziehen
                kann. Ebenso ist der Bauer die einzige Figur, die anders schlägt
                als zieht: er schlägt immer diagonal, zieht aber gerade.
              </p>
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
export default {
  data() {
    return {
      images: [
        "https://w.forfun.com/fetch/03/03fcf3213fcf72f9632092804abd88a2.jpeg",
        "https://image.winudf.com/v2/image/Y29tLkdvbGRNZWRhbC5DaGVzczYwX3NjcmVlbl8wXzM4NWo4YTV4/screen-0.jpg?fakeurl=1&type=.jpg",
        "https://rare-gallery.com/mocahbig/1344546-Black-amp-WhiteChess-4k-Ultra-HD-Wallpaper.jpg",
      ],
      currentIndex: 0,
      intervalId: null,
      intervalTime: 4000, // set the time in milliseconds for each slide
    };
  },
  computed: {
    currentImage() {
      return this.images[this.currentIndex];
    },
  },
  methods: {
    prev() {
      this.currentIndex =
        (this.currentIndex - 1 + this.images.length) % this.images.length;
    },
    next() {
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
    },
  },
};
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

section {
  float: left;
  width: 70%;
  padding: 20px;
  color: white;
}

aside {
  float: left;
  width: 30%;
  padding: 20px;
}

footer {
  background-color: #222;
  color: #fff;
  padding: 20px;
  text-align: center;
  font-size: 0.8em;
  clear: both;
}

.carousel {
  position: relative;
  width: 70%;
  height: 500px;
  overflow: hidden;
  margin-left: 15%;
  transition: transform 1s ease-in-out;
}

.carousel img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.carousel button {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  font-size: 2rem;
  background-color: transparent;
  border: none;
  color: white;
  cursor: pointer;
}

.carousel button:first-child {
  left: 0;
}

.carousel button:last-child {
  right: 0;
}
.card {
  width: 250px;
  height: 300px;
  background-image: linear-gradient(163deg, #00ff75 0%, #3700ff 100%);
  border-radius: 20px;
  transition: all 0.3s;
  margin-left: 20px;
  margin-top: 20px;
}

.card2 {
  width: 250px;
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
  width: 210px;
  background-color: rgba(0, 0, 0, 0.5);
  border-radius: 20px;
  color: #fff;
  display: none;
  transition: opacity 0.5s ease-in-out;
}

@media (min-width: 480px) {
   .flexbox { max-width: 430px; display: flex; flex-wrap: wrap }
   .fb { width: 49%; }
}
@media (min-width: 640px) {
   .flexbox { max-width: 580px;  }
   .fb { width: 32%; }
}

.fb{

  margin-left: 20px;
}
</style>
