$("#SearchingInputs").on("keyup", function () {
    let value = $(this).val();
    fetch("/Home/Search?search=" + value)
            .then(response => response.text())
            .then(data => {
                $(".searchList").html(data);
            })

    console.log(value)
});

$(".searchingGlass").click(function (e) {
    e.preventDefault();
    $(".searchPart").css("display", "flex");
});

$(document).on("click", ".tp-search-close", function () {
    $(".searchPart").css("display", "none");
});