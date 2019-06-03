'use strict';

var getApplications = function () {
    fetch("http://localhost:5000/api/switch/availableapplications")
        .then(function (result) {
        result.json()
            .then(function (cards) {
            var fragement = createCard(cards);
            debugger;
        });
    })
        .catch(function (error) {
        //
    });
};
var createCard = function (cardsData) {
    var fragement = document.createDocumentFragment();
    var container = document.createElement("div");
    container.classList.add("container");
    for (var _i = 0, cardsData_1 = cardsData; _i < cardsData_1.length; _i++) {
        var card = cardsData_1[_i];
        var card_1 = document.createElement("div");
        card_1.classList.add("card");
        var cardHeader = document.createElement("section");
        cardHeader.classList.add("card__header");
        var cardHeaderTitle = document.createElement("h1");
        cardHeaderTitle.classList.add("card__header-title");
        cardHeader.appendChild(cardHeaderTitle);
        var cardStatus = document.createElement("section");
        cardStatus.classList.add("card__status");
        var cardEnvironment = document.createElement("section");
        cardEnvironment.classList.add("card__environment");
        var cardEnvironmentTitle = document.createElement("div");
        cardEnvironmentTitle.classList.add("card__environment-title");
        cardEnvironment.appendChild(cardEnvironmentTitle);
        card_1.appendChild(cardHeader);
        card_1.appendChild(cardStatus);
        card_1.appendChild(cardEnvironment);
    }
    fragement.appendChild(container);
};
getApplications();
