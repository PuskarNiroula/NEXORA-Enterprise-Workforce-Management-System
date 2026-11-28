
const API_BASE_URL = "/api/department/";

const tableBody = document.getElementById("departmentTableBody");
const searchInput = document.getElementById("departmentSearch");
const visibleCount = document.getElementById("visibleCount");

let departments = [];

//function to get department
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

        const response = await fetch((API_BASE_URL+"Index"), {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error("Failed to fetch departments.");
        }

        const result = await response.json();

        // Supports both [] and { data: [] } responses.
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
                        <button class="btn-create"
                                onclick="getDepartments()">
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

        const id = department.id ?? department.Id;
        const name = department.name ?? department.Name ?? "";
        const status = department.status ?? department.Status;

        const isActive = status === true;

        return `
            <tr class="department-row">

                <td>
                    <span class="department-id">#${escapeHtml(id)}</span>
                </td>

                <td>
                    <div class="department-name">
                        <div class="department-avatar">
                            ${escapeHtml(name)}
                        </div>
                      
                    </div>
                </td>

                <td>
                    <span class="status-badge ${isActive
            ? "status-active"
            : "status-inactive"}">

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

                        <button type="button"
                                class="btn-action ${isActive
            ? "btn-deactivate"
            : "btn-activate"}"
                                onclick="toggleDepartmentStatus('${escapeHtml(id)}', ${!isActive})">

                            ${isActive ? "Deactivate" : "Activate"}

                        </button>

                    </div>
                </td>

            </tr>
        `;
    }).join("");
}


// Search departments
searchInput.addEventListener("input", function () {
    renderDepartments(this.value.trim());
});


// POST: Activate / Deactivate department
async function toggleDepartmentStatus(id, newStatus) {

    const action = newStatus ? "activate" : "deactivate";

    if (!confirm(`Are you sure you want to ${action} this department?`)) {
        return;
    }

    try {
        const response = await fetch(
            `${API_BASE_URL}/${encodeURIComponent(id)}/status`,
            {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                },
                body: JSON.stringify({
                    status: newStatus
                })
            }
        );

        if (!response.ok) {
            const error = await response.json().catch(() => null);

            throw new Error(
                error?.message ?? `Unable to ${action} department.`
            );
        }

        // Refresh list after successful update
        await getDepartments();

    } catch (error) {
        console.error("Department status error:", error);
        alert(error.message || "Something went wrong.");
    }
}


// Escape API values before inserting into HTML
function escapeHtml(value) {
    return String(value ?? "").replace(/[&<>"']/g, char => ({
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': "&quot;",
        "'": "&#39;"
    })[char]);
}


// Initialize
document.addEventListener("DOMContentLoaded", getDepartments);
