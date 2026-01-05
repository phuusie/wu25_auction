function loadUserModal(userId) {
    const modalBody = document.getElementById("userModalContent");

    modalBody.innerHTML = `
        <div class="text-center">
            <span class="muted">Laddar användare...</span>
        </div>
    `;

    fetch(`/Admin?handler=UserDetails&id=${userId}`)
        .then(r => r.text())
        .then(html => modalBody.innerHTML = html)
        .catch(() => {
            modalBody.innerHTML = "<p>Kunde inte ladda användaren</p>";
        });
}

document.addEventListener("click", e => {
    
    const row = e.target.closest(".user-row");
    if (row) {
        loadUserModal(row.dataset.userId);
        return;
    }
    
    if (e.target.matches(".edit-toggle")) {
        const card = e.target.closest(".user-card");
        card.querySelector(".view-mode").style.display = "none";
        card.querySelector(".edit-mode").style.display = "block";
        e.target.style.display = "none";
        return;
    }

    if (e.target.matches(".cancel-edit")) {
        const card = e.target.closest(".user-card");
        card.querySelector(".edit-mode").style.display = "none";
        card.querySelector(".view-mode").style.display = "block";
        card.querySelector(".edit-toggle").style.display = "inline-block";
        return;
    }

    if (e.target.id === "open-message") {
        const card = e.target.closest(".user-card");
        card.querySelector(".view-mode").style.display = "none";
        card.querySelector(".edit-mode").style.display = "none";
        card.querySelector(".message-mode").style.display = "block";
        card.querySelector(".edit-toggle").style.display = "none";
        return;
    }

    if (e.target.classList.contains("cancel-message")) {
        const userId = document.querySelector("input[name='userId']").value;
        loadUserModal(userId);
        return;
    }
    
    if (e.target.id === "delete-user") {
        const userId = e.target.dataset.userId;

        if (!confirm("Är du säker?")) return;

        fetch("/Admin/DeleteUser", {
            method: "POST",
            headers: { "Content-Type": "application/x-www-form-urlencoded" },
            body: `userId=${encodeURIComponent(userId)}`
        })
            .then(r => {
                if (!r.ok) throw new Error();
                bootstrap.Modal.getInstance(
                    document.getElementById("userModal")
                ).hide();
                location.reload();
            });

        return;
    }
});

document.addEventListener("submit", e => {
    
    if (e.target.id === "editUserForm") {
        e.preventDefault();

        const form = e.target;
        const data = new FormData(form);

        fetch("/Admin/UpdateUser", {
            method: "POST",
            body: data
        })
            .then(r => r.json())
            .then(res => {
                if (!res.success) throw new Error();
                loadUserModal(res.user.id);
            });

        return;
    }

    if (e.target.matches(".message-mode form")) {
        e.preventDefault();

        fetch("/Admin/SendMessage", {
            method: "POST",
            body: new FormData(e.target)
        })
            .then(r => {
                if (!r.ok) throw new Error();
                bootstrap.Modal.getInstance(
                    document.getElementById("userModal")
                ).hide();
            });

        return;
    }
});

document.addEventListener("change", e => {
    if (!e.target.matches(".role-checkbox-input")) return;

    const form = document.getElementById("roleForm");
    const data = new FormData(form);

    fetch("/Admin/UpdateRoles", {
        method: "POST",
        body: data
    })
        .then(r => r.json())
        .then(res => {
            if (!res.success) throw new Error();

            const cell = document.querySelector(
                `[data-user-roles="${data.get("userId")}"]`
            );

            if (cell) {
                cell.innerHTML = res.roles
                    .map(r => `<span class="role-badge role-${r.toLowerCase()}">${r}</span>`)
                    .join("");
            }
        })
        .catch(() => alert("Kunde inte spara roller"));
});
