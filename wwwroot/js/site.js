// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    console.log("Page is ready");


    // update HTML elemendt by its id
    function updateElement(id, data) {
        console.log(data);
        $(id).html(data)
    }


    function getPartialViewAjax(path, data_in) {
        $.ajax({
            type: "POST",
            url: path,
            data: data_in,
            success: function (response) {
                console.log("data posted to the controller:");
                console.log(data_in);
                updateElement("#contactIndexContent", response);
                searchNewButtons();
            }
        });
    }


    // read site html again and set functions on new #id elements
    function searchNewButtons() {

        // CONTACT INDEX MAIN PAGE
        // to work with <a> tags disable all $("a"),
        // just #id doesn't work
        $("#buttonGetContactIndex").click(function (event) {
            event.preventDefault();
            console.log("select button was clicked");
            data_in = $("form");
            console.log("data posted to the controller (RAW):");
            console.log(data_in);
            data_in = data_in.serialize();
            getPartialViewAjax('Spas/ShowContactIndex', data_in);
        });

    }


    searchNewButtons();

});