$(document).ready(function () {
    var canClick = true;  // متغیر برای کنترل محدودیت زمان
    var clickTimeout;  // متغیر تایم‌اوت

    // باز و بسته کردن دراپ‌داؤن
    $('.dropdown-toggle').on('click', function () {
        if (canClick) return;  // اگر محدودیت کلیک فعال است، هیچ کاری انجام نده

        var menu = $(this).next('.dropdown-menu');

        // بستن تمام دراپ‌داؤن‌های دیگر
        $('.dropdown-menu').not(menu).removeClass('show');

        // تغییر وضعیت نمایش برای دراپ‌داؤن فعلی
        menu.toggleClass('show');

        // تنظیم تایم‌اوت برای محدود کردن کلیک‌ها
        canClick = false;
        clearTimeout(clickTimeout);
        clickTimeout = setTimeout(function () {
            canClick = true;  // پس از 30 ثانیه، دوباره کلیک مجاز است
        }, 30000);  // 30 ثانیه
    });

    // بستن دراپ‌داؤن‌ها زمانی که کلیک خارج از آن‌ها انجام شود
    $(document).click(function (e) {
        if (!$(e.target).closest('.dropdown').length) {
            $('.dropdown-menu').removeClass('show');
        }
    });
});




document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById('menuToggle'); // دکمه همبرگر
    const sidebar = document.getElementById('sidebar'); // منو کناری
    const manageTagButton = document.querySelector('[data-action="manageTag"]'); // دکمه مدیریت تگ

    // باز و بسته کردن منو با کلیک روی دکمه همبرگر
    toggleButton.addEventListener("click", function (event) {
        event.stopPropagation(); // جلوگیری از انتشار کلیک به سایر بخش‌ها
        sidebar.classList.toggle("collapsed"); // تغییر وضعیت منو
        sidebar.style.visibility = sidebar.classList.contains("collapsed") ? "visible" : "hidden";
    });

    // جلوگیری از بسته شدن منو با کلیک داخل آن
    sidebar.addEventListener("click", function (event) {
        event.stopPropagation(); // جلوگیری از انتشار کلیک به سایر بخش‌ها
    });

    // بستن منو با کلیک در سایر بخش‌های صفحه (به جز دکمه همبرگر، دکمه مدیریت تگ و پیجینیشن)
    document.addEventListener("click", function (event) {
        const isToggleButton = toggleButton.contains(event.target); // آیا کلیک روی دکمه همبرگر بوده؟
        const isSidebar = sidebar.contains(event.target); // آیا کلیک داخل منو بوده؟
        const isPagination = event.target.closest('.pagination') !== null; // آیا کلیک روی پیجینیشن بوده؟
        const isManageTagButton = manageTagButton && manageTagButton.contains(event.target); // آیا کلیک روی دکمه مدیریت تگ بوده؟

        // اگر کلیک روی این دکمه‌ها نبود، منو بسته شود
        if (!isToggleButton && !isSidebar && !isPagination && !isManageTagButton) {
            sidebar.classList.add("collapsed");
            sidebar.style.visibility = "hidden"; // مخفی کردن منو
        }
    });

    // جلوگیری از تغییر وضعیت منو با کلیک روی لینک‌های پیجینیشن
    document.querySelectorAll('.pagination a').forEach(link => {
        link.addEventListener('click', function (event) {
            event.stopPropagation(); // جلوگیری از انتشار کلیک به سند
        });
    });


    // مدیریت دکمه مدیریت تگ
    if (manageTagButton) {
        manageTagButton.addEventListener("click", function (event) {
            event.stopPropagation(); // جلوگیری از انتشار کلیک به سایر بخش‌ها
            sidebar.classList.add("collapsed"); // بسته شدن منو
            sidebar.style.visibility = "hidden"; // مخفی کردن منو
        });
    }
});
document.addEventListener("DOMContentLoaded", function () {
    let menuTitles = document.querySelectorAll(".menu-title");
    let menuItems = document.querySelectorAll(".menu-content a");

    // دریافت مسیر فعلی صفحه
    let currentPath = window.location.pathname.toLowerCase();

    // دریافت آخرین منوی باز از localStorage
    let lastOpenMenu = localStorage.getItem("lastOpenMenu");

    // 1️⃣ بررسی و اعمال استایل به آیتم کلیک‌شده در منو
    menuItems.forEach(item => {
        let itemPath = item.getAttribute("href").toLowerCase();

        if (currentPath.includes(itemPath)) {
            item.classList.add("active"); // اضافه کردن کلاس فعال
            let parentMenu = item.closest(".menu-content");

            if (parentMenu) {
                let menuId = `#${parentMenu.id}`;

                // ذخیره فقط همین منو در localStorage
                localStorage.setItem("lastOpenMenu", menuId);

                // باز کردن فقط همین منو و بستن بقیه
                document.querySelectorAll(".menu-content").forEach(menu => {
                    if (`#${menu.id}` === menuId) {
                        menu.classList.add("show");
                    } else {
                        menu.classList.remove("show");
                    }
                });
            }
        }
    });

    // 2️⃣ بستن تمام منوها و فقط باز نگه داشتن آخرین منو از localStorage
    if (lastOpenMenu) {
        document.querySelectorAll(".menu-content").forEach(menu => {
            if (`#${menu.id}` === lastOpenMenu) {
                menu.classList.add("show");
            } else {
                menu.classList.remove("show");
            }
        });
    }

    // 3️⃣ کنترل باز و بسته شدن منو هنگام کلیک روی عنوان منو
    menuTitles.forEach(title => {
        title.addEventListener("click", function () {
            let target = document.querySelector(this.getAttribute("data-target"));

            if (target) {
                let isOpen = target.classList.contains("show"); // بررسی وضعیت قبلی
                let menuId = `#${target.id}`;

                // باز یا بسته کردن منو
                if (!isOpen) {
                    localStorage.setItem("lastOpenMenu", menuId);
                    target.classList.add("show");
                } else {
                    localStorage.removeItem("lastOpenMenu");
                    target.classList.remove("show");
                }

                // بستن بقیه منوها
                document.querySelectorAll(".menu-content").forEach(menu => {
                    if (`#${menu.id}` !== menuId) {
                        menu.classList.remove("show");
                    }
                });
            }
        });
    });
});
;

