﻿"use strict"

//import { signalR } from "../lib/signalr/dist/browser/signalr";

$(document).ready(function () {
    //Privacy Dropdown & Security checkboxs Js
    $('[data-privacy-profile-picture]').on('click', function () {
        $('[data-privacy-profile-picture-text]').text($(this).text());
        $('button[data-privacy-profile-picture-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-status]').on('click', function () {
        $('[data-privacy-status-text]').text($(this).text());
        $('button[data-privacy-status-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-last-seen]').on('click', function () {
        $('[data-privacy-last-seen-text]').text($(this).text());
        $('button[data-privacy-last-seen-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-phone]').on('click', function () {
        $('[data-privacy-phone-text]').text($(this).text());
        $('button[data-privacy-phone-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-birthday]').on('click', function () {
        $('[data-privacy-birthday-text]').text($(this).text());
        $('button[data-privacy-birthday-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-address]').on('click', function () {
        $('[data-privacy-address-text]').text($(this).text());
        $('button[data-privacy-address-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-social-links]').on('click', function () {
        $('[data-privacy-social-links-text]').text($(this).text());
        $('button[data-privacy-social-links-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('#privacy-accept-messages').on('click', function () {
        $(".changeprivacy").prop("disabled", false)
    });
    //security
    $('#security-tfa').on('click', function () {
        $(".changesecurity").prop("disabled", false)
    });
    $('#security-login-alerts').on('click', function () {
        $(".changesecurity").prop("disabled", false)
    });
    //Privacy Dropdown & Security checkboxs Js END

    //====================

    //Disabled Removed Account
    $(document).on("keydown", ".onchange", function (e) {
        $(".onchangesubmit").prop("disabled", false)
    });
    $(document).on("keydown", ".onchanges", function (e) {
        $(".onchangesocial").prop("disabled", false)
    });
    $(document).on("keydown", ".chpassword", function (e) {
        $(".changepassword").prop("disabled", false)
    });
    $(document).on("change", ".onchangess", function (e) {
        $(".onchangesubmit").prop("disabled", false)
    });

    //Update Profile Img 
    $(".deletedshow").click(function () {
        $(".deletedshow").removeClass("show");
    });
    if ($("input[name='AccountDetailViewModel.Image']").length) {
        $("input[name='AccountDetailViewModel.Image']").change(function () {
            var fileinput = document.getElementById('AccountDetailViewModel_Image');
            var formData = new FormData();
            for (var i = 0; i < fileinput.files.length; i++) {
              
                formData.append('file', fileinput.files[i]);

                $.ajax({
                    url: '/accountdetail/profileimgupload',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    dataType: "json",
                    success: function (response) {
                        console.log(response)

                        $(".avatar-img").attr('src', response.src)
                    }
                });
            }



        });
    }
    //Remove Image
    $(".profile-img-remove").click(function (e) {
        console.log("test");
        let elem = $(this).parents(".item");
        let href = $(this).attr("href");
        $.confirm({
            title: 'Remove Photo',
            content: 'Are you sure delete current profile photo?',
            buttons: {
                'Yes': {
                    btnClass: "btn-danger",
                    action: function () {
                        $.ajax({
                            url: href,
                            type: "post",
                            data: $(".avatar-img").attr("src"),
                            dataType: "json",
                            success: function (response) {
                                $(".avatar-img").attr("src", "https://localhost:44304/uploads/general_pack_NEW_glyph_profile-512.png");
                            }
                        });
                    }
                },
                'No': function () {
                }
            }
        });
        e.preventDefault();
    });
    //Update profile Img END

    //Sweet Alert View Photo
    $(".viewphoto").click(function (e) {

        Swal.fire({
            title: $(".fullnameprofilimage").text(),
            imageUrl: $(".avatar-img").attr("src"),
            imageWidth: 400,
            imageHeight: 400,
            imageAlt: $(".fullnameprofilimage").text()

        })
        e.preventDefault();
    })

    //====================================================================

    //Search Accounts with Autocomplete
    $("#search-accounts-input").autocomplete({
        //source: '/api/searchapi/searchaccount',
        source: '/friends/searchaccount',

        minLength: 2,
        select: function (event, ui) {
            event.preventDefault();
            $("#search-accounts-input").val(ui.item.label);
            $("#hidden-search-id").val(ui.item.id);

            SearchResultUserInfos(ui.item.id); //funtion show selected account's infos

            $("#search-accounts-input").val("");
            $("#hidden-search-id").val("");
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#search-accounts-input").val(ui.item.label);
        },
        html: true,
        open: function () {
            $("ul#ui-id-1").css({
                top: 110 + "px"
            });
        }
    })
        .autocomplete("instance")._renderItem = function (ul, item) {
            let img = "/uploads/default-profile-img.jpg";
            if (item.img != null) {
                img = "https://res.cloudinary.com/djmiksiim/v1/" + item.img;
            }
            return $("<li><div><div style='display: inline-block; border-radius: 50%; '><img style='width: 50px; height: 50px; object-fit: cover; border-radius: 50%' src='" + img + "'></div><span style='font-size: 16px;vertical-align: middle;'>" + item.value + "</span></div></li>").appendTo(ul);
        };

    //Get Search Result User Infos() Start
    function SearchResultUserInfos(selectedUserId) {

        if ($('#friendsTab li.active').length) {
            $('#friendsTab li.active').removeClass('active');
        }

        //show selected friend's details
        if ($('.friends-intro-wrapper').hasClass('d-flex')) {
            $('.friends-intro-wrapper').removeClass('d-flex').addClass('d-none');
            $('.selected-account-details').toggleClass('d-none')
        }
        //selected-account-details-img

        $.ajax({
            url: '/account/getaccountdatas',
            type: "POST",
            dataType: "json",
            data:
            {
                currentAccountId: $('#hidden-account-id').val(),
                searchedAccountId: selectedUserId
            },
            success: function (res) {
                let btnSendFriendRequest = $('.btn-send-friend-request');
                let btnRemoveFriend = $('.btn-remove-friend');
                let btnRejectFriendRequest = $('.btn-reject-friend-request');
                let btnAcceptFriendRequest = $('.btn-accept-friend-request');

                //id
                document.querySelector("#hidden-selected-account-id").textContent = res.id;

                //view own profile
                if (res.friendship == 4) {
                    if ($('.account-proccess-wrapper').hasClass('d-flex')) {
                        $('.account-proccess-wrapper').removeClass('d-flex');
                        $('.account-proccess-wrapper').addClass('d-none');
                    }
                }

                //view friend's profile
                if (res.friendship == 1) {
                    if ($('.account-proccess-wrapper').hasClass('d-flex') == false) {
                        $('.account-proccess-wrapper').addClass('d-flex');
                    }
                    if (btnSendFriendRequest.hasClass('d-none') == false) {
                        btnSendFriendRequest.addClass('d-none')
                    }
                    if (btnAcceptFriendRequest.hasClass('d-none') == false) {
                        btnAcceptFriendRequest.addClass('d-none')
                    }
                    if (btnRemoveFriend.hasClass('d-none')) {
                        btnRemoveFriend.removeClass('d-none')
                    }
                    if (btnRejectFriendRequest.hasClass('d-none') == false) {
                        btnRejectFriendRequest.addClass('d-none')
                    }
                }

                //view public profile (not friend)
                if (res.friendship == 3) {
                    if ($('.account-proccess-wrapper').hasClass('d-flex') == false) {
                        $('.account-proccess-wrapper').addClass('d-flex');
                    }
                    if (btnSendFriendRequest.hasClass('d-none')) {
                        btnSendFriendRequest.removeClass('d-none')
                    }
                    if (btnRemoveFriend.hasClass('d-none') == false) {
                        btnRemoveFriend.addClass('d-none')
                    }
                    if (btnRejectFriendRequest.hasClass('d-none') == false) {
                        btnRejectFriendRequest.addClass('d-none')
                    }
                    if (btnAcceptFriendRequest.hasClass('d-none') == false) {
                        btnAcceptFriendRequest.addClass('d-none')
                    }
                }

                //new friendship request sendded account's profile (fromCurrentUser)
                if (res.friendship == 0 && res.isFriendRequestSender == true) {
                    if ($('.account-proccess-wrapper').hasClass('d-flex') == false) {
                        $('.account-proccess-wrapper').addClass('d-flex');
                    }
                    if (btnRemoveFriend.hasClass('d-none') == false) {
                        btnRemoveFriend.addClass('d-none')
                    }
                    if (btnSendFriendRequest.hasClass('d-none') == false) {
                        btnSendFriendRequest.addClass('d-none')
                    }
                    if (btnAcceptFriendRequest.hasClass('d-none') == false) {
                        btnAcceptFriendRequest.addClass('d-none')
                    }
                    if (btnRejectFriendRequest.hasClass('d-none')) {
                        btnRejectFriendRequest.removeClass('d-none')
                    }
                }

                //new friendship request received from other useer to current account (toCurrentUser)
                if (res.friendship == 0 && res.isFriendRequestSender == false) {
                    if ($('.account-proccess-wrapper').hasClass('d-flex') == false) {
                        $('.account-proccess-wrapper').addClass('d-flex');
                    }
                    if (btnRemoveFriend.hasClass('d-none') == false) {
                        btnRemoveFriend.addClass('d-none')
                    }
                    if (btnSendFriendRequest.hasClass('d-none') == false) {
                        btnSendFriendRequest.addClass('d-none')
                    }
                    if (btnRejectFriendRequest.hasClass('d-none') == false) {
                        btnRejectFriendRequest.addClass('d-none')
                    }
                    if (btnRejectFriendRequest.hasClass('d-none')) {
                        btnRejectFriendRequest.removeClass('d-none')
                    }
                    if (btnAcceptFriendRequest.hasClass('d-none')) {
                        btnAcceptFriendRequest.removeClass('d-none')
                    }
                }



                //img
                if (res.img != null) {
                    $(".selected-account-details-img").attr("src", "https://res.cloudinary.com/djmiksiim/v1/" + res.img)
                } else {
                    $(".selected-account-details-img").attr("src", "/uploads/default-profile-img.jpg")
                }
                //statusText
                if (res.statusText == null) {
                    document.querySelector(".selected-account-details-statusText").textContent = ""
                } else {
                    document.querySelector(".selected-account-details-statusText").innerHTML = '<i class="fas fa-hashtag" style="font-size:12px;margin-right:4px;" aria-hidden="true"></i>' + res.statusText
                }
                //website
                if (res.website == null) {
                    document.querySelector(".selected-account-details-website").textContent = ""
                } else {
                    document.querySelector(".selected-account-details-website").textContent = res.website
                }
                //birthday
                if (res.birthday == null) {
                    document.querySelector(".selected-account-details-birthday").textContent = ""
                } else {
                    document.querySelector(".selected-account-details-birthday").textContent = res.birthday.slice(0, -9);
                }
                //address
                if (res.birthday == null) {
                    document.querySelector(".selected-account-details-address").textContent = ""
                } else {
                    document.querySelector(".selected-account-details-address").textContent = res.address;
                }

                document.querySelector(".selected-account-details-fullname").textContent = res.label; //fullname
                document.querySelector(".selected-account-details-phone").textContent = res.phone;
                document.querySelector(".selected-account-details-email").textContent = res.email;

                //socials start
                if (res.facebook == null) {
                    document.querySelector(".selected-account-social-facebook").textContent = ""
                } else {
                    document.querySelector(".selected-account-social-facebook").textContent = res.facebook;
                }
                if (res.twitter == null) {
                    document.querySelector(".selected-account-social-twitter").textContent = ""
                } else {
                    document.querySelector(".selected-account-social-twitter").textContent = res.twitter;
                }
                if (res.instagram == null) {
                    document.querySelector(".selected-account-social-instagram").textContent = ""
                } else {
                    document.querySelector(".selected-account-social-instagram").textContent = res.instagram;
                }
                if (res.linkedin == null) {
                    document.querySelector(".selected-account-social-linkedin").textContent = ""
                } else {
                    document.querySelector(".selected-account-social-linkedin").textContent = res.linkedin;
                }
                //socials end
            },
            error: function (res) {
                $.toast({
                    heading: 'Error',
                    text: "Unexpected Error Ocoured 500 ! Account details not found!",
                    icon: 'error',
                    loader: true,
                    bgColor: '#dc3545',
                    loaderBg: '#f7d40d',
                    position: 'top-right',
                    hideAfter: 6000
                });
            }
        });
    }

    //=====================================================================

    //Remove Friend
    if ($(".btn-remove-friend").length) {
        $(".btn-remove-friend").click(function (ev) {

            let selectedAccountName = $('.selected-account-details-fullname').text();

            Swal.fire({
                icon: 'info',
                title: 'Remove ' + selectedAccountName + ' from friend list ?',
                text: 'If you get out of friendship, you may not see the information shared with friends',
                reverseButtons: true,
                focusConfirm: false,
                showCancelButton: true,
                confirmButtonColor: '#ff005b',
                cancelButtonColor: '#858796',
                cancelButtonText: 'No',
                confirmButtonText: 'Yes'
            }).then((result) => {
                if (result.value) {
                    //remove friend db request
                    $.ajax({
                        url: '/friends/removefromfriendship',
                        type: "POST",
                        dataType: "json",
                        data:
                        {
                            friendId: $('#hidden-selected-account-id').text()
                        },
                        success: function (res) {
                            if (res.status) {
                                $.toast({
                                    heading: 'Info',
                                    text: selectedAccountName + " removed from your friend list!",
                                    icon: 'info',
                                    loader: true,
                                    bgColor: '#3988ff',
                                    loaderBg: '#f7d40d',
                                    position: 'top-right',
                                    hideAfter: 4000
                                });
                                setTimeout(function () { location.reload(true); }, 4000); //reload page
                            };
                            if (res.status == false) {
                                $.toast({
                                    heading: 'Info',
                                    text: "Bad Request! Friendship Not Found!",
                                    icon: 'info',
                                    loader: true,
                                    bgColor: '#3988ff',
                                    loaderBg: '#f7d40d',
                                    position: 'top-right',
                                    hideAfter: 7000
                                });
                            };
                        }
                    });
                };
            });
        });
    };

    //New Friend Request
    if ($(".btn-send-friend-request").length) {
        $(".btn-send-friend-request").click(function (ev) {

            let selectedAccountName = $('.selected-account-details-fullname').text();
            let selectedAccountId = $('#hidden-selected-account-id').text()

            if (selectedAccountId != "") {
                $.ajax({
                    url: '/friends/newfriendrequest',
                    type: "POST",
                    dataType: "json",
                    data:
                    {
                        accountId: selectedAccountId
                    },
                    success: function (res) {
                        if (res.status) {
                            $.toast({
                                heading: 'Info',
                                text: "Friendship request has been sent to " + selectedAccountName,
                                icon: 'info',
                                loader: true,
                                bgColor: '#3988ff',
                                loaderBg: '#f7d40d',
                                position: 'top-right',
                                hideAfter: 7000
                            });
                            $(".btn-send-friend-request").toggleClass('d-none')
                        };
                        if (res.status == false) {
                            $.toast({
                                heading: 'Error',
                                text: "Bad Request! Friendship Not Found!",
                                icon: 'info',
                                loader: true,
                                bgColor: '#3988ff',
                                loaderBg: '#f7d40d',
                                position: 'top-right',
                                hideAfter: 7000
                            });
                        };
                    }
                });
            }
            else {
                $.toast({
                    heading: 'Info',
                    text: "System Error! Unexpected error ocoured while sending friend request selected account!",
                    icon: 'info',
                    loader: true,
                    bgColor: '#3988ff',
                    loaderBg: '#f7d40d',
                    position: 'top-right',
                    hideAfter: 7000
                });
            }
        });
    };

    //======================================================================

    function getNotification() {
        $.ajax({
            url: "notification/GetUserNotifications",
            method: "GET",
            data: $('#hidden-account-id').val(),
            success: function (res) {
                alert("ok");
            },
            error: function () {
                alert("error");
            }
        });
    }

    //let connection = new signalR.HubConnection("/signalServer");

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build();

    connection.on('displayNewFriendRequestNotification', () => {
        getNotification();
    })
    connection.start();
});
