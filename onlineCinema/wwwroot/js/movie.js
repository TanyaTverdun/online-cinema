var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Movie/GetAll"
        },
        "autoWidth": false, 
        "columns": [
            {
                "data": "posterUrl",
                "className": "py-3 ps-3",
                "render": function (data) {
                    var imgUrl = data ? data : '/images/no-poster.png';
                    return `<img src="${imgUrl}" alt="Poster" class="rounded border border-secondary"
                                 style="width: 50px; height: 75px; object-fit: cover;">`;
                },
                "width": "60px",
                "orderable": false
            },
            {
                "data": "title",
                "render": function (data, type, row) {
                    return `
                        <div class="fw-bold text-white">${data}</div>
                        <small class="text-white opacity-75">${row.genreSummary}</small>
                    `;
                },
                "width": "35%"
            },
            {
                "data": "releaseYear",
                "className": "text-white",
                "width": "10%"
            },
            {
                "data": "rating",
                "className": "text-white",
                "width": "10%",
                "render": function (data) {
                   
                    if (!data || data === 0) {
                        return `<span class="text-muted small">-</span>`;
                    }
                    return `<span class="badge bg-warning text-dark border border-warning"><i class="bi bi-star-fill me-1"></i>${parseFloat(data).toFixed(1)}</span>`;
                }
            },
            {
                "data": "status",
                "render": function (data) {
                    if (data === 2) {
                        return `<span class="badge bg-success bg-opacity-10 text-success border border-success">Released</span>`;
                    } else if (data === 1) {
                        return `<span class="badge bg-warning bg-opacity-10 text-warning border border-warning">Coming Soon</span>`;
                    } else {
                        return `<span class="badge bg-secondary">Unknown</span>`;
                    }
                },
                "width": "15%"
            },
            {
                "data": "id",
                "className": "text-end pe-3",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/Admin/Movie/Edit/${data}" class="btn btn-sm btn-outline-info">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onClick=Delete('/Admin/Movie/Delete/${data}') class="btn btn-sm btn-outline-danger" style="cursor:pointer;">
                                <i class="bi bi-trash"></i>
                            </a>
                        </div>
                    `
                },
                "width": "10%",
                "orderable": false
            }
        ],
        "language": {
            "emptyTable": "No movies found. Click 'Add New Movie' to start.",
            "search": "", 
            "searchPlaceholder": "Search movies...",
            "lengthMenu": "Show _MENU_ entries"
        },
        "order": [[2, "desc"]] 
    });
}

function Delete(url) {
    if (confirm("Are you sure you want to archive this movie?")) {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();
                }
                else {
                    alert(data.message);
                }
            }
        });
    }
}