//SWEETALERT SECTION
function SuccessMessage(SuccessTxt) {
    Swal.fire({
        icon: 'success',
        title: 'با موفقیت انجام شد ',
        text: SuccessTxt,
    });
}
function ErrorMessage(ErrorTxt) {
    Swal.fire({
        icon: 'error',
        title: 'خطا',
        text: ErrorTxt,
    });
}

function doSearch(pageIndex) {
    let action = $("#btnSearch").attr("data-action");
    let controller = $("#btnSearch").attr("data-controller");
    let formid = "#" + $("#btnSearch").attr("data-form-id");

    if (!action || !controller || !$(formid).length) {
        console.error("مشکل در مقادیر دکمه یا فرم جستجو.");
        return;
    }

    let sendingUrl = `/${controller}/${action}`;

    let formData = $(formid).serialize(); // داده‌های فرم سرچ
    if (pageIndex !== undefined) {
        formData += `&PageIndex=${pageIndex}`; // اگر پیج جدیدی انتخاب شده
    }

    $.get(sendingUrl, formData + "&_=" + new Date().getTime())
        .done(function (response) {
            $("#dvList").html(response);
        })
        .fail(function () {
            alert("خطا در جستجو رخ داده است.");
        });
}

// سرچ کلی
$("#btnSearch").on("click", function () {
    doSearch();
});

// اگر کسی تایپ کرد یا تغییر داد
$(document).on("change", ".drpItem", function () {
    doSearch();
});

$(document).on("keyup", ".inputSearch", function () {
    doSearch();
});

// 📢 صفحه‌بندی ایجکسی:
$(document).on("click", ".pagination a", function (e) {
    e.preventDefault();
    const pageUrl = $(this).attr('href');
    const urlParams = new URLSearchParams(pageUrl.split('?')[1]);
    const pageIndex = urlParams.get("PageIndex");

    doSearch(pageIndex); // با ایندکس جدید سرچ کن
});


//----------

