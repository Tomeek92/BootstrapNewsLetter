
    document.addEventListener('DOMContentLoaded', function () {
        var regulaminLink = document.getElementById('regulaminLink'),
            regulaminModal = document.getElementById('regulaminModal'),
            closeButton = regulaminModal.querySelector('.close');

        function openModal() {
            regulaminModal.style.display = 'block';
        }

        function closeModal() {
            regulaminModal.style.display = 'none';
        }

        regulaminLink.addEventListener('click', function (event) {
            event.preventDefault();
            openModal();
        });

        closeButton.addEventListener('click', function () {
            closeModal();
        });

        window.addEventListener('click', function (event) {
            if (event.target === regulaminModal) {
                closeModal();
            }
        });
    });


