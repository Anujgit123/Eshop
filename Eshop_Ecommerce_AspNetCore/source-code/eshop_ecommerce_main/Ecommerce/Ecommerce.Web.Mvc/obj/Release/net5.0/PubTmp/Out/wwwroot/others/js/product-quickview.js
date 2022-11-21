function LoadQuickViewScript(id) {
    $('select').niceSelect();
    var id = $('.productId').html();
    var color = "";
    var size = "";
    //var size = null;
    var variable = null;
    //console.log(id, color, size);
    //var selectsizeoption = '<option value="">Select Size</option>';


    if ($(".size-block").length != 0) {
        size = $(".product-size-dropdown").val()
        if ($(".color-block").length != 0) {
            color = $(".select-color").attr("su-color-filter");
            filterSizebyColor(id, color);
        } else {
            filterData(id, color, size);
        }

    }

    if ($(".color-block").length != 0) {
        color = $(".select-color").attr("su-color-filter");
        filterSizebyColor(id, color);
    }


    function filterSizebyColor(id, color) {
        $.ajax({
            url: "/filtersizebycolor/" + id + "?color=" + color,
            type: 'GET',
            dataType: 'json', // added data type
            success: function (result) {
                //console.log(result);
                var options = "";
                if (result != null) {
                    $.each(result, function () {
                        options += '<option value="' + this.sizeId + '">' + this.name + '</option>';
                    });
                }

                $(".product-size-dropdown").html(options);
                $('select').niceSelect('update');

                var color = $(".select-color").attr("su-color-filter");
                var size = $(".product-size-dropdown").val() == null ? "" : $(".product-size-dropdown").val();
                if ($(".product-size-dropdown").val() !== "" || $(".product-size-dropdown").val() !== "undefined") {
                    filterData(id, color, size);
                }
            },
            error: function (e) {
                alert("Error!" + e);
            }
        });
    }


    $(document).on('click', '[su-color-filter]', function () {
        $('[su-color-filter]').removeClass('select-color');
        $(this).addClass('select-color');
        var color = $(this).attr("su-color-filter");
        var size = $(".product-size-dropdown").val();
        //console.log(id, color, size);

        filterSizebyColor(id, color);
        //$.when(filterSizebyColor(id, color, '')).done(function () {
        //    filterData(id, color, size);
        //});

    });

    $(document).on('change', '[su-size-filter]', function () {
        var size = $(this).val();
        var color = $(".select-color").attr("su-color-filter");
        color = color == null ? null : color;
        filterData(id, color, size);
    });


    function filterData(id, color, size) {
        $.ajax({
            url: "/filterdetails/" + id + "?color=" + color + "&size=" + size,
            type: 'GET',
            async: false,
            dataType: 'json', // added data type
            success: function (result) {
                if (result != null) {
                    $(".product-sku").html('<span class="text-dark">SKU: </span>' + result.sku);
                    $(".product-price").html(result.price);
                    $("#addToCart").attr("product-varientId", result.varientId);
                    $('#product-main-image img').attr('src', '/' + result.varientImage);

                    var outOfStockThreshold = $('.out-of-stock-threshold').html();
                    var lowStockThreshold = $('.low-stock-threshold').html();
                    var isLowStockNotificationEnabled = $('.is-lowstock-notification-enabled').html();
                    var isOutOfStockNotificationEnabled = $('.is-outofstock-notification-enabled').html();

                    $(".input_number").val(1);
                    $(".stock-status").html('');
                    $("#addToCart").removeClass("disabled");
                    $(".input_number_increment").removeClass("disabled");
                    $(".input_number_decrement").removeClass("disabled");

                    if (result.quantity <= outOfStockThreshold) {
                        if (isOutOfStockNotificationEnabled === 'true'){
                            $(".stock-status").html('<span class="text-danger fw-bold">Out of stock</span>');
                        };

                        $("#addToCart").addClass("disabled");
                        $(".input_number_increment").addClass("disabled");
                        $(".input_number_decrement").addClass("disabled");
                    } else if (result.quantity <= lowStockThreshold && isLowStockNotificationEnabled === 'true') {
                        $(".stock-status").html('<span class="text-warning fw-bold">Only ' + result.quantity + ' available</span>');
                    }else {
                        $(".stock-status").html('<span class="text-success fw-bold">In Stock</span>');
                    }
                }
            },
            error: function (e) {
                alert("Error!" + e);
            }
        });

        $('select').niceSelect('update');
    }

    $(document).on('click', '#addToCart', function () {
        var data = JSON.stringify({
            VariableId: $(this).attr("product-varientId"),
            Qty: $('#qty').val()
        });

        var obj = jQuery.parseJSON(data);
        return $.ajax({
            url: "/addtocart",
            type: 'POST',
            data: obj,
            success: function (result) {
                //console.log(result);
                CartCount();
                CartSummary();
                notifier.open({ type: "info", message: "Item Added to Cart" });
                //location.reload();
            },
            error: function () {
                alert("Error!");
            }
        });
    });
}