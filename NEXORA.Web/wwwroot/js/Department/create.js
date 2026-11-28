document.addEventListener("DOMContentLoaded", () => {

    const form = document.getElementById("departmentCreateForm");
    const nameInput = document.getElementById("departmentName");
    const nameError = document.getElementById("departmentNameError");
    const formMessage = document.getElementById("formMessage");
    const submitButton = document.getElementById("createDepartmentButton");

    form.addEventListener("submit", async (event) => {
        event.preventDefault();

        // Reset previous messages
        nameError.textContent = "";
        nameInput.classList.remove("input-invalid");
        formMessage.className = "form-message";
        formMessage.textContent = "";

        const name = nameInput.value.trim();

        // Client-side validation
        if (!name) {
            nameError.textContent = "Department name is required.";
            nameInput.classList.add("input-invalid");
            nameInput.focus();
            return;
        }

        // Confirmation before submitting
        const confirmation = await Swal.fire({
            title: "Create Department?",
            text: "Are you sure you want to create this department?",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Yes, Create",
            cancelButtonText: "Cancel",
            confirmButtonColor: "#16a34a",
            cancelButtonColor: "#64748b",
            reverseButtons: true
        });

        // Stop if user cancels
        if (!confirmation.isConfirmed) {
            return;
        }

        submitButton.disabled = true;
        submitButton.innerHTML = `
            <i class="bi bi-arrow-repeat"></i>
            Creating...
        `;

        try {
            const response = await fetch("/api/department/store", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                },
                body: JSON.stringify({
                    name: name
                })
            });

            const responseText = await response.text();

            let result = {};
            try {
                result = responseText ? JSON.parse(responseText) : {};
            } catch {
                result = {};
            }

            if (!response.ok) {
                throw new Error(
                    result.message ||
                    result.title ||
                    "Failed to create department."
                );
            }

            await Swal.fire({
                title: "Created!",
                text: result.message || "Department created successfully.",
                icon: "success",
                confirmButtonColor: "#16a34a"
            }).then(() => {
                window.location.href = "/department";
            });
            

            form.reset();

        } catch (error) {
            await Swal.fire({
                title: "Error!",
                text: error.message || "Something went wrong. Please try again.",
                icon: "error",
                confirmButtonColor: "#dc2626"
            });

        } finally {
            submitButton.disabled = false;
            submitButton.innerHTML = `
                <i class="bi bi-plus-circle"></i>
                Create Department
            `;
        }
    });

});