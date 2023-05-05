<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
</script>


<template>
  <main>
    <!DOCTYPE html>
    <html>
      <section>
        <h2>Willkommen auf unserer Schach-Homepage</h2>
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
        <div id="app">
          <carousel></carousel>
        </div>
        
        <div class="card">
          <div class="card2">
            <p><img src="./1.jpg" alt="" /></p>
          </div>
        </div>
        <div class="card">
          <div class="card2">
            <p><img src="./chess1.png" alt="" /></p>
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
  width: 190px;
  height: 254px;
  background-image: linear-gradient(163deg, #00ff75 0%, #3700ff 100%);
  border-radius: 20px;
  transition: all 0.3s;
  margin-left: 20px;
  display: inline-block;
}

.card2 {
  width: 190px;
  height: 254px;
  background-color: rgb(71, 70, 70);
  border-radius: 20px;
  transition: all 0.2s;
}

.card2:hover {
  transform: scale(0.98);
  border-radius: 20px;
}

.card:hover {
  box-shadow: 0px 0px 30px 1px rgba(0, 255, 117, 0.3);
}

.card img {
  width: 100%;
  height: 100%;
  margin-top: 20px;
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
</style>
