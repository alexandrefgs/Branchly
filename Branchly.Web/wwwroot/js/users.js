document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("createUserForm");
    const message = document.getElementById("message");

    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        const username = document.getElementById("username").value;

        try {
            const response = await fetch("/Users/Create", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Username: username })

            });

            if (response.ok) {
                const userId = await response.text();
                message.textContent = `Usuário criado com sucesso! Id: ${userId}`;
                form.reset();
            } else {
                message.textContent = "Erro ao criar usuário.";
            }
        } catch (error) {
            console.error(error);
            message.textContent = "Erro de comunicação com o servidor.";
        }
    });
});
