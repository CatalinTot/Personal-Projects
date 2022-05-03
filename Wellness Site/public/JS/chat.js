let user = JSON.parse(currentUser);
const chat = document.querySelector('.chat-div');
const openChat = document.querySelector('.open-chat');
const arrow = document.querySelector('.fa-arrow-up');
const send = document.getElementById('send');

openChat.addEventListener('click', () => {
    chat.classList.toggle('chat-heigth');
    arrow.classList.toggle('rotate-arrow');
})

const fadeTime = 150; // ms
const typingTimerLength = 400; // ms

// Initialize variables
const messages = document.querySelector('.messages-area'); // Messages area
const inputMessage = document.querySelector('.message-input'); // Input message input box
const socket = io();

// Prompt for setting a username
let username;
let connected = true;
let typing = false;
let lastTypingTime;

// Sends a chat message
const sendMessage = () => {
    let username = user.username;
    let message = inputMessage.value;
    // Prevent markup from being injected into the message
    message = cleanInput(message);
    // if there is a non-empty message and a socket connection
    if (message) {
        inputMessage.value = '';
        addChatMessage({
            username,
            message
        });
        // tell server to execute 'new message' and send along one parameter
        socket.emit('new message', `${username}  ${message}`);

    }
}

// Adds the visual chat message to the message list
const addChatMessage = (data, options) => {
    
    let messageBodyDiv;
    let usernameDiv;
    if (data.message !== 'Someone is typing...') {

        if (!data.username) {
            const newUsername = data.message.substr(0, data.message.indexOf(' '));
            const newMessage = data.message.substr(data.message.indexOf(' ') + 1);

            messageBodyDiv = document.createElement('p');
            messageBodyDiv.classList.add('text-black', 'fst-italic', 'm-0')
            messageBodyDiv.innerText = newMessage;

            usernameDiv = document.createElement('p');
            usernameDiv.classList.add('username', 'text-black', 'm-0');
            usernameDiv.innerText = newUsername;

        } else {
            usernameDiv = document.createElement('p');
            usernameDiv.classList.add('username', 'text-black', 'm-0');
            usernameDiv.innerText = data.username;

            messageBodyDiv = document.createElement('p');
            messageBodyDiv.classList.add('text-black', 'fst-italic', 'm-0')
            messageBodyDiv.innerText = data.message;
        }
        // const typingClass = data.typing ? 'typing' : 'not-typing';
        const messageDiv = document.createElement('p');
        messageDiv.classList.add('message', 'm-0', 'border-bottom');
        messageDiv.append(usernameDiv);
        messageDiv.append(messageBodyDiv);
        addMessageElement(messageDiv, options);
    } else {
        messageBodyDiv = document.createElement('p');
        messageBodyDiv.classList.add('text-black', 'fst-italic', 'm-0')
        messageBodyDiv.innerText = data.message;

        // const typingClass = data.typing ? 'typing' : 'not-typing';
        const messageDiv = document.createElement('p');
        messageDiv.classList.add('message', 'm-0', 'border-bottom', 'typing');
        messageDiv.append(messageBodyDiv);
        addMessageElement(messageDiv, options);
    }
}

// Adds the visual chat typing message
const addChatTyping = (data) => {
    data.typing = true;
    data.message = 'Someone is typing...';
    addChatMessage(data);
}

// Removes the visual chat typing message
const removeChatTyping = (data) => {
    if (document.querySelector('.typing') != null)
        document.querySelector('.typing').remove();
}

const addMessageElement = (el, options) => {

    if (!options) {
        options = {};
    }
    if (typeof options.fade === 'undefined') {
        options.fade = true;
    }
    if (typeof options.prepend === 'undefined') {
        options.prepend = false;
    }
    // if (options.fade) {
    //     el.hide().fadeIn(fadeTime);
    // }
    if (options.prepend) {
        messages.prepend(el);
    } else {
        messages.append(el);
    }

    messages.scrollTop = messages.scrollHeight;
}

const cleanInput = (input) => {
    const emptyElement = document.createElement('div');
    emptyElement.innerHTML = input;
    return emptyElement.innerHTML;
}

// Updates the typing event
const updateTyping = () => {
    if (!typing) {
        typing = true;
        socket.emit('typing');
    }
    lastTypingTime = (new Date()).getTime();

    setTimeout(() => {
        const typingTimer = (new Date()).getTime();
        const timeDiff = typingTimer - lastTypingTime;
        if (timeDiff >= typingTimerLength && typing) {
            socket.emit('stop typing');
            typing = false;
        }
    }, typingTimerLength);
}

// Send button event
send.addEventListener('click', () => {
    sendMessage();
    socket.emit('stop typing');
    typing = false;
})

inputMessage.addEventListener('input', () => {
    updateTyping();
});

// Focus input when clicking on the message input's border
inputMessage.click(() => {
    inputMessage.focus();
});

// Socket events
// Whenever the server emits 'new message', update the chat body
socket.on('new message', (data) => {
    addChatMessage(data);
});

// Whenever the server emits 'typing', show the typing message
socket.on('typing', (data) => {
    addChatTyping(data);
});

// Whenever the server emits 'stop typing', kill the typing message
socket.on('stop typing', (data) => {
    removeChatTyping(data);
});