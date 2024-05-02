let acc = document.getElementsByClassName("accordion");
let i;

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
        let panel = this.nextElementSibling;
        let isActive = this.classList.contains("active");

        let allPanels = document.querySelectorAll(".panel");
        allPanels.forEach(function (item) {
            if (item !== panel) {
                item.style.maxHeight = null;
                item.previousElementSibling.classList.remove("active");
            }
        });

        this.classList.toggle("active");
        if (isActive) {
            panel.style.maxHeight = null;
        } else {
            panel.style.maxHeight = panel.scrollHeight + "px";
            // Przewiń stronę tylko gdy lista jest rozwinięta
            let accordionTopOffset = document.querySelector(".accordion").offsetTop;
            window.scrollTo({ top: accordionTopOffset, behavior: 'smooth' })
        }
    });
}
function restartVideo() {
    var video = document.getElementById("myVideo");
    video.currentTime = 0;
    video.play();
}
