interface Card {
  Name: string;
}

const getApplications = () => {
  fetch("http://localhost:5000/api/switch/availableapplications")
    .then((result) => {
      result.json()
        .then((cards: Card[]) => {
          const fragement = createCard(cards);
          debugger;
        })
    })
    .catch((error) => {
      //
    })
}

const createCard = (cardsData: Card[]) => {
  const fragement = document.createDocumentFragment();
  const container = document.createElement("div");
  container.classList.add("container");

  for (const card of cardsData) {
    const card = document.createElement("div");
    card.classList.add("card");

    const cardHeader = document.createElement("section");
    cardHeader.classList.add("card__header");
    const cardHeaderTitle = document.createElement("h1");
    cardHeaderTitle.classList.add("card__header-title");
    cardHeader.appendChild(cardHeaderTitle);

    const cardStatus = document.createElement("section");
    cardStatus.classList.add("card__status");

    const cardEnvironment = document.createElement("section");
    cardEnvironment.classList.add("card__environment");
    const cardEnvironmentTitle = document.createElement("div");
    cardEnvironmentTitle.classList.add("card__environment-title");
    cardEnvironment.appendChild(cardEnvironmentTitle);

    card.appendChild(cardHeader);
    card.appendChild(cardStatus);
    card.appendChild(cardEnvironment);
  }

  fragement.appendChild(container);
}

getApplications();