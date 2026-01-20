document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("createUserForm");
    const message = document.getElementById("message");
    const usersTableBody = document.getElementById("usersTableBody");

    async function loadUsers() {
        try {
            const response = await fetch("/Users/GetAll");
            if (!response.ok) throw new Error("Falha ao obter usuários");
            const users = await response.json();

            usersTableBody.innerHTML = "";

            users.forEach(u => {
                const tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${u.id}</td>
                    <td>${u.username}</td>
                    <td>
                        <button class="btn btn-sm btn-warning edit-btn" data-id="${u.id}" data-username="${u.username}">Editar</button>
                        <button class="btn btn-sm btn-danger delete-btn" data-id="${u.id}">Deletar</button>
                    </td>
                `;
                usersTableBody.appendChild(tr);
            });
        } catch (err) {
            console.error(err);
            message.textContent = "Erro ao carregar usuários.";
        }
    }

    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        const username = document.getElementById("username").value.trim();
        if (!username) return;

        try {
            const response = await fetch("/Users/Create", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Username: username })
            });

            if (response.ok) {
                message.textContent = "Usuário criado com sucesso!";
                form.reset();
                loadUsers();
            } else {
                message.textContent = "Erro ao criar usuário.";
            }
        } catch (err) {
            console.error(err);
            message.textContent = "Erro de comunicação com o servidor.";
        }
    });

    usersTableBody.addEventListener("click", async (e) => {
        const target = e.target;
        const id = target.dataset.id;

        if (target.classList.contains("edit-btn")) {
            const currentUsername = target.dataset.username;
            const newUsername = prompt("Novo username:", currentUsername);
            if (!newUsername || newUsername.trim() === "") return;

            try {
                const response = await fetch("/Users/Edit", {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ Id: id, Username: newUsername.trim() })
                });

                if (response.ok) {
                    message.textContent = "Usuário atualizado com sucesso!";
                    loadUsers();
                } else {
                    message.textContent = "Erro ao atualizar usuário.";
                }
            } catch (err) {
                console.error(err);
                message.textContent = "Erro de comunicação com o servidor.";
            }
        }

        if (target.classList.contains("delete-btn")) {
            if (!confirm("Deseja realmente deletar este usuário?")) return;

            try {
                const response = await fetch(`/Users/Delete/${id}`, { method: "DELETE" });

                if (response.ok) {
                    message.textContent = "Usuário deletado com sucesso!";
                    loadUsers();
                } else {
                    message.textContent = "Erro ao deletar usuário.";
                }
            } catch (err) {
                console.error(err);
                message.textContent = "Erro de comunicação com o servidor.";
            }
        }
    });

    loadUsers();
});
