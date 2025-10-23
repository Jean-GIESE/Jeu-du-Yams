// Variable Globals
const btnFichier = document.querySelector(".btn-fichier");
const inputFicher = document.querySelector(".input-fichier");
let all_rounds_section = document.querySelector(".all-rounds ");
let individual_rounds_section = document.querySelector(".individual-rounds");
let pagination_section = document.querySelector(".carosal-change");
const rounds = document.querySelector(".rounds");
const round = document.querySelector(".round");
const tempRound = round.cloneNode(true);
rounds.innerHTML = "";
let fichier = "";

// Section: Ficher deposer
// Fonction pour valider la saisie
function validateInput(input) {
  if (!input || input.trim() === "") {
    alert("Veuillez fournir un valide numéro de fichier.");
    return false;
  }
  return true;
}
// Fonction pour récupérer des données
function fetchData(url) {
  return fetch(url)
    .then((res) => {
      if (!res.ok) {
        throw new Error(`Erreur ! Statut HTTP: ${res.status}`);
      }
      return res.json();
    })
    .catch((err) => {
      rounds.innerHTML = "";
      all_rounds_section.classList.add("display-none");
      individual_rounds_section.classList.add("display-none");
      pagination_section.classList.add("display-none");
      throw new Error(`Échec de la récupération des données : ${err.message}`);
    });
}
// Fonction principale déclenchée par un clic sur un bouton
function handleFileInput() {
  fichier = inputFicher.value;
  btnFichier.innerHTML = fichier;
  if (!validateInput(fichier)) {
    all_rounds_section.classList.add("display-none");
    individual_rounds_section.classList.add("display-none");
    pagination_section.classList.add("display-none");
    return;
  }
  const url = `http://yams.iutrs.unistra.fr:3000/api/games/${fichier}/parameters`;
  fetchData(url)
    .then(() => {
      all_rounds_section.classList.remove("display-none");
      individual_rounds_section.classList.add("display-none");
      pagination_section.classList.add("display-none");
      ParametersApi();
      ResultsApi();
      scoreBonusApi();
      AllroundsApi();
    })
    .catch((err) => {
      alert(err.message);
    });
}
btnFichier.addEventListener("click", handleFileInput);
inputFicher.addEventListener("keydown", (e) => {
  if (e.key === "Enter") {
    handleFileInput();
  }
});

// Section: Afficher résultat de jeux
function ParametersApi() {
  const game_info = document.querySelector(".game-info");
  const url = `http://yams.iutrs.unistra.fr:3000/api/games/${fichier}/parameters`;
  fetchData(url)
    .then((data) => {
      game_info.innerHTML =
        "Groupe:" + " 00" + data.code.split("groupe")[1] + " | " + data.date;
    })
    .catch((err) => alert(err.message));
}
// function pour api collecter le nom et id des jouers
function ResultsApi() {
  const id1 = document.querySelector(".result-1 .user-info .id");
  const name1 = document.querySelector(".result-1 .user-info .name");
  const id2 = document.querySelector(".result-2 .user-info .id");
  const name2 = document.querySelector(".result-2 .user-info .name");
  const url = `http://yams.iutrs.unistra.fr:3000/api/games/${fichier}/players`;
  fetchData(url)
    .then((data) => {
      id1.innerHTML = "00" + data[0].id;
      name1.innerHTML = data[0].pseudo;
      id2.innerHTML = "00" + data[1].id;
      name2.innerHTML = data[1].pseudo;
    })
    .catch((err) => alert(err.message));
}

// fonction pour collecter le résultat final du jeu
function scoreBonusApi() {
  const result1 = document.querySelector(".result-1");
  const result2 = document.querySelector(".result-2");
  const url = `http://yams.iutrs.unistra.fr:3000/api/games/${fichier}/final-result`;
  fetchData(url)
    .then((data) => {
      result1.querySelector(".score-display").innerHTML = data[0].score;
      result1.querySelector(".bonus-info").innerHTML =
        "Bonus: " + data[0].bonus;
      result2.querySelector(".score-display").innerHTML = data[1].score;
      result2.querySelector(".bonus-info").innerHTML =
        "Bonus: " + data[1].bonus;
      if (data[0].score > data[1].score) {
        result1.querySelector("img").classList.remove("display-none");
        result2.querySelector("img").classList.add("display-none");
      } else {
        result2.querySelector("img").classList.remove("display-none");
        result1.querySelector("img").classList.add("display-none");
      }
    })
    .catch((err) => alert(err.message));
}

