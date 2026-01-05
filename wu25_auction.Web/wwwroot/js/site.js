document.addEventListener("click", function (e) {
    const row = e.target.closest(".user-row");
    if (!row) return;

    const userId = row.dataset.userId;

    fetch(`/Admin/UserDetails?id=${userId}`)
        .then(res => res.text())
        .then(html => {
            document.getElementById("userModalContent").innerHTML = html;
        });
});

document.addEventListener("change", function (e) {
    if (!e.target.matches(".role-checkbox-input")) return;

    const form = document.getElementById("roleForm");
    if (!form) return;

    const label = e.target.closest(".role-checkbox");
    label.classList.add("saving");

    setTimeout(() => label.classList.remove("saving"), 300);

    const data = new FormData(form);

    fetch("/Admin/UpdateRoles", {
        method: "POST",
        body: data
    })
        .then(res => res.json())
        .then(result => {
            if (!result.success) throw new Error();

            const roleCell = document.querySelector(
                `[data-user-roles="${data.get("userId")}"]`
            );

            if (roleCell) {
                roleCell.innerHTML = "";
                result.roles.forEach(role => {
                    const span = document.createElement("span");
                    span.className = `role-badge role-${role.toLowerCase()}`;
                    span.textContent = role;
                    roleCell.appendChild(span);
                });
            }
        })
        .catch(err => {
            alert("Kunde inte spara roller");
            console.error(err);
        });
});

document.addEventListener("submit", function (e) {
    if (!e.target.matches("#editUserForm")) return;

    e.preventDefault();

    const form = e.target;
    const data = new FormData(form);

    const tokenInput = form.querySelector('input[name="__RequestVerificationToken"]');
    const headers = tokenInput ? { "RequestVerificationToken": tokenInput.value } : {};

    fetch("/Admin/UpdateUser", {
        method: "POST",
        body: data,
        headers
    })
        .then(res => {
            if (!res.ok) throw new Error("UpdateUser failed");
            return res.json();
        })
        .then(result => {
            if (!result.success) throw new Error();

            const user = result.user;

            // 🔁 uppdatera tabellraden
            const row = document.querySelector(
                `.user-row[data-user-id="${user.id}"]`
            );

            if (row) {
                row.querySelector(".user-name").textContent =
                    `${user.firstName ?? ""} ${user.lastName ?? ""}`;

                row.querySelector(".user-email").textContent = user.email;
                row.querySelector(".user-phone").textContent =
                    user.phoneNumber ?? "";

                row.querySelector(".user-city").textContent =
                    user.city ?? "";
            }

            fetch(`/Admin/UserDetails?id=${user.id}`)
                .then(res => res.text())
                .then(html => {
                    document.getElementById("userModalContent").innerHTML = html;
                });
        })
});

document.addEventListener("click", function (e) {

    if (e.target.matches(".edit-toggle")) {
        const card = e.target.closest(".user-card");
        card.querySelector(".view-mode").style.display = "none";
        card.querySelector(".edit-mode").style.display = "block";
        card.querySelector("#edit-button").style.display = "none";
    }

    if (e.target.matches(".cancel-edit")) {
        const card = e.target.closest(".user-card");
        card.querySelector(".edit-mode").style.display = "none";
        card.querySelector(".view-mode").style.display = "block";
        card.querySelector("#edit-button").style.display = "block";
    }

    if (e.target.id === "open-message") {
        document.querySelector(".view-mode").style.display = "none";
        document.querySelector(".edit-mode").style.display = "none";
        document.querySelector(".message-mode").style.display = "block";
        document.getElementById("edit-button").style.display = "none";
    }

    if (e.target.classList.contains("cancel-message")) {
        document.querySelector(".message-mode").style.display = "none";
        document.querySelector(".view-mode").style.display = "block";
        document.getElementById("edit-button").style.display = "block";
    }

});

document.addEventListener("click", function (e) {
    const messageBtn = e.target.closest("#message-user");
    if (!messageBtn) return;

    const userId = messageBtn.dataset.userId;
    const template = document.getElementById("message-template");
    const clone = template.content.cloneNode(true);

    clone.querySelector("input[name='UserId']").value = userId;

    const modalBody = document.getElementById("userModalContent");
    modalBody.innerHTML = "";
    modalBody.appendChild(clone);
});

document.addEventListener("click", function (e) {
    if (!e.target.matches(".cancel-message")) return;

    const userId = document.querySelector("input[name='UserId']").value;

    fetch(`/Admin/UserDetails?id=${userId}`)
        .then(res => res.text())
        .then(html => {
            document.getElementById("userModalContent").innerHTML = html;
        });
});

document.addEventListener("submit", function (e) {
    if (!e.target.matches("#messageForm")) return;

    e.preventDefault();

    const form = e.target;
    const data = new FormData(form);

    fetch("/Admin/SendMessage", {
        method: "POST",
        body: data
    })
        .then(res => {
            if (!res.ok) throw new Error("Misslyckades att skicka mail");

            const modal = bootstrap.Modal.getInstance(
                document.getElementById("userModal")
            );
            modal.hide();
        })
        .catch(err => {
            alert("Kunde inte skicka meddelandet");
            console.error(err);
        });
});

document.addEventListener("click", function (e) {
    if (!e.target.matches("#delete-user")) return;

    const userId = e.target.dataset.userId;

    if (!confirm("Är du säker på att du vill ta bort användaren?")) return;

    fetch("/Admin/DeleteUser", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: `userId=${encodeURIComponent(userId)}`
    })
        .then(res => {
            if (!res.ok) throw new Error("Delete failed");

            const modal = bootstrap.Modal.getInstance(
                document.getElementById("userModal")
            );
            modal.hide();

            location.reload();
        })
        .catch(err => {
            alert("Kunde inte ta bort användaren");
            console.error(err);
        });
});