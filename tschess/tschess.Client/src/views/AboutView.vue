<template>
  <main>
    <!DOCTYPE html>
    <html>
      <header>
        <h1>Über uns</h1>
        <hr width="15%"/>
        <p style="padding-top:10px;padding-bottom:10px;">
          Es ist unsere Mission das wundervolle Spiel Schach der Menschheit
          näher zu bringen.
        </p>
      </header>
      <body>
        <div>
          <div class="subheading">
            <h2>Wer wir sind</h2>
          </div>
          <p>
            Wir sind ein kleines Team von Schachenthusiasten, die sich zum Ziel
            gesetzt haben, das Schachspiel zu revolutionieren.
          </p> 
          <p>
            Wir sind der Meinung, dass Schach ein Spiel ist, das jeder Schüler
            der HTL Spengergasse spielen sollte, da es ein Spiel ist, das die
            Kreativität und das logische Denken fördert.
          </p>
        </div>
        <div class="subheading">
          <div style="padding-bottom:16px">
            <h2>Unsere Geschichte</h2>
          </div>
        </div>
        <div class="container">
          <div class="image-container">
            <img src="./araber.jpg" alt="9/11-Mensch in der Wüste auf arabischem Pferd">
          </div>
          <div class="text">
            <p style="margin-top: 0;">
              Als sich unser hochgeschätzer Teamkollege und Co-Founder Khalid bin
              Abdulrahman Al-Hassan Al-Ali Al-Salem Al-Mahmoud Al-Qadiri
              (خالد بن عبدالرحمن الحسن العلي السالم المحمود القادري) auf einem
              sommerlichen Ritt durch die Wüste befand, kam ihm die Idee zu Tschess.
              Er wollte ein Schachspiel entwickeln, das die Schüler der HTL-Spengergasse 
              zusammenbringt und ihnen die Möglichkeit gibt, sich in einem friedlichen
              Wettstreit zu messen. 
            </p>
            <p>
              Khalid bin Abdulrahman Al-Hassan Al-Ali Al-Salem Al-Mahmoud Al-Qadiri
              (خالد بن عبدالرحمن الحسن العلي السالم المحمود القادري) war von der
              Schönheit und Stille der Wüste fasziniert, während er auf seinem
              sommerlichen Ritt durch die endlosen Sanddünen ritt. Die Ruhe und
              Einsamkeit ließen seine Gedanken
              schweifen und er begann über die Bedeutung von Gemeinschaft und
              Verbindung nachzudenken. Als passionierter Schachspieler fühlte er,
              dass dieses Spiel die perfekte Plattform bieten könnte, um Menschen
              aus verschiedenen Kulturen und Hintergründen zusammenzubringen. 
            </p>
            <p>
              Mit jedem Schritt seines Pferdes wurde seine Vision von Tschess
              klarer. Er wollte ein Schachspiel schaffen, das über die physischen
              Grenzen hinweg Menschen verbindet und ihnen die Möglichkeit gibt,
              ihre Strategien und Fähigkeiten in einem friedlichen Wettstreit
              auszutauschen. Es sollte ein Ort sein, an dem Anfänger ihr Können
              verbessern, Experten ihr Wissen teilen und alle Schachliebhaber
              eine lebendige und inklusive Gemeinschaft erleben konnten.
          </p>
          <p>
            Nachdem Khalid von seiner Reise zurückgekehrt war, teilte er seine
            Idee mit seinem engsten Freundeskreis. Zusammen mit Ihnen entwickelte
            er dann schließliche Tschess, wie wir es heute alle kennen und lieben.
          </p>
          </div>
        </div>
      </body>
    </html>
  </main>
</template>

<style>
main {
  font-family: "Poppins", sans-serif;
}

body {
  margin: 0;
  padding: 0;
  font-family: "Poppins", sans-serif;
  color: #fff;
  text-align: center;
  font-size: 1em;
  justify-content: center;
  align-items: center;
}

p {
  line-height: 1.75em;
}

header {
  text-align: center;
  color: white;
  font-size: 1.15em;
}

footer {
  background-color: #222;
  color: #fff;
  padding: 20px;
  text-align: center;
  font-size: 0.8em;
  clear: both;
}

.subheading {
  font-size: 1.15em;
  margin: 0;
  padding-top: 20px;
}
.element {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.container {
  display: flex;
  align-items: center;
}
.image-container {
  text-align: center;
}

.image-container img {
  max-width: 95%;
  height: auto;
}

.text {
  margin: 20px;
  text-align: center;
}

@media (min-width: 1500px) {
  .container {
    flex-direction: row;
    align-items: flex-start;
    max-width: 1600px;
    margin: 0 auto;
  }

  .image-container {
    text-align: right;
    max-width: 50%;
  }

  .image-container img {
    max-width: 100%;
    height: auto;
    padding-right: 20px;
  }

  .text {
    margin: 0;
    text-align: left;
    max-width: 50%;
  }

  .text p {
    padding-left: 20px;
  }

}
</style>
