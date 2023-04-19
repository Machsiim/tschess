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

        <div class="card">
          <div class="card-header">
            <img src="./chess1.png" alt="Schach" />
          </div>
          <div class="card-body">
            <h4>Bauern </h4>
            <p>
              
            </p>
          </div>
        </div>
        <div class="card">
          <div class="card-header">
            <img src="./Projekt3.jpg" alt="Schach" />
          </div>
          <div class="card-body">
            <h4>Springer</h4>
            <p>
              
            </p>
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

.card {
  color: darkgreen;
  margin: 10px;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 2px 20px rgba(0, 0, 0, 0.2);
  overflow: hidden;
  width: 300px;
}
.card:hover {
  background-color: rgba(255, 242, 242, 0.8);
}
.card-header img {
  border-radius: 10px;
  width: 100%;
  height: 200px;
  object-fit: cover;
}
.card-body {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  padding: 20px;
  min-height: 250px;
  color: black;
}
.card-body p {
  font-size: 13px;
  margin: 0 0 40px;
}
</style>
