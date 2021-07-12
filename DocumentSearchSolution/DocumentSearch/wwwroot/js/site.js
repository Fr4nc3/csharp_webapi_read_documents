// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    $(document).on("keypress", 'form', function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
            return false;
        }
    });
    $('[data-toggle="tooltip"]').tooltip(); 

    // search action
    $(".search-button").click(function () {
        var searchString = $("#searchString").val();
        var filter = $("#filter").val();
        var sortField = $("#sortField").val();
        var sortDirection = $("#sortDirection").val();

        var url = "/?sortField=" + sortField + "&";
        url += "sortDirection=" + sortDirection + "&";

        if (filter !== "" && filter !=="Select Filter") {
            url += "filter=" + filter + "&";
        }

        if (searchString !== "") {
            url += "searchString=" + searchString;
        }
        //string searchString, string filter, string sortField, string sortDirection, int currentPage = 1
       console.log(url);
       window.location.href = url;
    });
    // click-table table sort
    $(".click-table").click(function () {
 
        var searchString = $("#searchString").val();
        var filter = $("#filter").val();
        var sortField = $(this).data("field");
        if (sortField == "download") {
            return false;
        }
        var selected = $(this).data("selected");;
        var sortDirection = "";
        if (selected) {
            sortDirection = $(this).data("sortdirection") == "desc" ? "asc" : "desc"; //inverse action
        } else {
            sortDirection =  "desc";
        }


        var url = "/?sortField=" + sortField + "&";
        url += "sortDirection=" + sortDirection + "&";

        if (filter !== "" && filter !== "Select Filter") {
            url += "filter=" + filter + "&";
        }

        if (searchString !== "") {
            url += "searchString=" + searchString;
        }
        //string searchString, string filter, string sortField, string sortDirection, int currentPage = 1
        console.log(url);
        window.location.href = url;
    });
});