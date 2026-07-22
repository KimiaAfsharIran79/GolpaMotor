let currentWarrantyPage = 0;

$(function () {

    LoadWarrantyCards();

    LoadProducts();

});


function validate12DigitNumber(value, fieldName) {

    value = value.trim();

    if (value === "") {
        Swal.fire({
            icon: "warning",
            title: "خطا",
            text: `${fieldName} را وارد کنید.`
        });
        return false;
    }

    if (value.includes(" ")) {
        Swal.fire({
            icon: "warning",
            title: "خطا",
            text: `${fieldName} نباید فاصله داشته باشد.`
        });
        return false;
    }

    if (!/^[0-9]{12}$/.test(value)) {
        Swal.fire({
            icon: "warning",
            title: "خطا",
            text: `${fieldName} باید دقیقاً ۱۲ رقم انگلیسی باشد.`
        });
        return false;
    }

    return true;
}

function LoadProducts() {

    $.get("/WarrantyManagement/GetProducts",
        function (data) {

            let ddl = $("#WarrantyProduct");

            ddl.empty();

            ddl.append(
                `<option value="">
        همه محصولات
        </option>`
            );


            $.each(data, function (i, item) {

                ddl.append(
                    `<option value="${item.id}">
            ${item.name}
            </option>`
                );

            });


            // بعد از پر شدن محصولات
            LoadWarrantyCards();

        });

}
//function LoadWarrantyCards() {

//    $("#WarrantyGrid").load(
//        "/WarrantyManagement/WarrantyCardList?ts=" + new Date().getTime()
//    );

//}
function LoadWarrantyCards() {

    let searchModel = {
        Search: $("#WarrantySearch").val() || "",
        ProductID: $("#WarrantyProduct").val() || null,
        IsRegistered: $("#WarrantyStatus").val() || null,
        PageIndex: currentWarrantyPage,
        PageSize: 10
    };


    $("#WarrantyGrid").load(
        "/WarrantyManagement/WarrantyCardList?" + $.param(searchModel)
    );
}

function LoadWarrantyPage(pageIndex) {

    if (pageIndex < 0)
        return;

    currentWarrantyPage = pageIndex;

    LoadWarrantyCards();
}

$("#btnWarrantyFilter").click(function () {

    currentWarrantyPage = 0;

    LoadWarrantyCards();

});

//آپلود اکسل
$(document).on("click", "#btnUploadExcel", function () {

    if (!$("#ProductID").val()) {

        Swal.fire({
            icon: "warning",
            title: "انتخاب محصول",
            text: "ابتدا محصول مورد نظر را انتخاب کنید.",
            confirmButtonText: "باشه",
            confirmButtonColor: "#f59e0b"
        });

        return;
    }

    if (!$("#ExcelFile")[0].files.length) {

        Swal.fire({
            icon: "warning",
            title: "انتخاب فایل",
            text: "ابتدا فایل اکسل را انتخاب کنید.",
            confirmButtonText: "باشه",
            confirmButtonColor: "#f59e0b"
        });

        return;
    }

    var formData = new FormData();

    formData.append("ProductID", $("#ProductID").val());

    formData.append("ExcelFile", $("#ExcelFile")[0].files[0]);

    $.ajax({

        url: "/WarrantyManagement/UploadExcel",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,

        success: function (res) {

            if (res.success) {


                let icon = "success";
                let title = "بارگذاری موفق";


                if (res.insertedCount === 0 && res.duplicateCount > 0) {
                    icon = "warning";
                    title = "همه رکوردها تکراری هستند";
                }


                Swal.fire({
                    icon: icon,
                    title: title,
                    html:
                        `<div style="text-align:right;direction:rtl">
                    <p>✅ تعداد ثبت شده: <b>${res.insertedCount}</b> رکورد</p>
                    <p>⚠️ تعداد تکراری: <b>${res.duplicateCount}</b> رکورد</p>
                    <p>⛔ تعداد خالی: <b>${res.emptyCount}</b> رکورد</p>
                </div>`,
                    confirmButtonText: "باشه",
                    confirmButtonColor: "#198754"
                }).then(() => {

                    currentWarrantyPage = 0;
                    LoadWarrantyCards();

                    // اگر خواستی فرم هم ریست شود
                    // $("#frmUploadExcel")[0].reset();
                    $("#ExcelFile").val("");
                });

            } else {

                Swal.fire({
                    icon: "error",
                    title: "خطا",
                    text: res.message,
                    confirmButtonText: "باشه",
                    confirmButtonColor: "#dc3545"
                });

            }
        },
        error: function () {

            Swal.fire({
                icon: "error",
                title: "خطا",
                text: "ارتباط با سرور برقرار نشد.",
                confirmButtonText: "باشه",
                confirmButtonColor: "#dc3545"
            });

        }

    }); 

}); 

//تولید کد گارانتی
$(document).on("click", "#btnGenerate", function () {

    if (!$("#ProductID").val()) {

        Swal.fire({
            icon: "warning",
            title: "انتخاب محصول",
            text: "ابتدا محصول مورد نظر را انتخاب کنید."
        });

        return;
    }

    if (!validate12DigitNumber($("#SerialFrom").val(), "شماره سریال از"))
        return;

    if (!validate12DigitNumber($("#SerialTo").val(), "شماره سریال تا"))
        return;

    if (!validate12DigitNumber($("#WarrantyFrom").val(), "کد گارانتی از"))
        return;

    if (!validate12DigitNumber($("#WarrantyTo").val(), "کد گارانتی تا"))
        return;

    var model = {

        ProductID: $("#ProductID").val(),

        SerialFrom: $("#SerialFrom").val(),

        SerialTo: $("#SerialTo").val(),

        CodeFrom: $("#WarrantyFrom").val(),

        CodeTo: $("#WarrantyTo").val(),

        Count: $("#Count").val(),

        ValidityMonths: $("#ValidityMonths").val(),
    };

    $.ajax({

        url: "/WarrantyManagement/GenerateWarrantyCodes",

        type: "POST",

        data: model,

        success: function (res) {

            console.log(res);

            if (res.success) {

                Swal.fire({
                    icon: "success",
                    title: "موفق",
                    text: res.message
                }).then(function () {

                    currentWarrantyPage = 0;
                    LoadWarrantyCards();

                });

            }
            else {

                Swal.fire({
                    icon: "error",
                    title: "خطا",
                    text: res.message
                });

            }

        },

        error: function () {

            Swal.fire({
                icon: "error",
                title: "خطا",
                text: "ارتباط با سرور برقرار نشد."
            });

        }

    });

});
