$("#SearchingInputs").on("keyup", function () {
    let value = $(this).val();
    fetch("/Home/Search?search=" + value)
            .then(response => response.text())
            .then(data => {
                $(".searchList").html(data);
            })

    console.log(value)
});

$(document).ready(function () {
    const starContainer = $(".star");
    console.log("Star container:", starContainer);
    const rating = parseFloat(starContainer.data("rating")) || 0;
    console.log("Rating:", rating);

    starContainer.find("i").each(function (index) {
        console.log("Star index:", index);
        if (rating >= index + 1) {
            $(this).css("color", "gold");
        } else if (rating > index) {
            $(this).css({
                "background": "linear-gradient(to right, gold 50%, #ccc 50%)",
                "-webkit-background-clip": "text",
                "-webkit-text-fill-color": "transparent"
            });
        } else {
            $(this).css("color", "#ccc");
        }
    });
    console.log("Скрипт rating.js загружен");
});

$(".searchingGlass").click(function (e) {
    e.preventDefault();
    $(".searchPart").css("display", "flex");
});

$(document).on("click", ".tp-search-close", function () {
    $(".searchPart").css("display", "none");
});