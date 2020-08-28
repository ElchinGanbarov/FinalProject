"use strict"

//import { signalR } from "../lib/signalr/dist/browser/signalr";

$(document).ready(function () {
    //Privacy Dropdown & Security checkboxs Js
    $('[data-privacy-profile-picture]').on('click', function (e) {
        e.preventDefault();
        $('[data-privacy-profile-picture-text]').text($(this).text());
        $('button[data-privacy-profile-picture-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-status]').on('click', function (e) {
        e.preventDefault();
        $('[data-privacy-status-text]').text($(this).text());
        $('button[data-privacy-status-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-last-seen]').on('click', function (e) {
        e.preventDefault();
        $('[data-privacy-last-seen-text]').text($(this).text());
        $('button[data-privacy-last-seen-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-phone]').on('click', function (e) {
        e.preventDefault();
        $('[data-privacy-phone-text]').text($(this).text());
        $('button[data-privacy-phone-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-birthday]').on('click', function (e) {
        e.preventDefault();
        $('[data-privacy-birthday-text]').text($(this).text());
        $('button[data-privacy-birthday-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-address]').on('click', function (e) {
        e.preventDefault();
        $('[data-privacy-address-text]').text($(this).text());
        $('button[data-privacy-address-text]').attr("data-selected-value", $(this).data("privacy-value"));
        $(".changeprivacy").prop("disabled", false)
    });
    $('[data-privacy-social-links]').on('click', function (e) {
        e.preventDefault();
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

    //Sweet Alert Voice Buttons (STATIC !!!)
    $(".voice-call-now").click(function (e) {

        Swal.fire({
            type: 'info',
            title: 'Oops...',
            text: 'You are using a basic account. Switch to Premium Messenger App Account to use voice and video calls!',
            footer: '<a href>Get Premium Account Now!</a>'
        })
        e.preventDefault();

    })

    //====================================================================

    //(Update Account Datas)
    $(document).on("submit", ".accountchange", function (e) {
        let Name = $("#AccountDetailViewModel_Name").val().trim();
        let Surname = $("#AccountDetailViewModel_Surname").val().trim();
        let Phone = $("#AccountDetailViewModel_Phone").val().trim();
        let Birthday = $("#AccountDetailViewModel_Birthday").val();
        let Email = $("#AccountDetailViewModel_Email").val().trim();
        let Website = $("#AccountDetailViewModel_Website").val().trim();
        let Address = $("#AccountDetailViewModel_Address").val().trim();
        $.ajax({
            url: '/accountdetail/update',
            type: "POST",
            dataType: "json",
            data:
            {
                name: Name,
                surname: Surname,
                phone: Phone,
                birthday: Birthday,
                email: Email,
                website: Website,
                address: Address
            },
            success: function (res) {
                $(".email").text(Email);
                $(".profilename").text(Name + " " + Surname);
                $(".birthday").text(Birthday);
                $(".phone").text(Phone);
                $(".website").text(Website);
                $(".address").text(Address);
                $.toast({
                    heading: 'Info',
                    text: "Account Details Updated Successfully",
                    icon: 'info',
                    loader: true,
                    bgColor: '#3988ff',
                    loaderBg: '#f7d40d',
                    position: 'top-right',
                    hideAfter: 4000
                });
                $(".onchangesubmit").prop("disabled", true);
            }
        });
        e.preventDefault();
    });

    //Update Social Links
    $(document).on("submit", ".sociallink", function (e) {
        let Facebook = $("#AccountSocialLinkViewModel_Facebook").val().trim();
        let Twitter = $("#AccountSocialLinkViewModel_Twitter").val().trim();
        let Instagram = $("#AccountSocialLinkViewModel_Instagram").val().trim();
        let Linkedin = $("#AccountSocialLinkViewModel_Linkedin").val().trim();
        $.ajax({
            url: '/accountdetail/updatesociallink',
            type: "POST",
            dataType: "json",
            data:
            {
                facebook: Facebook,
                twitter: Twitter,
                instagram: Instagram,
                linkedin: Linkedin
            },
            success: function (res) {
                console.log(res);
                $(".facebook").text(Facebook);
                $(".twitter").text(Twitter);
                $(".instagram").text(Instagram);
                $(".linkedin").text(Linkedin);
                $.toast({
                    heading: 'Info',
                    text: "Social Links Updated Successfully!",
                    icon: 'info',
                    loader: true,
                    bgColor: '#3988ff',
                    loaderBg: '#f7d40d',
                    position: 'top-right',
                    hideAfter: 4000
                });
                $(".onchangesocial").prop("disabled", true);
            }
        });
        e.preventDefault();
    });

    //Update Password
    $(document).on("submit", ".bir", function (e) {
        let CurrentPassword = $("#UpdatePasswordViewModel_CurrentPassword").val().trim();
        let NewPassword = $("#UpdatePasswordViewModel_Password").val().trim();
        let RepeatPassword = $("#UpdatePasswordViewModel_PasswordConfirm").val().trim();
        $.ajax({
            url: '/account/updatepassword',
            type: "POST",
            dataType: "json",
            data:
            {
                currentpassword: CurrentPassword,
                password: NewPassword,
                passwordconfirm: RepeatPassword
            },

            success: function (res) {
                if (res.status == false) {
                    $(".currentpassworderror").text(res.message);
                    $.toast({
                        heading: 'Error',
                        text: res.message,
                        icon: 'error',
                        loader: true,
                        bgColor: '#dc3545',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 4000
                    });
                    $("#UpdatePasswordViewModel_Password").val("");
                    $("#UpdatePasswordViewModel_PasswordConfirm").val("");
                }

                if (res.status == true) {
                    $.toast({
                        heading: 'Info',
                        text: res.message,
                        icon: 'info',
                        loader: true,
                        bgColor: '#3988ff',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 5000
                    });
                    $("#UpdatePasswordViewModel_Password").val("");
                    $("#UpdatePasswordViewModel_PasswordConfirm").val("");
                    $("#UpdatePasswordViewModel_CurrentPassword").val("");
                    $(".changepassword").prop("disabled", true);
                }
            }
        });
        e.preventDefault();
    });

    //Update Privacy
    $(document).on("submit", ".update-account-privacy", function (e) {
        let ProfileImg = $('button[data-privacy-profile-picture-text]').attr("data-selected-value");
        let StatusText = $('button[data-privacy-status-text]').attr("data-selected-value");
        let LastSeen = $('button[data-privacy-last-seen-text]').attr("data-selected-value");
        let Phone = $('button[data-privacy-phone-text]').attr("data-selected-value");
        let UserBirthday = $('button[data-privacy-birthday-text]').attr("data-selected-value");
        let Address = $('button[data-privacy-address-text]').attr("data-selected-value");
        let SocialLink = $('button[data-privacy-social-links-text]').attr("data-selected-value");
        let AcceptMessages = $("#privacy-accept-messages").prop("checked");
        $.ajax({
            url: '/accountdetail/updateprivacy',
            type: "POST",
            dataType: "json",
            data:
            {
                profileImg: ProfileImg,
                statusText: StatusText,
                lastSeen: LastSeen,
                phone: Phone,
                birthday: UserBirthday,
                address: Address,
                socialLink: SocialLink,
                acceptAllMessages: AcceptMessages
            },
            success: function (res) {
                if (res.status) {
                    $.toast({
                        heading: 'Completed!',
                        text: res.message,
                        icon: 'info',
                        loader: true,
                        bgColor: '#3988ff',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                    $(".changeprivacy").prop("disabled", true);
                }
                if (res.status == false) {
                    $.toast({
                        heading: 'Error',
                        text: res.message,
                        icon: 'error',
                        loader: true,
                        bgColor: '#dc3545',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                }
            },
            error: function (res) {
                $.toast({
                    heading: 'Error',
                    text: "System Error 500 !",
                    icon: 'error',
                    loader: true,
                    bgColor: '#dc3545',
                    loaderBg: '#f7d40d',
                    position: 'top-right',
                    hideAfter: 6000
                });
            }
        });
        e.preventDefault();
    });

    //Update Security
    $(document).on("submit", ".update-account-security", function (e) {
        let TFA = $("#security-tfa").prop("checked");
        let LoginAlerts = $("#security-login-alerts").prop("checked");
        $.ajax({
            url: '/accountdetail/updatesecurity',
            type: "POST",
            dataType: "json",
            data:
            {
                twoFactoryAuth: TFA,
                loginAlerts: LoginAlerts
            },
            success: function (res) {
                if (res.status) {
                    $.toast({
                        heading: 'Completed!',
                        text: res.message,
                        icon: 'info',
                        loader: true,
                        bgColor: '#3988ff',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                    $(".changesecurity").prop("disabled", true);
                }
                if (res.status == false) {
                    $.toast({
                        heading: 'Error',
                        text: res.message,
                        icon: 'error',
                        loader: true,
                        bgColor: '#dc3545',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                }
            },
            error: function (res) {
                $.toast({
                    heading: 'Error',
                    text: "System Error 500 !",
                    icon: 'error',
                    loader: true,
                    bgColor: '#dc3545',
                    loaderBg: '#f7d40d',
                    position: 'top-right',
                    hideAfter: 6000
                });
            }
        });
        e.preventDefault();
    });

    //Show / Hide selected accounts's Details (Friend List)
    $(document).ready(function () {
        $('#friendsTab').on('click', 'li', function (e) {
            e.preventDefault();
            $('#friendsTab li.active').removeClass('active');
            $(this).addClass('active');

            //show selected friend's details
            if ($('.friends-intro-wrapper').hasClass('d-flex')) {
                $('.friends-intro-wrapper').removeClass('d-flex').addClass('d-none');
                $('.selected-account-details').toggleClass('d-none')
            }
            //selected-account-details-img

            let accountId = $('#friendsTab li.active').attr('data-id')
            $.ajax({
                url: '/friends/getfriendinfo',
                type: "POST",
                dataType: "json",
                data:
                {
                    friendId: accountId
                },
                success: function (res) {


                    let btnSendFriendRequest = $('.btn-send-friend-request');
                    let btnRemoveFriend = $('.btn-remove-friend');
                    let btnCancelFriendRequest = $('.btn-reject-friend-request');
                    let btnAcceptFriendRequest = $('.btn-accept-friend-request');

                    if ($('.account-proccess-wrapper').hasClass('d-flex') == false) {
                        $('.account-proccess-wrapper').addClass('d-flex');
                    }
                    if (btnSendFriendRequest.hasClass('d-none') == false) {
                        btnSendFriendRequest.addClass('d-none')
                    }
                    if (btnCancelFriendRequest.hasClass('d-none') == false) {
                        btnCancelFriendRequest.addClass('d-none')
                    }
                    if (btnAcceptFriendRequest.hasClass('d-none') == false) {
                        btnAcceptFriendRequest.addClass('d-none')
                    }
                    if (btnRemoveFriend.hasClass('d-none')) {
                        btnRemoveFriend.removeClass('d-none')
                    }

                    //id
                    document.querySelector("#hidden-selected-account-id").textContent = res.id;
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

                    //facebook
                    if (res.facebook == null) {
                        document.querySelector(".selected-account-social-facebook").textContent = ""
                    } else {
                        document.querySelector(".selected-account-social-facebook").textContent = res.facebook;
                    }
                    //twitter
                    if (res.twitter == null) {
                        document.querySelector(".selected-account-social-twitter").textContent = ""
                    } else {
                        document.querySelector(".selected-account-social-twitter").textContent = res.twitter;
                    }
                    //instagram
                    if (res.instagram == null) {
                        document.querySelector(".selected-account-social-instagram").textContent = ""
                    } else {
                        document.querySelector(".selected-account-social-instagram").textContent = res.instagram;
                    }
                    //linkedin
                    if (res.linkedin == null) {
                        document.querySelector(".selected-account-social-linkedin").textContent = ""
                    } else {
                        document.querySelector(".selected-account-social-linkedin").textContent = res.linkedin;
                    }
                },
                error: function (res) {
                    $.toast({
                        heading: 'Error',
                        text: "System Error 500 ! All Friends Not Response!",
                        icon: 'error',
                        loader: true,
                        bgColor: '#dc3545',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                }
            });
        });
    })

    // hide selected account's details
    if ($('.hide-selected-account-details').length) {
        $('.hide-selected-account-details').click(function (e) {
            e.preventDefault();
            if ($('.friends-intro-wrapper').hasClass('d-none')) {
                $('.friends-intro-wrapper').removeClass('d-none').addClass('d-flex');
                $('.selected-account-details').toggleClass('d-none')
                $('#friendsTab li.active').removeClass('active');

                //reset inputs
                $(".selected-account-details-img").attr("src", "/uploads/default-profile-img.jpg") //img
                document.querySelector(".selected-account-details-statusText").textContent = ""; //statusText
                document.querySelector(".selected-account-details-fullname").textContent = ""; //fullname
                document.querySelector(".selected-account-details-phone").textContent = ""; //phone
                document.querySelector(".selected-account-details-email").textContent = ""; //email
                document.querySelector(".selected-account-details-birthday").textContent = ""; //birthday
                document.querySelector(".selected-account-details-website").textContent = ""; //website
                document.querySelector(".selected-account-details-address").textContent = ""; //address

                //socials reset
                document.querySelector(".selected-account-social-facebook").textContent = "";
                document.querySelector(".selected-account-social-twitter").textContent = "";
                document.querySelector(".selected-account-social-instagram").textContent = "";
                document.querySelector(".selected-account-social-linkedin").textContent = "";
            }
        })
    }

    //====================================================================

    //Search All Accounts with Autocomplete
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
                                    hideAfter: 3000
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
                            $(".btn-reject-friend-request").toggleClass('d-none')
                        };
                        if (res.status == false) {
                            $.toast({
                                heading: 'Error',
                                text: "Bad Request! Unexpected Error Occoured!",
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
            method: "POST",
            //method: "GET",
            data: $('#hidden-account-id').val(),
            success: function (result) {

                if (result.count != 0) {
                    console.log(result.count);
                }
                if (result.count < 1) {
                    console.log("empty result");
                }
                alert("new friend request from system successfully");
            },
            error: function () {
                alert("friend request error ocoured");
            }
        });
    }

    //Search Friend Accounts Autocomplete
    $("#search-friends-input").autocomplete({

        source: function (request, response) {
            $.ajax({
                url: "/friends/searchfriends",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {

                    for (var i = 0; i < data.length; i++) {
                        let img = "/uploads/default-profile-img.jpg";
                        //clear old datas
                        if (i == 0) {
                            $('#search-friend-accounts-results').empty();
                            $('#search-friend-accounts-results').removeClass('d-none');
                            $('#search-friend-accounts-wrapper').addClass('d-none');
                        }

                        if (data[i].img != null) {
                            img = "https://res.cloudinary.com/djmiksiim/v1/" + data[i].img;
                        }
                        $('#search-friend-accounts-wrapper').addClass('d-none');
                        $('#search-friend-accounts-results').append("<li style='z-index: 1042;' class='list-group-item'><div class='media'><div class='avatar avatar-online mr-2'><img style='width: 48px; height: 48px; object-fit: cover; border-radius: 50%' src='" + img + "'/></div><div class='media-body'><h6 class='text-truncate'><a href='#' class='text-reset'>" + data[i].label + "</a></h6><p class='text-muted mb-0'>Online</p></div></div></li>");

                    }
                },
                error: function () {
                    $('#search-friend-accounts-results').empty();
                    $('#search-friend-accounts-results').addClass('d-none');
                    $('#search-friend-accounts-wrapper').removeClass('d-none');
                }
            });
        },

        minLength: 0,
        select: function (event, ui) {
            event.preventDefault();
            $("#search-friends-input").val(ui.item.label);
            $("#hidden-friend-search-id").val(ui.item.id);

            //SearchResultUserInfos(ui.item.id) //funtion - send message to selected account's hub

            //$("#search-friends-input").val("");
            //$("#hidden-friend-search-id").val("");
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#search-friends-input").val(ui.item.label);
        },
        html: true,
        open: function () {
            $("ul#ui-id-2").css("z-index", "1041");
            $("#hidden-search-friends-result-count").val = $('ul#ui-id-2 > li').length;
        }
    })

    $("#search-friends-input").autocomplete("instance")._renderItem = function (ul, item) {

            let img = "/uploads/default-profile-img.jpg";
            if (item.img != null) {
                img = "https://res.cloudinary.com/djmiksiim/v1/" + item.img;
            }
            $('#search-friend-accounts-wrapper').addClass('d-none');
            return $('#search-friend-accounts-results').append("<li style='z-index: 1042;' class='list-group-item'><div class='media'><div class='avatar avatar-online mr-2'><img style='width: 50px; height: 50px; object-fit: cover; border-radius: 50%' src='" + img + "'/></div><div class='media-body'><h6 class='text-truncate'><a href='#' class='text-reset'>" + item.value + "</a></h6><p class='text-muted mb-0'>Online</p></div></div></li>");

    };

    //======================================================================================


    //Call List
    $(document).ready(function () {
        $('#callLogTab').on('click', 'li', function (e) {
            e.preventDefault();

            if ($('#callLogTab li.active').hasClass('active')) {
                $('#callLogTab li.active').removeClass('active')
            }
            $(this).addClass('active');

            //show selected accounts's details
            if ($('.call-list-own-wrap').hasClass('d-none') == false ) {
                $('.call-list-own-wrap').addClass('d-none')
                $('.call-list-selected-account-wrap').removeClass('d-none');
            }

            let accountId = $('#callLogTab li.active').attr('data-id')
            $.ajax({
                url: '/friends/getfriendinfo',
                type: "POST",
                dataType: "json",
                data:
                {
                    friendId: accountId
                },
                success: function (res) {

                    if ($('.call-list-own-wrap').hasClass('d-none') == false) {
                        $('.call-list-own-wrap').addClass('d-none');
                        $('.call-list-selected-account-wrap').removeClass('d-none');
                    }
                    //id
                    document.querySelector("#call-hidden-selected-user-id").textContent = res.id;
                    //img
                    if (res.img != null) {
                        $(".call-list-user-photo").attr("src", "https://res.cloudinary.com/djmiksiim/v1/" + res.img)
                    } else {
                        $(".call-list-user-photo").attr("src", "/uploads/default-profile-img.jpg")
                    }

                    document.querySelector(".call-list-user-fullname").textContent = res.label; //fullname
                    document.querySelector(".call-list-user-phone").textContent = res.phone;

                },
                error: function (res) {
                    $.toast({
                        heading: 'Error',
                        text: "System Error 500 ! All Friends Not Response!",
                        icon: 'error',
                        loader: true,
                        bgColor: '#dc3545',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                }
            });

        });
    })

    // hide selected account's details
    $('.hide-call-selected-user-options').click(function (e) {
        e.preventDefault();
        if ($('.call-list-own-wrap').hasClass('d-none')) {
            $('.call-list-own-wrap').removeClass('d-none');
            $('.call-list-selected-account-wrap').toggleClass('d-none')
            $('#callLogTab li.active').removeClass('active');

            //reset inputs
            $(".call-list-user-photo").attr("src", "/uploads/default-profile-img.jpg") //img
            document.querySelector(".call-list-user-fullname").textContent = ""; //fullname
            document.querySelector(".call-list-user-phone").textContent = ""; //phone
            document.querySelector("#call-hidden-selected-user-id").textContent = "";
        }
    })


    //=======================================================================================

    //send invitation mail
    $("#btn-send-invitation-email").click(function (e) {
        e.preventDefault();

        var isValidEmail = true;
        var isValidText = true;

        var regex = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        if (regex.test($("#InvitationEmailViewModel_ReceiverEmail").val()) == false) {
            $('.send-invitation-email-error').css("display", "block")

            $.toast({
                heading: 'Error',
                text: "Please, enter valid email address",
                icon: 'error',
                loader: true,
                bgColor: '#dc3545',
                loaderBg: '#f7d40d',
                position: 'top-right',
                hideAfter: 6000
            });

            isValidEmail = false;
        }

        if ($("#InvitationEmailViewModel_Text").val().trim().length < 10) {
            $.toast({
                heading: 'Error',
                text: "Message text must be minimum 10 characters",
                icon: 'error',
                loader: true,
                bgColor: '#dc3545',
                loaderBg: '#f7d40d',
                position: 'top-right',
                hideAfter: 6000
            });

            isValidEmail = false;
        }

        if ($("#InvitationEmailViewModel_Text").val().trim().length > 200) {
            $.toast({
                heading: 'Error',
                text: "Message text must be maximum 200 characters",
                icon: 'error',
                loader: true,
                bgColor: '#dc3545',
                loaderBg: '#f7d40d',
                position: 'top-right',
                hideAfter: 6000
            });

            isValidEmail = false;
        }

        if (isValidEmail == true && isValidText == true) {
            $.ajax({
                url: '/friends/SendInvitationEmail',
                type: "POST",
                dataType: "json",
                data:
                {
                    senderFullname: "",
                    receiverEmail: $("#InvitationEmailViewModel_ReceiverEmail").val().trim(),
                    text: $("#InvitationEmailViewModel_Text").val().trim()
                },
                beforeSend: function () {
                    $.toast({
                        heading: 'Sending email...',
                        text: "Please, wait for sending email",
                        icon: 'info',
                        loader: true,
                        bgColor: '#3988ff',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 10000
                    });

                    $("#inviteOthers").modal('hide');
                },
                success: function (res) {
                    if (res.status) {
                        $.toast({
                            heading: 'Info',
                            text: "The invitation mail has been sent successfully!",
                            icon: 'info',
                            loader: true,
                            bgColor: '#3988ff',
                            loaderBg: '#f7d40d',
                            position: 'top-right',
                            hideAfter: 6000
                        });
                        $('.send-invitation-email-error').css("display", "none")
                        $("#InvitationEmailViewModel_ReceiverEmail").val("");
                        $("#InvitationEmailViewModel_Text").val("This application awesome and useful! You can use this free! Sign up now and enjoy...");
                    };
                    if (res.status == false) {
                        $.toast({
                            heading: 'Error',
                            text: "Send invitation mail has been failed!",
                            icon: 'error',
                            loader: true,
                            bgColor: '#dc3545',
                            loaderBg: '#f7d40d',
                            position: 'top-right',
                            hideAfter: 6000
                        });
                    }
                },
                error: function (res) {
                    $.toast({
                        heading: 'Error',
                        text: "System Error 500 !",
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
    })

    //=======================================================================================

    //Chat Hub Click event
    $(document).ready(function () {
        $('#chatContactTab').on('click', 'li', function (e) {
            e.preventDefault();

            if ($('#callLogTab li.active').hasClass('active')) {
                $('#callLogTab li.active').removeClass('active')
            }
            $(this).addClass('active');

            //show selected message hub's details
            if ($('.chat-body-introduction').hasClass('d-flex')) {
                $('.chat-body-introduction').removeClass('d-flex').addClass('d-none');
                $('.chat-body').removeClass('d-none');
            }

            let hubId = $('#callLogTab li.active').attr('data-id')

            $.ajax({
                url: '/chat/gethubmessagesall',
                type: "POST",
                dataType: "json",
                data:
                {
                    friendId: accountId
                },
                success: function (res) {

                    //let btnSendFriendRequest = $('.btn-send-friend-request');
                    //let btnRemoveFriend = $('.btn-remove-friend');
                    //let btnCancelFriendRequest = $('.btn-reject-friend-request');
                    //let btnAcceptFriendRequest = $('.btn-accept-friend-request');


                    if ($('.call-list-own-wrap').hasClass('d-none') == false) {
                        $('.call-list-own-wrap').addClass('d-none');
                        $('.call-list-selected-account-wrap').removeClass('d-none');
                    }


                    //if (btnSendFriendRequest.hasClass('d-none') == false) {
                    //    btnSendFriendRequest.addClass('d-none')
                    //}
                    //if (btnCancelFriendRequest.hasClass('d-none') == false) {
                    //    btnCancelFriendRequest.addClass('d-none')
                    //}
                    //if (btnAcceptFriendRequest.hasClass('d-none') == false) {
                    //    btnAcceptFriendRequest.addClass('d-none')
                    //}
                    //if (btnRemoveFriend.hasClass('d-none')) {
                    //    btnRemoveFriend.removeClass('d-none')
                    //}


                    //id
                    document.querySelector("#call-hidden-selected-user-id").textContent = res.id;
                    //img
                    if (res.img != null) {
                        $(".call-list-user-photo").attr("src", "https://res.cloudinary.com/djmiksiim/v1/" + res.img)
                    } else {
                        $(".call-list-user-photo").attr("src", "/uploads/default-profile-img.jpg")
                    }

                    document.querySelector(".call-list-user-fullname").textContent = res.label; //fullname
                    document.querySelector(".call-list-user-phone").textContent = res.phone;

                },
                error: function (res) {
                    $.toast({
                        heading: 'Error',
                        text: "System Error 500 ! All Friends Not Response!",
                        icon: 'error',
                        loader: true,
                        bgColor: '#dc3545',
                        loaderBg: '#f7d40d',
                        position: 'top-right',
                        hideAfter: 6000
                    });
                }
            });

        });
    })
});