// DELETE
let deleteDebounce;
function removeEntity(button) {
    if (deleteDebounce) {
        clearTimeout(deleteDebounce);
    }

    deleteDebounce = setTimeout(() => {
        let entityTypeID = button.data("id");
        let controller = button.data("controller");
        let action = button.data("action");

        if (!controller || !action || !entityTypeID) {
            ErrorMessage("❌ خطا: `controller` یا `action` یا `categoryFeatureID` مقدار ندارد.");
            return;
        }

        if (confirm("آیا مطمئن هستید؟")) {
            button.prop("disabled", true);

            let sendingUrl = `/${controller}/${action}`;
            let sendingData = { entityTypeID: entityTypeID };

            $.post(sendingUrl, sendingData, function (operationResult) {
                if (operationResult.success) {
                    button.closest("li").fadeOut(300, function () {
                        $(this).remove();
                    });
                    SuccessMessage(operationResult.message);
                } else {
                    ErrorMessage(operationResult.message);
                }
            }).fail(function () {
                button.prop("disabled", false);
                ErrorMessage("❌ خطا در ارتباط با سرور");
            });
        }
    }, 300);
}
//Call DELETE
$(document).on("click", ".removeEntity", function () {
    removeEntity($(this)); // 🔥 صدا زدن متد حذف ویژگی بدون لود مجدد
});

//SEARCH
$(document).ready(function () {
    $(document).on("keyup", ".searchEntity", function () {
        let input = $(this);
        let searchTerm = input.val().trim();
        let entityID = input.data("entity-id");
        let controller = input.data("controller");
        let action = input.data("action");
        let searchEntityNumber = input.data("search-entity");
        console.log("searchEntityNumber:", searchEntityNumber);

        if (searchTerm.length < 1) {
            $("#List").html("");
            return;
        }


        let sendingUrl = `/${controller}/${action}`;
        let sendingData = { searchTerm: searchTerm, entityID: entityID };


        function createProductDetailedListItemHtml(result, controller) {
            console.log("Inside createProductDetailedListItemHtml function");
            console.log("Result data:", result);  // بررسی داده‌های ورودی
            return `
            <li class="list-group-item d-flex justify-content-between align-items-center" 
                data-id="${result.entityID}">
                ${result.entityName} 
                <span>(برند: ${result.brandName})</span>
                <span>(دسته‌بندی: ${result.categoryName})</span>
                <span>(کد: ${result.codeNumber})</span>
                <div class="action-buttons">
                    <button class="btn btn-success btn-action addEntity" 
                        data-id="${result.entityID}"
                        data-controller="${controller}" 
                        data-search-entity="${searchEntityNumber}"
                        data-action="AddEntity">
                    افزودن
                    </button>
                </div>
            </li>`;
        }

        function createDetailedListItemHtml(result, controller) {
            console.log("Inside createDetailedListItemHtml function");
            console.log("Result data:", result);
            return `
            <li class="list-group-item d-flex justify-content-between align-items-center" 
                data-id="${result.entityID}">
                ${result.entityName} 
                <div class="action-buttons">
                    <button class="btn btn-success btn-action addEntity" 
                        data-id="${result.entityID}"
                        data-controller="${controller}" 
                        data-search-entity="${searchEntityNumber}"
                        data-action="AddEntity">
                    افزودن
                    </button>
                </div>
            </li>`;
        }

        function createCustomerDetailedListItemHtml(result, controller) {
            console.log("Inside createCustomerDetailedListItemHtml function");
            console.log("Result data:", result);  // بررسی داده‌های ورودی
            return `
            <li class="list-group-item d-flex ml-4 justify-content-between align-items-center" 
                data-id="${result.entityID}">
                ${result.entityName} 
                <span>(ایمیل: ${result.email})</span>
                <span>(نام کاربری: ${result.userName})</span>
                <span>(شماره همراه: ${result.phoneNumber})</span>
                <div class="action-buttons">
                    <button class="btn btn-success btn-action addEntity" 
                        data-id="${result.entityID}"
                        data-controller="${controller}" 
                        data-search-entity="${searchEntityNumber}"
                        data-action="AddEntity">
                    افزودن
                    </button>
                </div>
            </li>`;
        }


        $.get(sendingUrl, sendingData, function (response) {
            console.log("Response from server:", response);

            let resultHtml = "<ul class='list-group'>";

            if (response.length > 0) {
                console.log("Calling the appropriate function based on searchEntityNumber");

                resultHtml = response.map(result => {
                    if (searchEntityNumber === 1) {
                        console.log("Calling createProductDetailedListItemHtml for searchEntityNumber = 1");
                        return createProductDetailedListItemHtml(result, controller);  // استفاده از این فانکشن
                    }
                    if (searchEntityNumber === 2) {
                        console.log("Calling createProductDetailedListItemHtml for searchEntityNumber = 2");
                        return createDetailedListItemHtml(result, controller);  // استفاده از این فانکشن
                    }
                    if (searchEntityNumber === 3) {
                        console.log("Calling createProductDetailedListItemHtml for searchEntityNumber = 3");
                        return createDetailedListItemHtml(result, controller);  // استفاده از این فانکشن
                    }
                    if (searchEntityNumber === 4) {
                        console.log("Calling createCustomerDetailedListItemHtml for searchEntityNumber = 4");
                        return createCustomerDetailedListItemHtml(result, controller);  // استفاده از این فانکشن
                    }
                }).join('');
            } else {
                resultHtml += "<li class='list-group-item'>رکوردی یافت نشد.</li>";
            }

            resultHtml += "</ul>";
            $("#List").html(resultHtml);
        }).fail(function () {
            console.log("Request failed");
            ErrorMessage("❌ خطا در دریافت داده‌های جستجو.");
        });
    });
});

