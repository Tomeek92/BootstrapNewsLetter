document.getElementById('menu-toggle').addEventListener('click', function () {
    var menuList = document.getElementById('menu-list');
    if (menuList.style.display === 'block') {
        menuList.style.display = 'none';
    } else {
        menuList.style.display = 'block';
    }
});
