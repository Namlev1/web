const signalR = require("@microsoft/signalr");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5170/ticketHub") // Replace {port} with your server's port
    .build();

// Handle incoming tickets
connection.on("ReceiveNewTicket", (ticket) => {
    console.log("New Ticket Received:");
    console.log("Ticket:", ticket);
});

// Handle changed tickets
connection.on("ReceiveStatusChange", (id, newStatus) => {
    console.log("Status changed:");
    console.log(`Ticket ID: ${id}`);
    console.log(`New status: ${newStatus}`);
});


// Handle incoming messages
connection.on("ReceiveNewComment", (ticketId, comment) => {
    console.log("New Comment Received:");
    console.log(`Ticket ID: ${ticketId}`);
    console.log(`Comment:`, comment);
});

// happens every 5 seconds
connection.on("ReceiveAllTasks", (tasks) => {
    console.log("Received tasks:", tasks);
    // Update your UI with the tasks
});

// chatting
connection.on("ReceiveMessage", (message) => {
    console.log("Received message:", message);
    // Update your UI with the tasks
});



// Start the connection
connection.start()
    .then(() => console.log("SignalR Connected"))
    .catch(err => console.error("Error while starting connection:", err));

