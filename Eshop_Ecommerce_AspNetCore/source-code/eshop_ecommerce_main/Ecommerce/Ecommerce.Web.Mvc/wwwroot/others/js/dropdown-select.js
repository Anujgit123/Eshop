$(".selectBox").on("click", function (e) {
    $(this).toggleClass("show");
    var dropdownItem = e.target;
    var container = $(this).find(".selectBox__value");
    container.text(dropdownItem.text);
    $(dropdownItem)
        .addClass("active")
        .siblings()
        .removeClass("active");
});
