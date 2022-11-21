

$(function () {
    // ------------ Counter BEGIN ------------
    $(document).on('click', '.counter__increment, .counter__decrement', function (e) {
        var $this = $(this);
        var $counter__input = $(this).parent().find(".counter__input");
        var $currentVal = parseInt($(this).parent().find(".counter__input").val());

        //Increment
        if ($currentVal != NaN && $this.hasClass('counter__increment')) {

            $counter__input.val($currentVal + 1);
        }
        //Decrement
        else if ($currentVal != NaN && $this.hasClass('counter__decrement')) {
            if ($currentVal > 1) {
                $counter__input.val($currentVal - 1);
            }
        }
    });
    // ------------ Counter END ------------
});

//cart count ViewComponent
function CartCount() {
    $.ajax({
        url: "/getcartcount",
        type: "get",
        dataType: "html",
        //beforeSend: function (x) {
        //},
        //data: null,
        success: function (result) {
            $(".total-cart-item").html(result);
        }
    });
}

//cart summary ViewComponent
function CartSummary() {
    $.ajax({
        url: "/getcartsummary",
        type: "get",
        dataType: "html",
        success: function (result) {
            $(".cart-summary-preview").html(result);
        }
    });
}

//cart ViewComponent
function GetCart() {
    $.ajax({
        url: "/getcart",
        type: "get",
        dataType: "html",
        success: function (result) {
            $(".cart-component").html(result);
        }
    });
}

//checkoutorderpreview ViewComponent
function GetCheckoutOrderPreview() {
    $.ajax({
        url: "/getcheckoutorderpreview",
        type: "get",
        dataType: "html",
        success: function (result) {
            $(".getcheckoutorderpreview-component").html(result);
            try {
                calDeliveryCharge();
            }
            catch (err) {
                return true;
            }
            calTotalCharge();
        }
    });
}


function ItemIncrement(v) {
    console.log(v);
    $.ajax({
        url: "/cartitemincrement/" + v,
        type: "get",
        dataType: "html",
        success: function (result) {

            CartSummary();
            CartCount();
            GetCart();
            GetCheckoutOrderPreview();

        }
    });
}

function ItemDecrement(v) {
    console.log(v);
    $.ajax({
        url: "/cartitemdecrement/" + v,
        type: "get",
        dataType: "html",
        success: function (result) {

            CartSummary();
            CartCount();
            GetCart();
            GetCheckoutOrderPreview();
        }
    });
}

function CartItemRemove(v) {
    console.log(v);
    $.ajax({
        url: "/cartitemremove/" + v,
        type: "get",
        dataType: "html",
        success: function (result) {
            CartSummary();
            CartCount();
            GetCart();
            GetCheckoutOrderPreview();
        }
    });
}


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
        url: "/Product/ProductSearch/" + searchValue,
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (result) {
            console.log(result);
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