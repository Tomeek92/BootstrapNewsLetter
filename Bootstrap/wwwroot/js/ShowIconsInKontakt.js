const icons = document.querySelectorAll('.socialMediaa');
icons.forEach((icon, index) => {
    setTimeout(() => {
        icon.style.opacity = '1';
    }, (index + 1) * 1000);
});

