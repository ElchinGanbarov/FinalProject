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
    $(document).ready(function () {

        $("#search-accounts-input").autocomplete({
            source: '/api/friendapi/searchaccount',
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
    })

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
                //img
                if (res.profileImg != null) {
                    $(".selected-account-details-img").attr("src", "https://res.cloudinary.com/djmiksiim/v1/" + res.profileImg)
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

                document.querySelector(".selected-account-details-fullname").textContent = res.name + " " + res.surname;
                document.querySelector(".selected-account-details-phone").textContent = res.phone;
                document.querySelector(".selected-account-details-email").textContent = res.email;

                //get social links from db
                //$.ajax({
                //    url: '/friends/friendsociallinks',
                //    type: "POST",
                //    dataType: "json",
                //    data:
                //    {
                //        friendId: accountId
                //    },
                //    success: function (socialresp) {
                //        //facebook
                //        if (socialresp.facebook == null) {
                //            document.querySelector(".selected-account-social-facebook").textContent = ""
                //        } else {
                //            document.querySelector(".selected-account-social-facebook").textContent = socialresp.facebook;
                //        }
                //        //twitter
                //        if (socialresp.twitter == null) {
                //            document.querySelector(".selected-account-social-twitter").textContent = ""
                //        } else {
                //            document.querySelector(".selected-account-social-twitter").textContent = socialresp.twitter;
                //        }
                //        //instagram
                //        if (socialresp.instagram == null) {
                //            document.querySelector(".selected-account-social-instagram").textContent = ""
                //        } else {
                //            document.querySelector(".selected-account-social-instagram").textContent = socialresp.instagram;
                //        }
                //        //linkedin
                //        if (socialresp.linkedin == null) {
                //            document.querySelector(".selected-account-social-linkedin").textContent = ""
                //        } else {
                //            document.querySelector(".selected-account-social-linkedin").textContent = socialresp.linkedin;
                //        }

                //    }
                //})
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
});
