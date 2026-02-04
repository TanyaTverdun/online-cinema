function initDashboardCharts(config) {
    const accentRed = '#e50914';
    const mutedGray = '#9a9a9a';
    const cardBg = '#181818';
    const gridLineColor = 'rgba(255, 255, 255, 0.05)';

    Chart.defaults.color = mutedGray;
    Chart.defaults.font.family = "'Segoe UI', 'Roboto', sans-serif";

    const defaultScales = {
        y: {
            grid: { color: gridLineColor },
            border: { display: false },
            ticks: { padding: 10 }
        },
        x: {
            grid: { display: false },
            ticks: { padding: 10 }
        }
    };

    // 1. Графік доходу (Line)
    const revCtx = document.getElementById('revenueChart');
    if (revCtx) {
        new Chart(revCtx, {
            type: 'line',
            data: {
                labels: config.daysLabels,
                datasets: [{
                    label: 'Дохід (грн)',
                    data: config.dailyRevenue,
                    borderColor: accentRed,
                    backgroundColor: 'rgba(229, 9, 20, 0.1)',
                    fill: true,
                    tension: 0.4,
                    borderWidth: 3,
                    pointBackgroundColor: accentRed,
                    pointRadius: 4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: defaultScales
            }
        });
    }

    // 2. Топ фільмів (Bar)
    const movCtx = document.getElementById('moviesChart');
    if (movCtx) {
        new Chart(movCtx, {
            type: 'bar',
            data: {
                labels: config.movieLabels,
                datasets: [{
                    label: 'Прибуток',
                    data: config.movieRevenue,
                    backgroundColor: accentRed,
                    borderRadius: 5,
                    barThickness: 25
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: defaultScales
            }
        });
    }

    // 3. Топ снеків (Doughnut)
    const snackCtx = document.getElementById('snacksChart');
    if (snackCtx) {
        new Chart(snackCtx, {
            type: 'doughnut',
            data: {
                labels: config.snackLabels,
                datasets: [{
                    data: config.snackRevenue,
                    backgroundColor: [
                        '#ffd6cc',
                        '#ff8080',
                        '#ff0000',
                        '#800000',
                        '#330a00'
                    ],
                    borderWidth: 4,
                    borderColor: cardBg,
                    hoverOffset: 20
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '70%',
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            padding: 20,
                            usePointStyle: true,
                            color: '#FFFFFF'
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: (ctx) => ` ${ctx.label}: ${ctx.raw.toLocaleString()} ₴`
                        }
                    }
                }
            }
        });
    }
}