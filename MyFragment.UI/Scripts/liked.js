
$("button[data-liked]").click(function () {
    var btn = $(this);
    var liked = btn.data("liked");
    var movieId = btn.data("movie-id");
    var spanHeart = btn.children().first();

    $.ajax({
        method: "POST",
        url: "/Home/SetLikeState",
        data: { "movieId": movieId, "liked": !liked }
    }).done(function (data) {

        if (data.hasError) {
            alert(data.errorMessage);
        } else {
            liked = !liked;
            btn.data("liked", liked);

            spanHeart.removeClass("fa-heart");
            spanHeart.removeClass("fa-heart-broken");

            if (liked) {
                spanHeart.addClass("fa-heart");
            } else {
                spanHeart.addClass("fa-heart-broken");
            }
        }
    }).fail(function () {
        alert("Sunucu ile bağlantı kurulamadı");
    });
});
