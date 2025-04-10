$("#SearchingInputs").on("keyup", function () {
    let value = $(this).val();
    if (value != null) {
        fetch("/_Layout/Search?search=" + value)
            .then(response => response.text())
            .then(data => {
                $(".searchList").html(data);
            })
    }
});

$(".fa-magnifying-glass").click(function (e) {
    e.preventDefault();
    $(".tp-header-search-bar").show();

})