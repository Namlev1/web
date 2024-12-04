const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5170/ticketHub")
    .build();

connection.start()
    .then(() => console.log("SignalR connected"))
    .catch(err => console.error("SignalR connection error:", err));

// Get references to DOM elements
const messageInput = document.getElementById("messageInput");
const sendMessageButton = document.getElementById("sendMessageButton");
const messagesDiv = document.getElementById("messages");

connection.on("ReceiveMessage", (message) => {
    const messageElement = document.createElement("div");
    messageElement.textContent = message;
    messagesDiv.appendChild(messageElement);
    messagesDiv.scrollTop = messagesDiv.scrollHeight; // Auto-scroll to the bottom
});

sendMessageButton.addEventListener("click", function () {
    const message = messageInput.value.trim();
    if (message !== "") {
        connection.invoke("SendMessage", message)
            .catch(function (err) {
                console.error("Error sending message: " + err);
            });
        messageInput.value = ""; // Clear the input field after sending
    }
});

messageInput.addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        sendMessageButton.click();
    }
});