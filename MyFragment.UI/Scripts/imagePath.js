$('input[type="file"]').change(function (e) {
    var val = $(this).val();

    switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
    case 'jpeg': case 'jpg': case 'png':

        var fileName = e.target.files[0].name;
        $('#ImagePath').val(fileName);
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imgshow').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
        break;
    default:
        $(this).val('');
        // error message here
        alert("Bu bir fotoğraf değildir. Lütfen '.png' , '.jpg' , '.jpeg' uzantılı dosyaları seçiniz!");
        break;
    }
});