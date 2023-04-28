<script setup lang="ts">
</script>

<template>
  <main>
    <!DOCTYPE html>
    <html>
      <head>
        <title>Schach Homepage</title>
        <meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      </head>
      <body>
        <header>
          <h1>Schach Homepage</h1>
        </header>

        <section>
          <h2>Willkommen auf unserer Schach-Homepage</h2>
          <p>
            Hier finden Sie Informationen über das Schachspiel, die
            Schachregeln, Schachstrategien, Schachnotation und vieles mehr.
          </p>
          <p>
            Wir bieten auch eine Plattform zum Spielen von Schach online gegen
            andere Schüler an.
          </p>
        </section>

        <div class="carousel">
          <img :src="currentImage" alt="" />
          <button @click="prev">&#8678;</button>
          <button @click="next">&#8680;</button>
        </div>
        <div id="app">
          <carousel></carousel>
        </div>

        <div class="book">
          <p>
            Der Springer, oder Pferd genannt, darf zwei unterschiedliche Züge
            machen: Entwe‐ der zieht er ein Feld geradeaus und zwei Felder nach
            links oder rechts. Oder er zieht zwei Felder geradeaus und ein Feld
            nach links oder rechts. Der Springer ist die einzige Figur, die über
            andere Figuren springen kann.
          </p>
          <div class="cover">
            <img src="./1.jpg" alt="" />
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
header {
  color: white;
}

body {
  margin: 0;
  padding: 0;
  font-family: Arial, sans-serif;
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
.container {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}
.book {
  position: relative;
  border-radius: 10px;
  width: 220px;
  height: 300px;
  background-color: whitesmoke;
  -webkit-box-shadow: 1px 1px 12px #000;
  box-shadow: 1px 1px 12px #000;
  -webkit-transform: preserve-3d;
  -ms-transform: preserve-3d;
  transform: preserve-3d;
  -webkit-perspective: 2000px;
  perspective: 2000px;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  -webkit-box-pack: center;
  -ms-flex-pack: center;
  justify-content: center;
  color: #000;
}

.cover {
  top: 0;
  position: absolute;
  background-color: lightgray;
  width: 100%;
  height: 100%;
  border-radius: 10px;
  cursor: pointer;
  -webkit-transition: all 0.5s;
  transition: all 0.5s;
  -webkit-transform-origin: 0;
  -ms-transform-origin: 0;
  transform-origin: 0;
  -webkit-box-shadow: 1px 1px 12px #000;
  box-shadow: 1px 1px 12px #000;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  -webkit-box-pack: center;
  -ms-flex-pack: center;
  justify-content: center;
}

.book:hover .cover {
  -webkit-transition: all 0.5s;
  transition: all 0.5s;
  -webkit-transform: rotatey(-80deg);
  -ms-transform: rotatey(-80deg);
  transform: rotatey(-80deg);
}

p {
  font-size: 15px;
  font-weight: bolder;
}
img {
  height: 300px;
}
</style>
