function initDashboardCharts(config) {
    const colors = {
        red: '#e50914', 
        yellow: '#ffc107', 
        cyan: '#0dcaf0',  
        text: '#9a9a9a',  
        grid: 'rgba(255, 255, 255, 0.05)', 
        cardBg: '#181818'
    };

    Chart.defaults.color = colors.text;
    Chart.defaults.font.family = "'Segoe UI', 'Roboto', sans-serif";
    Chart.defaults.borderColor = colors.grid;

    const commonScales = {
        y: {
            grid: { color: colors.grid, drawBorder: false },
            ticks: { padding: 10 }
        },
        x: {
            grid: { display: false },
            ticks: { padding: 10 }
        }
    };

    // графік заповненості
    const ctxOccupancy = document.getElementById('occupancyChart');
    if (ctxOccupancy) {
        new Chart(ctxOccupancy, {
            type: 'bar',
            data: {
                labels: config.occupancy.labels,
                datasets: [{
                    label: 'Заповненість',
                    data: config.occupancy.data,
                    backgroundColor: colors.red,
                    borderRadius: 4,
                    barThickness: 30
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        callbacks: {
                            label: (context) => ` ${context.raw}%`
                        }
                    }
                },
                scales: {
                    y: {
                        ...commonScales.y,
                        beginAtZero: true,
                        max: 100,
                        title: { display: true, text: 'Відсоток (%)' }
                    },
                    x: commonScales.x
                }
            }
        });
    }

    // графік популярних фільмів
    const ctxPopMovies = document.getElementById('popularMoviesChart');
    if (ctxPopMovies) {
        new Chart(ctxPopMovies, {
            type: 'bar',
            indexAxis: 'y',
            data: {
                labels: config.popularMovies.labels,
                datasets: [{
                    label: 'Продано квитків',
                    data: config.popularMovies.data,
                    backgroundColor: colors.yellow,
                    borderRadius: 4,
                    barThickness: 20
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false }
                },
                scales: {
                    x: { ...commonScales.y, grid: { display: false } },
                    y: { ...commonScales.x, grid: { color: colors.grid } }
                }
            }
        });
    }

    // графік не популярних фільмів
    const ctxLeastMovies = document.getElementById('leastMoviesChart');
    if (ctxLeastMovies) {
        new Chart(ctxLeastMovies, {
            type: 'bar',
            indexAxis: 'y',
            data: {
                labels: config.leastMovies.labels,
                datasets: [{
                    label: 'Продано квитків',
                    data: config.leastMovies.data,
                    backgroundColor: colors.cyan,
                    borderRadius: 4,
                    barThickness: 20
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false }
                },
                scales: {
                    x: { ...commonScales.y, grid: { display: false } },
                    y: { ...commonScales.x, grid: { color: colors.grid } }
                }
            }
        });
    }

    //графік снеків 
    const ctxLeastSnacks = document.getElementById('leastSnacksChart');
    if (ctxLeastSnacks) {
        new Chart(ctxLeastSnacks, {
            type: 'doughnut',
            data: {
                labels: config.leastSnacks.labels,
                datasets: [{
                    data: config.leastSnacks.data,
                    backgroundColor: [
                        '#6c757d',
                        '#495057',
                        '#343a40',
                        '#adb5bd',
                        '#ced4da'
                    ],
                    borderColor: colors.cardBg, 
                    borderWidth: 2,
                    hoverOffset: 10
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '70%',
                plugins: {
                    legend: {
                        position: 'right',
                        labels: {
                            usePointStyle: true,
                            padding: 20,
                            color: '#fff'
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: (ctx) => ` ${ctx.label}: ${ctx.raw} шт.`
                        }
                    }
                }
            }
        });
    }
}