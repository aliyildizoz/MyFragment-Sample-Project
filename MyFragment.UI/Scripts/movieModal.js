$(function () {
    $("#modalSummary").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");
        $("#modalBody").html('yükleniyor');
        $("#modalBody").load("/Movie/GetModalSummary/" + id);
    });

    $("#modalFragment").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");

        $("#modalFragmentBody").html('yükleniyor');
        $("#modalFragmentBody").load("/Movie/GetModalFragment/" + id);
    });
    $("#modalFragment").on('hide.bs.modal', function () {
        $("#fragmentVideo").attr('src', '');
    });
});