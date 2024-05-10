$(function () {
    /*
    Для взаимодействия с хабом ChatHub с помощью метода build() объекта HubConnectionBuilder
    создается объект hubConnection - объект подключения.
    Метод withUrl устанавливает адрес, по котору приложение будет обращаться к хабу.
    Поскольку ChatHub на сервере сопоставляется с адресом "/chat", то именно этот адрес и передается в withUrl.
    */

    

    $('#chatBody').hide();
    $('#loginBlock').show();
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();
    /*
        Если адрес сервера и адрес клиента не будут совпадать, то потребуется настроить поддержку CORS.
        В данном случае серверная и клиентская части работают в рамках одного приложения, поэтому настройка CORS не требуется.
    */

    // Метод hubConnection.on устанавливает функцию на стороне клиента,
    // которая будет получать данные от сервера (хаба)
    hubConnection.on("AddMessage", function (name, message) {
        // Добавление сообщений на веб-страницу
        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: <i>' + htmlEncode(message) + '</i></p>');
    });

    // Функция, вызываемая сервером при подключении нового пользователя
    hubConnection.on("Connected", function (id, userName, allUsers, allMessages) {

        $('#loginBlock').hide();
        $('#chatBody').show();
        // установка в скрытых полях имени и id текущего пользователя
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>Добро пожаловать, ' + userName + '</h3>');

        // Добавление всех пользователей
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].connectionId, allUsers[i].name);
        }

        for (i = 0; i < allMessages.length; i++) {
            AddMessage(allMessages[i].from, allMessages[i].text);
        }
    });

    // Функция, вызываемая сервером для добавления нового пользователя
    hubConnection.on("NewUserConnected", function (id, name) {

        AddUser(id, name);
    });

    // Функция, вызываемая сервером для удаления пользователя
    hubConnection.on("UserDisconnected", function (id, userName) {

        $('#' + id).remove();
    });


    // Добавляем обработчик на кнопку "Выйти"
    //$('#disconnect').click(function () {
    //    // Отправляем сигнал на сервер для отключения
    //    alert("Test");
    //    hubConnection.invoke("Disconnect")
    //        .catch(function (err) {
    //            console.error(err.toString());
    //        });
    //});
    // Открываем соединение с сервером.
    // Если подключение к хабу успешно установлено,
    // то сработает метод then, чтобы выполнить некоторые действия.
    // Если же в процессе подключения к серверу возникнет ошибка,
    // то сработает функция, которая передается в метод catch и которая получит данные об ошибке.
    hubConnection.start()
        .then(function () {
            // отправка сообщения
            $('#sendmessage').click(function () {
                // Вызываем у хаба метод Send и передаем ему данные.
                // В случае, если при отправке возникнет ошибка,
                // то сработает функция, которая передается в метод catch().
                hubConnection.invoke("Send", $('#username').val(), $('#message').val())
                    .catch(function (err) {
                        return console.error(err.toString());
                    });
                $('#message').val('');
            });

            // обработка логина
            $("#btnLogin").click(function () {
                let name = $("#txtUserName").val();
                if (name.length > 0) {
                    // Вызываем у хаба метод Connect и передаем ему данные.
                    // В случае, если при отправке возникнет ошибка,
                    // то сработает функция, которая передается в метод catch().
                    hubConnection.invoke("Connect", name)
                        .catch(function (err) {
                            return console.error(err.toString());
                        });
                }
                else {
                    alert("Введите имя");
                }
            });
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
});

// Кодирование тегов
function htmlEncode(value) {
    let encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//Добавление нового пользователя
function AddUser(id, name) {

    let userId = $('#hdId').val();

    if (name !== undefined && userId !== id) {

        $("#chatusers").append('<li id="' + id + '"><b>' + name + '</b></li>');
    }
}

function AddMessage(name, message) {
    // Добавление сообщений на веб-страницу
    $('#chatroom').append('<p><b>' + htmlEncode(name)
        + '</b>: <i>' + htmlEncode(message) + '</i></p>');
}