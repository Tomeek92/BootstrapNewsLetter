    var clipboard = new ClipboardJS('.btn');

   
    clipboard.on('success', function (e) {
        alert('Adresy email zostały skopiowane do schowka.');
        });

    
    clipboard.on('error', function (e) {
        alert('Skopiowanie adresów email do schowka nie powiodło się.');
        });

    
    document.getElementById('copyAll').addEventListener('click', function () {
            var emailList = document.getElementById('emailList');
    copyToClipboard(emailList.innerText);
        });

 
    document.getElementById('copySingle').addEventListener('click', function () {
            var emails = [];
    document.querySelectorAll('#emailList li').forEach(function (li) {
        emails.push(li.textContent.trim());
            });
    copyToClipboard(emails.join('\n'));
        });

  
    function copyToClipboard(text) {
            var input = document.createElement('textarea');
    input.style.position = 'fixed';
    input.style.opacity = 0;
    input.value = text;
    document.body.appendChild(input);
    input.select();
    document.execCommand('copy');
    document.body.removeChild(input);
    alert('Zawartość została skopiowana do schowka.');
        }
