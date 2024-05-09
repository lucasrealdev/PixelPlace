document.addEventListener('DOMContentLoaded', function() {
    const menuHamburguer = document.getElementById('menu-hamburguer');
    const hamburguer = document.querySelector('.hamburguer');
    const menu = document.querySelector('#navegacao');

    // Verifica se os elementos existem
    if (menuHamburguer && hamburguer && menu) {
        menuHamburguer.addEventListener('change', function () {
            if (this.checked) {
                hamburguer.style.transform = 'rotate(45deg)';
                hamburguer.style.setProperty('background', '#2596BE', 'important');
            } else {
                hamburguer.style.setProperty('background', '#FFFFFF', 'important');
                hamburguer.style.transform = '';
            }
        });

        window.addEventListener('scroll', function() {
            // Verifica a posição vertical da página
            var scrollPosition = window.scrollY || window.pageYOffset || document.documentElement.scrollTop;
    
            // Verifica se a posição vertical excede 600 pixels
            if (scrollPosition > menu.clientHeight-window.innerHeight && menuHamburguer.checked) {
                console.log(menu.offsetHeight)
                // Se exceder, define a posição do scroll como 600 pixels
                window.scrollTo(0, menu.offsetHeight-window.innerHeight);
            }
        });
        
    } else {
        console.error("Os elementos não foram encontrados.");
    }
});

// SLIDERS

  