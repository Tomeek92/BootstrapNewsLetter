document.getElementById("newsletterForm").addEventListener("submit", function (event) {
    // Zatrzymaj domyślne zachowanie formularza (przeładowanie strony)
    event.preventDefault();

    // Wyślij żądanie POST do kontrolera za pomocą JavaScript
    var form = document.getElementById("newsletterForm");
    var formData = new FormData(form);

    fetch("/NewsLetter/SaveEmailToDb", {
        method: "POST",
        body: formData
    })
        .then(response => {
            if (response.ok) {
                var toastElement = document.getElementById("toastMessage");
                var toastText = document.getElementById("toastText");
                toastText.textContent = "Zostałeś zapisany!";
                toastElement.classList.remove("hidden");
                // Wyczyść pole e-mail
                document.getElementById("emailUser").value = "";
                setTimeout(function () {
                    toastElement.classList.add("hidden");
                }, 4000);
            } else {
                alert("Wystąpił błąd podczas zapisywania adresu e-mail.");
            }
        })
        .catch(error => {
            console.error('Błąd:', error);
            alert("Wystąpił błąd podczas przetwarzania żądania.");
        });
});

document.getElementById("closeToast").addEventListener("click", function () {
    document.getElementById("toastMessage").classList.add("hidden");
});