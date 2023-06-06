// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetAllUsers() {
    $.ajax({
        url: "/Home/GetAllUsers",
        method: "GET",
        success: function (data) {
            console.log(data);
            let content = "";
            for (var i = 0; i < data.length; i++) {
                let style = 'border: 5px solid ';
                if (data[i].isOnline) {
                    style += 'springgreen;';
                }
                else {
                    style += 'red;';
                }

                let item = 
                `<div class='card' style='width=14rem; margin:5px;'>
                    <img style='${style}width='220px; height:220px;' src='/images/${data[i].imageUrl}'>
                    <div class='card-body'></div>
                    <h5 class='card-title'>${data[i].userName}</h5>
                    <p class='card-text'>${data[i].userName}</pssssssss>
                </div>`

                content += item;
            }

            $("#allUsers").html(content);
        }
    })
}