$(function () {
    $("#modalSummary").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");

        $("#modalBody").html('yükleniyor');

        $("#modalBody").load("/User/GetModalSummary/" + id);
    });

    $("#modalActors").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");

        $("#modalActorsBody").html('yükleniyor');

        $("#modalActorsBody").load("/User/GetModalActors/" + id);
    });
    $("#modalDirectors").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");
        $("#modalActorsBody").html('yükleniyor');
        $("#modalDirectorsBody").load("/User/GetModalDirectors/" + id);
    });
    $("#modalCategories").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");
        $("#modalActorsBody").html('yükleniyor');
        $("#modalCategoriesBody").load("/User/GetModalCategories/" + id);
    });
    $("#modalFragment").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.attr("movie-id");

        $("#modalFragmentBody").html('yükleniyor');
        $("#modalFragmentBody").load("/User/GetModalFragment/" + id);
    });
    $("#modalFragment").on('hide.bs.modal', function () {
        $("#fragmentVideo").attr('src', '');
    });
});