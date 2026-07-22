//Sweet Alert پیغام موفقیت آمیز
function success(msg) {

    Swal.fire({
        icon: "success",
        title: "موفق",
        text: msg,
        confirmButtonText: "باشه",
        timer: 1800,
        timerProgressBar: true
    });

}
//Sweet Alert پیغام خطا
function error(msg) {

    Swal.fire({
        icon: "error",
        title: "خطا",
        text: msg,
        confirmButtonText: "باشه"
    });

}

$(function () {

    LoadProducts();

});

function LoadProducts() {
    $("#ProductGrid").load(
        "/ProductManagement/ProductList?ts=" + new Date().getTime()
    );
}

// Create
$(document).on("click", "#btnAdd", function () {

    $.get("/ProductManagement/Create", function (result) {

        $("#modalBody").html(result);

        $("#productModal").modal("show");

    });

});

// Save Create
$(document).on("submit", "#frmCreate", function (e) {

    e.preventDefault();

    var formData = new FormData(this);

    $.ajax({

        url: "/ProductManagement/Create",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,

        headers: {
            RequestVerificationToken:
                $('input[name="__RequestVerificationToken"]', this).val()
        },

        success: function (res) {

            if (res.success) {

                $("#productModal").modal("hide");

                LoadProducts();

                success(res.message);

            }
            else {

                error(res.message);

            }
        }
    });

});

// Edit
$(document).on("click", ".btnEdit", function () {

    var id = $(this).data("id");

    $.get("/ProductManagement/Edit",
        {
            productID: id
        },
        function (result) {

            $("#modalBody").html(result);

            $("#productModal").modal("show");

        });

});

// Save Edit
$(document).on("submit", "#frmEdit", function (e) {

    e.preventDefault();

    var formData = new FormData(this);

    $.ajax({

        url: "/ProductManagement/Edit",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,

        headers: {
            RequestVerificationToken:
                $('input[name="__RequestVerificationToken"]', this).val()
        },

        success: function (res) {

            if (res.success) {

                $("#productModal").modal("hide");

                LoadProducts();

                success(res.message);

            }
            else {

                error(res.message);

            }
        }
    });

});

// Details
$(document).on("click", ".btnDetails", function () {

    var id = $(this).data("id");

    $.get("/ProductManagement/Details",
        {
            productID: id
        },
        function (result) {

            $("#modalBody").html(result);

            $("#productModal").modal("show");

        });

});

// Delete
$(document).on("click", ".btnDelete", function () {

    var id = $(this).data("id");

    Swal.fire({
        title: "حذف محصول",
        text: "آیا از حذف این محصول مطمئن هستید؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "بله، حذف شود",
        cancelButtonText: "انصراف",
        reverseButtons: true
    }).then((result) => {

        if (!result.isConfirmed)
            return;

        $.ajax({

            url: "/ProductManagement/Delete",
            type: "POST",

            data: {
                productID: id
            },

            headers: {
                RequestVerificationToken:
                    $('#frmAntiForgery input[name="__RequestVerificationToken"]').val()
            },

            success: function (res) {

                if (res.success) {

                    LoadProducts();

                    success(res.message);

                }
                else {

                    error(res.message);

                }

            }

        });

    });

});

// Remove Picture
$(document).on("click", ".btnRemovePicture", function () {

    var id = $(this).data("id");

    Swal.fire({

        title: "حذف تصویر",
        text: "آیا از حذف تصویر این محصول مطمئن هستید؟",
        icon: "warning",

        showCancelButton: true,
        confirmButtonText: "بله، حذف شود",
        cancelButtonText: "انصراف",

        reverseButtons: true

    }).then((result) => {

        if (!result.isConfirmed)
            return;

        $.ajax({

            url: "/ProductManagement/RemovePicture",
            type: "POST",

            data: {

                productID: id,
                __RequestVerificationToken:
                    $('#frmAntiForgery input[name="__RequestVerificationToken"]').val()

            },

            success: function (res) {

                if (res.success) {

                    LoadProducts();

                    success(res.message);

                }
                else {

                    error(res.message);

                }

            }

        });

    });

});

//پیش نمایش عکس محصول Create
$(document).on("change", "#ImageFileCreate", function () {

    const file = this.files[0];

    if (!file)
        return;

    const reader = new FileReader();

    reader.onload = function (e) {

        $("#imgPreviewCreate")
            .attr("src", e.target.result)
            .show();

    };

    reader.readAsDataURL(file);

});

//پیش نمایش عکس محصول Edit 
$(document).on("change", "#ImageFileEdit", function () {

    const file = this.files[0];

    if (!file)
        return;

    const reader = new FileReader();

    reader.onload = function (e) {

        $("#imgPreviewEdit")
            .attr("src", e.target.result);

    };

    reader.readAsDataURL(file);

});


// جستجوی محصول
function SearchProducts() {

    let data = {
        ProductName: $("#ProductName").val(),
        IsAvailable: $("#IsAvailable").val(),
        ProductPoint: $("#ProductPoint").val()
    };

    $.get("/ProductManagement/Search", data, function (result) {

        $("#ProductGrid").html(result);

    });

}

$("#btnSearchProduct").click(function () {

    SearchProducts();

});


$("#btnReset").click(function () {

    $("#ProductName").val("");
    $("#IsAvailable").val("");
    $("#ProductPoint").val("");

    LoadProducts();
});


//صفحه بندی
function LoadProductPage(pageIndex) {

    let data = {
        PageIndex: pageIndex,
        ProductName: $("#ProductName").val(),
        IsAvailable: $("#IsAvailable").val(),
        ProductPoint: $("#ProductPoint").val()
    };


    $.get("/ProductManagement/Search", data, function (result) {

        $("#ProductGrid").html(result);

    });

}
