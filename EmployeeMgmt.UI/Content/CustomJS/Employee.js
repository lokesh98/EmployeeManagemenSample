$(document).ready(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "yy-mm-dd",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0"
    });


    $("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#success-alert").slideUp(500);
    });
    $("#error-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#error-alert").slideUp(500);
    });

    $(".resetform").click(function () {
        window.location.href = window.location.href.split('?')[0];
    })



   

});

function ShowImagePreview(imageUploader, previewImage) {
    var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024

    if (imageUploader.files && imageUploader.files[0]) {

        if (imageUploader.files[0].size < maxFileSize) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(previewImage).attr('src', e.target.result);
            }
            reader.readAsDataURL(imageUploader.files[0]);
        }
        else {
            alert('File Size is Greate than 4MB. Please Choose smaller file.');
            $("#imagePreview").val('');
            $("#ImageUpload").val('');
        }
    }
}