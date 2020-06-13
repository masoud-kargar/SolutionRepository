// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

import { type } from "os";

// Write your JavaScript code.
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#exampleModalCenter .modal-body").html(res);
            $("#exampleModalCenter .modal-title").html(title);
            $("#exampleModalCenter").modal('show');
        }
    })
};
jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.IsValid) {
                    $("#viewAll").html(res.html);
                    $("#exampleModalCenter .modal-body").html('');
                    $("#exampleModalCenter .modal-title").html('');
                    $("#exampleModalCenter").modal('hide');
                } else {
                    $("#exampleModalCenter .modal-body").html(res.html);
                }
            },
            error: function (err) {
                console.log(err);
            }
        })

    } catch (e) {
        console.log(e);
    }

    return false;
};