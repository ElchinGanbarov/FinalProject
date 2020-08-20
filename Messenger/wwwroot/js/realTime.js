"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/realTimeHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//receive msg
connection.on("ReceiveMessage", function (user, message) {
    //current-user-id-msg

    if (user == document.getElementById("current-user-id-msg").textContent) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = /*user+"says"+*/ msg;
        alert(encodedMsg);
    }

    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = /*user+"says"+*/ msg;
    //alert(encodedMsg);
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("messagesList").appendChild(li);

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("msg-receiver-user-id").textContent;
    var message = document.getElementById("send-message-input").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

//Send private message
$('[data-send-private-message]').click(function (e) {
    e.preventDefault();

    alert($(this).attr("data-id"))

    let receiverId = $(this).attr("data-id");

    //$.ajax({
    //    url: '/chat/update',
    //    type: "POST",
    //    dataType: "json",
    //    data:
    //    {
    //        name: Name,
    //        surname: Surname,
    //        phone: Phone,
    //        birthday: Birthday,
    //        email: Email,
    //        website: Website,
    //        address: Address
    //    },
    //    success: function (res) {
    //        $(".email").text(Email);
    //        $(".profilename").text(Name + " " + Surname);
    //        $(".birthday").text(Birthday);
    //        $(".phone").text(Phone);
    //        $(".website").text(Website);
    //        $(".address").text(Address);
    //        $.toast({
    //            heading: 'Info',
    //            text: "Account Details Updated Successfully",
    //            icon: 'info',
    //            loader: true,
    //            bgColor: '#3988ff',
    //            loaderBg: '#f7d40d',
    //            position: 'top-right',
    //            hideAfter: 4000
    //        });
    //        $(".onchangesubmit").prop("disabled", true);
    //    }
    //});
});