//ADD
$(document).on("click", ".addEntity", function () {
    let button = $(this);
    let firstEntityID = $("input[data-entity-id]").data("entity-id");
    let secondEntityID = button.data("id");
    let controller = button.data("controller");
    let action = button.data("action");

    if (!controller || !action || !firstEntityID || !secondEntityID) {
        ErrorMessage("❌ خطا: `controller` یا `action` یا `firstEntityID` یا `secondEntityID` مقدار ندارد.");
        return;
    }

    let sendingUrl = `/${controller}/${action}`;
    let sendingData = { firstEntityID: firstEntityID, secondEntityID: secondEntityID };
    $.post(sendingUrl, sendingData, function (response) {
        console.log(response);

        if (!response.success) {
            console.error(response.message);
            ErrorMessage(response.message);
            return;
        }

        //SuccessMessage(response.message);

        setTimeout(function () {
            location.reload();
        }, 300);

    }).fail(function () {
        ErrorMessage("❌ خطا در ارتباط با سرور.");
    });
});


$(document).ready(function () {
    $('#supplierSearch').on('input', function () {
        var searchTerm = $(this).val();

        if (searchTerm.length >= 1) {
            if ($('#BrandID').val() == '-1') {
                $('#BrandID').val(null);
            }
            if ($('#CategoryID').val() == '-1') {
                $('#CategoryID').val(null);
            }
            console.log('Input typed:', searchTerm);

            $.get('/Product/SearchSuppliers', { searchTerm: searchTerm }, function (data) {
                console.log('Search results:', data);

                var options = '<option value="">انتخاب فروشنده</option>';

                if (data.success && data.data.length > 0) {
                    var optionsList = data.data.map(function (supplier) {
                        return `<option value="${supplier.supplierID}" data-suppliername="${supplier.supplierName}" data-phone="${supplier.phoneNumber}" data-nickName="${supplier.supplierNickName}">${supplier.supplierName} - ${supplier.phoneNumber} - ${supplier.supplierNickName}</option>`;
                    });

                    options += optionsList.join('');
                    $('#supplierSearch').next('select').html(options);
                } else {
                    options += '<option value="">فروشنده یافت نشد</option>';
                    $('#supplierSearch').next('select').html(options);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.log("GET error:", textStatus, errorThrown);
            });
        }
    });

    $('#supplierSearch').next('select').on('change', function () {
        var selectedSupplierID = $(this).val();
        if (selectedSupplierID) {
            $('#SupplierID').val(selectedSupplierID);
        }
    });
});


$(document).ready(function () {
    $('#customerSearch').on('input', function () {
        var searchTerm = $(this).val();

        if (searchTerm.length > 0) {
            console.log('Input typed:', searchTerm);

            $.get('/Address/SearchCustomers', { searchTerm: searchTerm }, function (data) {
                console.log('Search results:', data);

                var options = '<option value="">انتخاب مشتری</option>';

                if (data.success && data.data.length > 0) {
                    var optionsList = data.data.map(function (customer) {
                        return `<option value="${customer.entityID}" data-customername="${customer.entityName}" data-phone="${customer.phoneNumber}" data-userName="${customer.userName}">${customer.entityName} - ${customer.phoneNumber} - ${customer.userName}</option>`;
                    });

                    options += optionsList.join('');
                    $('#customerSearch').next('select').html(options);
                } else {
                    options += '<option value="">مشتری یافت نشد</option>';
                    $('#customerSearch').next('select').html(options);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.log("GET error:", textStatus, errorThrown);
            });
        }
    });

    $('#customerSearch').next('select').on('change', function () {
        var selectedCustomerID = $(this).val();
        if (selectedCustomerID) {
            $('#CustomerID').val(selectedCustomerID);
        }
    });
});


