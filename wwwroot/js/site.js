document.addEventListener("DOMContentLoaded", function () {
    const notificacoes = document.querySelector(".notificacoes");
    const cardNotificacoes = document.querySelector(".cardNotificacoes");

    notificacoes.addEventListener("click", function () {
        if (cardNotificacoes.style.display === "none" || cardNotificacoes.style.display === "") {
            cardNotificacoes.style.display = "flex";
            cardNotificacoes.style.width = "100%";
            const rect = notificacoes.getBoundingClientRect();
            const cardRect = cardNotificacoes.getBoundingClientRect();

            cardNotificacoes.style.top = (rect.bottom + 10) + "px"; // 10px abaixo da div notificacoes

            // Ajusta a posição horizontal para não sair da tela
            let leftPosition = rect.right - cardRect.width;
            if (leftPosition < 0) {
                leftPosition = 0;
            }
            cardNotificacoes.style.left = leftPosition + "px";
        } else {
            cardNotificacoes.style.display = "none";
        }
    });

    // Opcional: Para ocultar o cardNotificacoes quando clicar fora dele
    document.addEventListener("click", function (event) {
        if (!notificacoes.contains(event.target) && !cardNotificacoes.contains(event.target)) {
            cardNotificacoes.style.display = "none";
        }
    });
});