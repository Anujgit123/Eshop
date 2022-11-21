$(function () {

    $('#customer_account_tab li a').each(function () {
        var hrefurl = $(this).attr("href");
        var pathurl = window.location.pathname;
        if (hrefurl.toLowerCase() == pathurl.toLowerCase()) {
            $(this).parent('li').addClass('active');
        }
    });

})