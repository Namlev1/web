// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// book charts configuration



if (document.getElementById("bookLoansChart") != null) {

    var ctx_loans = document.getElementById("bookLoansChart").getContext("2d");

    document.addEventListener('DOMContentLoaded', async function () {
        try {
            // Fetch the JSON data from the endpoint
            const response = await fetch('/api/data/loans');

            if (!response.ok) {
                throw new Error('Failed to load loans data');
            }

            // Parse the JSON response
            const loans = await response.json();

            // Use the JSON data as needed

            // Assuming `loans` is the array of loan objects from loans.json

            // Step 1: Initialize month counters for the last 12 months
            const monthCounts = {};
            const currentDate = new Date();
            for (let i = 0; i < 12; i++) {
                const date = new Date(currentDate.getFullYear(), currentDate.getMonth() - i, 1);
                const monthKey = `${date.getFullYear()}-${date.getMonth() + 1}`;  // Format as "YYYY-M"
                monthCounts[monthKey] = 0;
            }

            // Step 2: Count loans per month in the last 12 months
            loans.forEach(loan => {
                const loanDate = new Date(loan.LoanDate);
                const loanMonthKey = `${loanDate.getFullYear()}-${loanDate.getMonth() + 1}`;

                if (monthCounts.hasOwnProperty(loanMonthKey)) {
                    monthCounts[loanMonthKey]++;
                }
            });

            // Step 3: Prepare labels and data for the chart
            const labels = Object.keys(monthCounts).reverse();  // Reverse to start from the oldest month
            const data = Object.values(monthCounts).reverse();

            // Create chart

            var gradientFill = ctx_loans.createLinearGradient(0, 500, 0,200);
            gradientFill.addColorStop(0, "rgba(128, 182, 244, 0)");
            gradientFill.addColorStop(1, hexToRGB('#2CA8FF', 0.6));

            new Chart(ctx_loans, {
                type: 'bar',  // or 'bar' if you prefer
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Number of Loans',
                        data: data,
                        backgroundColor: gradientFill,
                        borderColor: "#2CA8FF",
                        pointBorderColor: "#FFF",
                        pointBackgroundColor: "#2CA8FF",
                        borderWidth: 1,
                        fill: true
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true,
                        },
                        x: {
                            border: {
                                display: false
                            },
                            grid: {
                                display: false
                            }
                        }
                    },
                    plugins: {
                        legend: {
                            display: false,
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: 'Nubmer of loaned books in the past 12 months',
                            font: {
                                size: 16
                            }
                        }
                        
                    },


                }
            });


        } catch (error) {
            console.error('Error:', error);
        }
    });

}

if (document.getElementById("mostLoanedBooks") != null) {

    var ctx_books = document.getElementById("mostLoanedBooks").getContext("2d");

    document.addEventListener('DOMContentLoaded', async function () {
        try {
            // Fetch the JSON data from the endpoint
            const response2 = await fetch('/api/data/books');

            if (!response2.ok) {
                throw new Error('Failed to load books data');
            }

            const response3 = await fetch('/api/data/loans');

            if (!response3.ok) {
                throw new Error('Failed to load loans data');
            }

            // Parse the JSON response
            const books = await response2.json();
            const loans = await response3.json();

            // Use the JSON data as needed

            // Step 1: Count loans for each BookId
            const loanCounts = {};

            loans.forEach(loan => {
                if (loanCounts[loan.BookId]) {
                    loanCounts[loan.BookId]++;
                } else {
                    loanCounts[loan.BookId] = 1;
                }
            });

            // Step 2: Sort by the number of loans and get the top 10 BookIds
            const top10Books = Object.entries(loanCounts)
                .sort((a, b) => b[1] - a[1])  // Sort by count in descending order
                .slice(0, 10);                 // Take top 10 entries

            // top10Books will be an array like [[BookId, count], [BookId, count], ...]

            // Step 1: Create a mapping of BookId to Title
            const bookTitles = {};
            books.forEach(book => {
                bookTitles[book.Id] = book.Title;
            });

            // Step 2: Prepare labels and data arrays
            const labels = top10Books.map(([bookId]) => bookTitles[bookId] || 'Unknown Book');
            const data = top10Books.map(([_, count]) => count);

            var gradientFill = ctx_loans.createLinearGradient(0, 500, 0, 200);
            gradientFill.addColorStop(0, "rgba(204, 178, 255, 0)");
            gradientFill.addColorStop(1, hexToRGB('#ccb2ff', 0.6));

            new Chart(ctx_books, {
                type: 'bar',
                data: {
                    labels: labels,  // Titles of top 10 most loaned books
                    datasets: [{
                        label: 'Number of Loans',
                        data: data,  // Loan count for each book
                        backgroundColor: gradientFill,
                        borderColor: 'rgba(153, 102, 255, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true,
                            title: {
                                display: true,
                                text: 'Number of Loans'
                            
                            },
                            ticks: {
                                // forces step size to be 50 units
                                stepSize: 1
                            }
                        },
                        x: {
                            title: {
                                display: true,
                                text: 'Book Title',
                                
                            },
                            grid: {
                                display: false
                            }
                        }
                    },
                    plugins: {
                        legend: {
                            display: false,
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: 'Most loaned books',
                            font: {
                                size: 16
                            }
                        }

                    },
                }
            });

        } catch (error) {
            console.error('Error:', error);
        }
    });

}