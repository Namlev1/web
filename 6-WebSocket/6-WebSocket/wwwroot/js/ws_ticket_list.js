
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5170/ticketHub")
    .build();

connection.start()
    .then(() => console.log("SignalR connected"))
    .catch(err => console.error("SignalR connection error:", err));

connection.on("ReceiveNewTicket", (ticket) => {
    $.notify("New ticket available!\nPlease refresh the page",{ position: "top center", className:"info" });
    console.log("New ticket available!");
});

// Handle changed ticket statuses
connection.on("ReceiveStatusChange", (id, newStatus) => {
    const row = document.querySelector(`#data-table tbody tr[data-id="${id}"]`);
    if (row) {
        // Find the second cell (status column) and update its text
        const statusCell = row.cells[1];
        statusCell.textContent = newStatus;
        const actions = row.cells[5];
        const toRemove = actions.querySelector(`#close`);
        actions.removeChild(toRemove);
        console.log(`Updated status for ID ${id} to ${newStatus}`);
    } else {
        console.warn(`Row with ID ${id} not found`);
    }
});

// Handle incoming comments
connection.on("ReceiveNewComment", (ticketId, comment) => {
    const row = document.querySelector(`#data-table tbody tr[data-id="${ticketId}"]`);
    if (row) {
        //row.notify("New comment available", "info");
        $(`[data-id="${ticketId}"] td`).eq(5).append('<div><b>New comment!</b></div>')
        console.log(`New comment for ticket ${ticketId}`);
    } else {
        console.warn(`Row with ID ${ticketId} not found`);
    }
});

