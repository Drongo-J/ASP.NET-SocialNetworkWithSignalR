"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start()
    .then(function () {
        console.log("Connected . . .");
        GetAllUsers();
    })
    .catch(function (error) {
        console.log(error.toString());
    });

connection.on("Connect", function (info) {
    //console.log(info);
    var li = document.createElement(li);
    li.innerHtml = `<span style="color: springgreen; ">${info}</span>`;
    document.getElementById("messageList").appendChild(li);
    GetAllUsers();
});

connection.on("Disconnect", function (info) {
    //console.log(info);
    var li = document.createElement(li);
    li.innerHtml = `<span style="color: red; ">${info}</span>`;
    document.getElementById("messageList").appendChild(li);
    GetAllUsers();
});