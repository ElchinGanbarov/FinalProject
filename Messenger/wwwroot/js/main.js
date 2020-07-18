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


});
