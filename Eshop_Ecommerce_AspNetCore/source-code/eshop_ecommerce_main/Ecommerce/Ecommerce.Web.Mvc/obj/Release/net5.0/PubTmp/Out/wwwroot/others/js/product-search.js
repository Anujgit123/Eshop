$("#productSearch").keyup(function () {
    var search = $(this).val() == '' ? null : $(this).val();
    if (search != null && search.length > 2) {
        var result = LiveSearch(search);
        $('#productSearchResult').html(result);
    } else {
        $('#productSearchResult').html('');
    }

});

function LiveSearch(searchValue) {
    var response;
    $.ajax({
        url: "/Stock/ProductSearch/" + searchValue,
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (result) {
            var li = '';
            $.each(result, function () {
                li += `<a href="/Shop/ProductDetails/` + this.productId + `">
                                    <div class="d-flex justify-content-start align-item-center">
                                        <div>
                                            <img src="/`+ this.imagePreview + `">
                                        </div>
                                        <div class="d-flex align-items-start flex-column ms-2">
                                            <span class="d-block text-dark">` + this.productName + `</span>
                                            <small class="fw-bold">`+ this.productCategory + `</small>
                                        </div>
                                    </div>
                                </a>`


            });
            response = li;
        },
        error: function (e) {
            console.log(e);
        }
    });
    return response;
}