// Section: Affichage des tous les rounds
function AllroundsApi() {
  rounds.innerHTML = "";

  for (let i = 1; i <= 13; i++) {
    const newRound = tempRound.cloneNode(true);
    const roundCard = newRound.querySelectorAll(".round-div .round-card");
    const url = `http://yams.iutrs.unistra.fr:3000/api/games/${fichier}/rounds/${i}`;

    fetchData(url)
      .then((data) => {
        newRound.querySelector("h3").textContent = `Round ${i}`;

        data.results.forEach((result, index) => {
          const card = roundCard[index];
          card.querySelector(
            ".player-id"
          ).textContent = `00${result.id_player}`;
          card.querySelector(".challenge").textContent = result.challenge;
          card.querySelector(".score").textContent = result.score;
          card.querySelector(".dices").innerHTML = afficherDes(result.dice);
        });
      })
      .catch((err) => {
        alert(`Erreur pour le round ${i} : ${err.message}`);
      });

    rounds.appendChild(newRound);
  }
}

function afficherDes(de) {
  const des = `
               <div class="dice-corner">
                  <img src="./src/images/dice-${de[0]}.svg" />
                  <img src="./src/images/dice-${de[1]}.svg" />
                </div>
                <div class="dice-center">
                      <img src="./src/images/dice-${de[2]}.svg" />
                </div>
                <div class="dice-corner">
                      <img src="./src/images/dice-${de[3]}.svg" />
                      <img src="./src/images/dice-${de[4]}.svg" />
                </div>`;
  return des;
}

// fonction pour afficher la vue pour tous les tours
document.querySelector("#global-view-btn").addEventListener("click", () => {
  if (rounds.childNodes.length == 13) {
    all_rounds_section.classList.remove("display-none");
    individual_rounds_section.classList.add("display-none");
    pagination_section.classList.add("display-none");
  }
});

// Function for individual round view
document.querySelector("#btn-rounds").addEventListener("click", () => {
  let index = 1;
  if (rounds.childNodes.length == 13) {
    all_rounds_section.classList.add("display-none");
    individual_rounds_section.classList.remove("display-none");
    pagination_section.classList.remove("display-none");
    changerTour(index);
  }
});

// ******************************************************************************************
// Section individual Rounds

function changerTour(index) {
  const left_btn = document.querySelector(".left-arrow");
  const right_btn = document.querySelector(".right-arrow");
  left_btn.addEventListener("click", () => {
    if (index > 1) {
      index = index - 1;
      individual_rounds(index);
      updatePagination(index);
    }
    // else {
    //   index = 13;
    //   individual_rounds(index);
    //   updatePagination(index);
    // }
  });
  right_btn.addEventListener("click", () => {
    if (index < 13) {
      index = index + 1;
      individual_rounds(index);
      updatePagination(index);
    }
    // else {
    //   index = 1;
    //   individual_rounds(index);
    //   updatePagination(index);
    // }
  });
  // pour premier fois
  individual_rounds(index);
  updatePagination(index);
}
const updatePagination = (newIndex) => {
  pagination_section
    .querySelectorAll("div")
    .forEach((item) => item.classList.remove("isTrue"));
  pagination_section
    .querySelectorAll("div")
    [newIndex - 1].classList.add("isTrue");
};

function individual_rounds(index) {
  const h1 = document.querySelector(".individual-rounds h1");
  const jour1_des = document.querySelector(
    ".result-jouer-1 .dice-design-container"
  );
  const jour2_des = document.querySelector(
    ".result-jouer-2 .dice-design-container"
  );
  const jouer1_text = document.querySelector(".result-jouer-1");
  const jouer2_text = document.querySelector(".result-jouer-2");
  const url = `http://yams.iutrs.unistra.fr:3000/api/games/${fichier}/rounds/${index}`;
  fetchData(url)
    .then((data) => {
      h1.innerHTML = `Round ${data.id}`;
      jour1_des.innerHTML = afficherDeIndividualRounds(data.results[0].dice);
      jour2_des.innerHTML = afficherDeIndividualRounds(data.results[1].dice);
      jouer1_text.querySelector(".challenge").innerHTML =
        data.results[0].challenge;
      jouer2_text.querySelector(".challenge").innerHTML =
        data.results[1].challenge;
      jouer1_text.querySelector(".id").innerHTML =
        "Jouer: 00" + data.results[0].id_player;
      jouer2_text.querySelector(".id").innerHTML =
        "Jouer: 00" + data.results[1].id_player;
      jouer1_text.querySelector(".score").innerHTML =
        "Score: " + data.results[0].score;
      jouer2_text.querySelector(".score").innerHTML =
        "Score: " + data.results[1].score;
    })
    .catch((err) => alert(err.message));
}
function afficherDeIndividualRounds(de) {
  const des = ` <img src="./src/images/dice-${de[0]}.svg" />
                <img src="./src/images/dice-${de[1]}.svg" />
                <img src="./src/images/dice-${de[2]}.svg" />
                <img src="./src/images/dice-${de[3]}.svg" />
                <img src="./src/images/dice-${de[4]}.svg" />
              `;
  return des;
}
