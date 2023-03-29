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
        <section class="slider-wrapper">
          <button class="slide-arrow" id="slide-arrow-prev">&#8249;</button>
          <button class="slide-arrow" id="slide-arrow-next">&#8250;</button>
          <ul class="slides-container" id="slides-container">
            <li class="slide"></li>
            <li class="slide"></li>
            <li class="slide"></li>
            <li class="slide"></li>
          </ul>
        </section>
      </body>
      <footer>
        <p>&copy; 2023 Schach Homepage</p>
      </footer>
    </html>
  </main>
</template>


<script>

const slidesContainer = document.getElementById("slides-container");
const slide = document.querySelector(".slide");
const prevButton = () => {
    const slider = document.getElementById("slide-arrow-prev");
    if (slider == null) return;
    slider.scrollLeft = slider.scrollLeft -1000;
};
const nextButton = () => {
    const slider = document.getElementById("slide-arrow-next");
    if (slider == null) return;
    slider.scrollLeft = slider.scrollLeft +1000;
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
</style>
