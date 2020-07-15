//Update Profile Img
$(document).ready(function () {
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

    $(".profile-img-remove").click(function (e) {
        console.log("test");
        let elem = $(this).parents(".item");
        let href = $(this).attr("href");
        $.confirm({
            title: 'Şəkil',
            content: 'Şəkil silinsin mi?',
            buttons: {
                'Bəli': {
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
                'Xeyir': function () {
                }
            }
        });
        e.preventDefault();
    });

   
});
