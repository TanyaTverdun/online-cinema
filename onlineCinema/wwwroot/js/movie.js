$(document).ready(function () {
    MovieController.init();
});

const MovieController = {
    dataTable: null,

    init: function () {
        this.loadDataTable();
        this.registerEvents();
    },

    loadDataTable: function () {
        const table = $('#tblData');
        const url = table.data('load-url');

        this.dataTable = table.DataTable({
            "ajax": {
                "url": url,
                "type": "GET",
                "datatype": "json"
            },
            "autoWidth": false,
            "columns": [
                {
                    "data": "posterUrl",
                    "className": "py-3 ps-3",
                    "render": (data) => {
                        const imgUrl = data || '/images/no-poster.png';
                        return `<img src="${imgUrl}" alt="Постер" class="rounded border border-secondary"
                                     style="width: 50px; height: 75px; object-fit: cover;">`;
                    },
                    "width": "80px",
                    "orderable": false
                },
                {
                    "data": "title",
                    "render": (data, type, row) => {
                        return `
                            <div class="fw-bold">${data}</div>
                            <small class="opacity-75">${row.genreSummary}</small>
                        `;
                    },
                    "width": "30%"
                },
                { "data": "releaseYear", "width": "10%" },
                {
                    "data": "rating",
                    "width": "10%",
                    "render": (data) => {
                        if (data > 0) {
                            return `<span class="badge bg-warning text-dark border border-warning">
                                      <i class="bi bi-star-fill me-1"></i>${parseFloat(data).toFixed(1)}
                                    </span>`;
                        }
                        return `<span class="text-muted small">-</span>`;
                    }
                },
                {
                    "data": "status",
                    "render": (data) => {
                        
                        const statusMap = {
                            2: { text: 'Вийшов', class: 'success' },      
                            1: { text: 'Очікується', class: 'warning' }   
                        };

                        const state = statusMap[data] || { text: 'Невідомо', class: 'secondary' };

                        const extraClasses = state.class !== 'secondary'
                            ? `bg-opacity-10 text-${state.class} border border-${state.class}`
                            : '';

                        return `<span class="badge bg-${state.class} ${extraClasses}">${state.text}</span>`;
                    },
                    "width": "15%"
                },
                {
                    "data": "id",
                    "className": "text-end pe-3",
                    "render": (data) => {
                        return `
                            <div class="btn-group">
                                <a href="/Admin/Movie/Edit/${data}" class="btn btn-sm btn-outline-info" title="Редагувати">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <button type="button" class="btn btn-sm btn-outline-danger js-delete" data-id="${data}" title="Видалити">
                                    <i class="bi bi-trash"></i>
                                </button>
                            </div>
                        `;
                    },
                    "width": "15%",
                    "orderable": false
                }
            ],
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.13.7/i18n/uk.json",
                "emptyTable": "Фільмів не знайдено. Натисніть 'Додати новий фільм', щоб почати.",
                "searchPlaceholder": "Пошук фільмів..."
            },
            "order": [[2, "desc"]]
        });
    },

    registerEvents: function () {
        $('#tblData').on('click', '.js-delete', (e) => {
            const button = $(e.currentTarget);
            const id = button.data('id');

            if (confirm("Ви впевнені, що бажаєте архівувати/видалити цей фільм?")) {
                this.deleteMovie(id);
            }
        });
    },

    deleteMovie: function (id) {
        $.ajax({
            url: `/Admin/Movie/Delete/${id}`,
            type: 'DELETE',
            success: (data) => {
                if (data.success) {
                    this.dataTable.ajax.reload();
                } else {
                    alert(data.message);
                }
            },
            error: function () {
                alert("Сталася помилка при видаленні.");
            }
        });
    }
};