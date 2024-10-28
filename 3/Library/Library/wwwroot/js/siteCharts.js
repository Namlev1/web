// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// book charts configuration

const ctx = document.getElementById('bookChart').getContext("2d");

var barGradientFill = ctx.createLinearGradient(0, 200, 0, 50);
barGradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
barGradientFill.addColorStop(1, "rgba(255, 255, 255, 0.24)");

new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"],
        datasets: [{
            label: 'Number of borrowed books',
            data: [50, 150, 100, 190, 130, 90, 150, 160, 120, 140, 190, 95],
            backgroundColor: barGradientFill,
            borderWidth: 2,
        }]
    },
    options: {
        plugins: {
            // z jakiegoś powodu nie działa
            title: {
                display: true,
                color: "#FFFFFF",
                text: 'Number of borrowed books'
            }
        },
        layout: {
            padding: {
                left: 20,
                right: 20,
                top: 0,
                bottom: 0
            }
        },
        maintainAspectRatio: false,
        tooltips: {
            backgroundColor: '#fff',
            titleFontColor: '#333',
            bodyFontColor: '#666',
            bodySpacing: 4,
            xPadding: 12,
            mode: "nearest",
            intersect: 0,
            position: "nearest"
        },
        legend: {
            position: "bottom",
            fillStyle: "#FFF",
            display: false
        },
        scales: {
            yAxes: [{
                ticks: {
                    fontColor: "rgba(255,255,255,0.4)",
                    fontStyle: "bold",
                    beginAtZero: true,
                    maxTicksLimit: 5,
                    padding: 10
                },
                gridLines: {
                    drawTicks: true,
                    drawBorder: false,
                    display: true,
                    color: "rgba(255,255,255,0.1)",
                    zeroLineColor: "transparent"
                }

            }],
            xAxes: [{
                gridLines: {
                    zeroLineColor: "transparent",
                    display: false,

                },
                ticks: {
                    padding: 10,
                    fontColor: "rgba(255,255,255,0.4)",
                    fontStyle: "bold"
                }
            }]
        }
    }
});
