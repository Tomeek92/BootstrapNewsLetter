
var modals = document.querySelectorAll('.modal');


var closeButtons = document.querySelectorAll('.close');


document.getElementById('regulaminLink').addEventListener('click', function () {
    modals[0].style.display = "block";
});

document.getElementById('regulaminLink1').addEventListener('click', function () {
    modals[1].style.display = "block";
});

document.getElementById('regulaminLink2').addEventListener('click', function () {
    modals[2].style.display = "block";
});

document.getElementById('regulaminLink3').addEventListener('click', function () {
    modals[3].style.display = "block";
});

// Obsługa kliknięcia na przyciskach zamknięcia
for (let i = 0; i < closeButtons.length; i++) {
    closeButtons[i].addEventListener('click', function () {
        this.parentElement.parentElement.style.display = "none";
    });
}


window.onclick = function (event) {
    for (let a = 0; a < modals.length; a++) {
        if (event.target == modals[a]) {
            modals[a].style.display = "none";
        }
    }
}


