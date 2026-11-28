
const API_BASE_URL = "/api/department/";

const tableBody = document.getElementById("departmentTableBody");
const searchInput = document.getElementById("departmentSearch");
const visibleCount = document.getElementById("visibleCount");

let departments = [];

async function getDepartments() {
    try {
        tableBody.innerHTML = `
            <tr>
                <td colspan="4">
                    <div class="department-loading">
                        <div class="loading-spinner"></div>
                        <span>Loading departments...</span>
                    </div>
                </td>
            </tr>
        `;

        const response = await fetch(`${API_BASE_URL}Index`, {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error("Failed to fetch departments.");
        }

        const result = await response.json();

        departments = Array.isArray(result)
            ? result
            : (result.data ?? []);

        renderDepartments();

    } catch (error) {
        console.error("Department fetch error:", error);

        tableBody.innerHTML = `
            <tr>
                <td colspan="4">
                    <div class="department-empty">
                        <div class="empty-icon">!</div>
                        <h3>Unable to load departments</h3>
                        <p>Please check your connection and try again.</p>
                        <button class="btn-create" onclick="getDepartments()">
                            Try Again
                        </button>
                    </div>
                </td>
            </tr>
        `;

        visibleCount.textContent = "0";
    }
}

function renderDepartments(searchValue = "") {
    let sn =1;
    const filteredDepartments = departments.filter(department =>
        (department.name ?? department.Name ?? "")
            .toLowerCase()
            .includes(searchValue.toLowerCase())
    );

    visibleCount.textContent = filteredDepartments.length;

    if (filteredDepartments.length === 0) {
        tableBody.innerHTML = `
            <tr>
                <td colspan="4">
                    <div class="department-empty">
                        <div class="empty-icon">▤</div>
                        <h3>No departments found</h3>
                        <p>There are no departments matching your search.</p>
                    </div>
                </td>
            </tr>
        `;
        return;
    }

    tableBody.innerHTML = filteredDepartments.map(department => {
        const id = department.departmentId ?? department.DepartmentId ?? department.id ?? department.Id;
        const name = department.name ?? department.Name ?? "";
        const status = department.status ?? department.Status;

        const isActive = Number(status) === 1;

        return `
            <tr class="department-row">
                <td>
                    <span class="department-id">#${escapeHtml(sn++)}</span>
                </td>

                <td>
                    <div class="department-name">
                        <div class="department-avatar">
                            ${escapeHtml(name)}
                        </div>
                    </div>
                </td>

                <td>
                    <span class="status-badge ${isActive ? "status-active" : "status-inactive"}">
                        <span class="status-dot"></span>
                        ${isActive ? "Active" : "Inactive"}
                    </span>
                </td>

                <td>
                    <div class="department-actions">
                        <a href="/Departments/Edit/${encodeURIComponent(id)}"
                           class="btn-action btn-edit">
                            <span>✎</span>
                            Edit
                        </a>

                        ${
            isActive
                ? `<button type="button"
                                           class="btn-action btn-deactivate"
                                           onclick="deactivateDepartment('${escapeHtml(id)}')">
                                       Deactivate
                                   </button>`
                : `<button type="button"
                                           class="btn-action btn-activate"
                                           onclick="activateDepartment('${escapeHtml(id)}')">
                                       Activate
                                   </button>`
        }
                    </div>
                </td>
            </tr>
        `;
    }).join("");
}

searchInput.addEventListener("input", function () {
    renderDepartments(this.value.trim());
});

async function activateDepartment(id) {
    const result = await Swal.fire({
        title: "Activate Department?",
        text: "Are you sure you want to activate this department?",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Yes, Activate",
        cancelButtonText: "Cancel",
        confirmButtonColor: "#198754",
        cancelButtonColor: "#6c757d"
    });

    if (!result.isConfirmed) return;

    try {
        Swal.fire({
            title: "Activating...",
            text: "Please wait.",
            allowOutsideClick: false,
            allowEscapeKey: false,
            didOpen: () => Swal.showLoading()
        });

        const response = await fetch(
            `${API_BASE_URL}Activate/${encodeURIComponent(id)}`,
            {
                method: "PATCH",
                headers: {
                    "Accept": "application/json"
                }
            }
        );

        if (!response.ok) {
            const error = await response.json().catch(() => null);
            throw new Error(error?.message ?? "Unable to activate department.");
        }

        await Swal.fire({
            title: "Activated!",
            text: "Department has been activated successfully.",
            icon: "success",
            confirmButtonColor: "#198754"
        });

        await getDepartments();

    } catch (error) {
        console.error("Department activation error:", error);

        Swal.fire({
            title: "Error!",
            text: error.message || "Something went wrong.",
            icon: "error",
            confirmButtonColor: "#dc3545"
        });
    }
}

async function deactivateDepartment(id) {
    const result = await Swal.fire({
        title: "Deactivate Department?",
        text: "Are you sure you want to deactivate this department?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, Deactivate",
        cancelButtonText: "Cancel",
        confirmButtonColor: "#dc3545",
        cancelButtonColor: "#6c757d"
    });

    if (!result.isConfirmed) return;

    try {
        Swal.fire({
            title: "Deactivating...",
            text: "Please wait.",
            allowOutsideClick: false,
            allowEscapeKey: false,
            didOpen: () => Swal.showLoading()
        });

        const response = await fetch(
            `${API_BASE_URL}Deactivate/${encodeURIComponent(id)}`,
            {
                method: "PATCH",
                headers: {
                    "Accept": "application/json"
                }
            }
        );

        if (!response.ok) {
            const error = await response.json().catch(() => null);
            throw new Error(error?.message ?? "Unable to deactivate department.");
        }

        await Swal.fire({
            title: "Deactivated!",
            text: "Department has been deactivated successfully.",
            icon: "success",
            confirmButtonColor: "#198754"
        });

        await getDepartments();

    } catch (error) {
        console.error("Department deactivation error:", error);

        Swal.fire({
            title: "Error!",
            text: error.message || "Something went wrong.",
            icon: "error",
            confirmButtonColor: "#dc3545"
        });
    }
}

function escapeHtml(value) {
    return String(value ?? "").replace(/[&<>"']/g, char => ({
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': "&quot;",
        "'": "&#39;"
    })[char]);
}

document.addEventListener("DOMContentLoaded", getDepartments);
