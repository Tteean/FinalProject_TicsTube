$(document).ready(function () {
    $("#File").change(function (ev) {
        let file = ev.target.files[0];
        var uploadimg = new FileReader();

        uploadimg.onload = function (displayimg) {
            $("#imgPreview").attr('src', displayimg.target.result);
            $("#imgPreview").show();
        }
        uploadimg.readAsDataURL(file);
    })
})


$(document).ready(function () {
    const $movieRadio = $("#moviechose");
    const $tvShowRadio = $("#showchose");
    const $movieSelect = $("#movie");
    const $tvShowSelect = $("#show");

    function toggleSelects() {
        if ($movieRadio.is(":checked")) {
            $movieSelect.prop("disabled", false);
            $tvShowSelect.prop("disabled", true).val('');
        } else if ($tvShowRadio.is(":checked")) {
            $tvShowSelect.prop("disabled", false);
            $movieSelect.prop("disabled", true).val('');
        }
    }

    $movieRadio.change(toggleSelects);
    $tvShowRadio.change(toggleSelects);

    toggleSelects();
});