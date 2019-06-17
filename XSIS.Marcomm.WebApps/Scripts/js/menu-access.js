$(document).ready(function () {
    GetListMenuAccess();
});

$(document).on("click", ".btn-search", function () {
    var RoleCode = $("#RoleCode").val();
    var RoleName = $("#RoleName").val();
    var CreatedDate = $("#CreatedDate").val().replace(/\//g, '-');
    var CreatedBy = $("#CreatedBy").val();
    GetListMenuAccess(RoleCode, RoleName, CreatedDate, CreatedBy);
});

$(document).on("click", ".modalMenuAccess", function () {
    var action = $(this).attr("id");
    var id = $(this).attr("data-id");
    var code = $(this).attr("data-code");
    if (action == "Delete") {
        swal({
            title: "Delete Data ?",
            customClass: "swal-wide",
            showCancelButton: true,
            confirmButtonText: "Delete",
            cancelButtonText: "Cancel",
            confirmButtonColor: "#2196F3",
            cancelButtonColor: "#FF9800",
        }).then(isConfirm => {
            if (isConfirm.value) {
                $.ajax({
                    type: "POST",
                    data: { "RoleId": id },
                    url: "/MenuAccess/Delete/",
                    dataType: "html",
                    success: function (data) {
                        var RoleCode = $("#RoleCode").val();
                        var RoleName = $("#RoleName").val();
                        var CreatedDate = $("#CreatedDate").val().replace(/\//g, '-');
                        var CreatedBy = $("#CreatedBy").val();
                        GetListMenuAccess(RoleCode, RoleName, CreatedDate, CreatedBy);
                        var alertTitle = "Data Deleted !";
                        var alertText = "Data menu access with referential role code <strong>" + code + "</strong> has been deleted !";
                        Notify("info", alertTitle, alertText);
                    },
                    error: function (data) {
                        var alertTitle = "Error !";
                        var alertText = "Delete menu access with referential role code <strong>" + code + "</strong> has been failed !";
                        Notify("danger", alertTitle, alertText);
                    }
                });
            }
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: "/MenuAccess/" + action == "Create" ? action : action + '/' + id,
            dataType: "html",
            success: function (data) {
                $(".modal-content").html(data);
                $("#modalMenuAccess").modal("show");
            }
        });
    }
});

$("#modalMenuAccess").on("shown.bs.modal", function () {
    var action = $(this).find("section").attr("class");

    if (action == "create") {
        $(".selectedMenus").prop("disabled", true).prop("checked", false);
        var RoleId;
        var RoleName;
        $(document).on("change", "select[id='RoleName']", function () {
            RoleId = $(this).val();
            RoleName = $(this).find("option:selected").text();
            if (RoleId.trim() == "") {
                $('.selectedMenus').prop('disabled', true).prop("checked", false);
            } else {
                $('.selectedMenus').prop('disabled', false).prop("checked", false);
            }
        });

        $(".btn-create").click(function () {
            var MenusId = $('.checkbox input:checked').map(function () {
                return parseInt(this.value);
            }).get();

            if (MenusId.length === 0) {
                swalError("Please select at least one Menu!");
            } else {
                $.ajax({
                    type: "POST",
                    url: "/MenuAccess/Create/",
                    data: { "RoleId": RoleId, "MenusId": MenusId },
                    dataType: "html",
                    success: function (data) {
                        GetListMenuAccess();
                        $("#modalMenuAccess").modal("hide");
                        var alertTitle = "Data Saved !";
                        var alertText = "New menu access for role <strong>" + RoleName + "</strong> has been added !";
                        Notify("info", alertTitle, alertText);
                    },
                    error: function (data) {
                        $("#modalMenuAccess").modal("hide");
                        var alertTitle = "Error !";
                        var alertText = "Add menu access for role <strong>" + RoleName + "</strong> has been failed !";
                        Notify("danger", alertTitle, alertText);
                    }
                });
            }
        });
        return false;
    }

    if (action == "edit") {
        var pastMenusId = $('.checkbox input:checked').map(function () {
            return parseInt(this.value);
        }).get();

        $(".btn-edit").click(function (e) {
            var MenusId = $('.checkbox input:checked').map(function () {
                return parseInt(this.value);
            }).get();
            var RoleId = $("#mRoleId").val();
            var RoleNameAlert = $("input[name='mRoleName']").val();
            MenusId = MenusId.concat(pastMenusId)
                .filter(function (item, index, array) {
                    return array.indexOf(item) == array.lastIndexOf(item);
                })
            if (MenusId.length === 0) {
                swalError("No changes applied!");
            } else {
                $.ajax({
                    type: "POST",
                    data: { "RoleId": RoleId, "MenusId": MenusId },
                    url: "/MenuAccess/Edit/",
                    dataType: "html",
                    success: function (data) {
                        var RoleCode = $("#RoleCode").val();
                        var RoleName = $("#RoleName").val();
                        var CreatedDate = $("#CreatedDate").val().replace(/\//g, '-');
                        var CreatedBy = $("#CreatedBy").val();
                        GetListMenuAccess(RoleCode, RoleName, CreatedDate, CreatedBy);
                        $("#modalMenuAccess").modal("hide");
                        var alertTitle = "Data Updated !";
                        var alertText = "Menu Access for role <strong>" + RoleNameAlert + "</strong> has been updated !";
                        Notify("info", alertTitle, alertText);
                    },
                    error: function (data) {
                        $("#modalMenuAccess").modal("hide");
                        var alertTitle = "Error !";
                        var alertText = "Edit menu access for role <strong>" + RoleNameAlert + "</strong> has been failed !";
                        Notify("danger", alertTitle, alertText);
                    }
                });
            }
        });
        return false;
    }
});

function GetListMenuAccess(RoleCode, RoleName, CreatedDate, CreatedBy) {
    $.ajax({
        type: "POST",
        data: {
            "RoleCode": RoleCode,
            "RoleName": RoleName,
            "CreatedDate": CreatedDate,
            "CreatedBy": CreatedBy,
        },
        url: "/MenuAccess/Index/",
        dataType: "html",
        success: function (data) {
            $(".tbody").html(data);
        },
    });
};

function swalError(title) {
    swal({
        title: title,
        type: "error",
        customClass: "swal-wide",
        confirmButtonText: "Close",
        confirmButtonColor: "#FF9800",
    });
}

function Notify(Type, Title, Text) {
    $("#my-alert").removeClass();
    $('html, body').animate({
        scrollTop: document.body.scrollHeight
    }, 500);
    $("#my-alert").html("<strong>" + Title + "</strong> " + Text);
    $("#my-alert").addClass("alert alert-" + Type);
    $("#my-alert").hide();
    setTimeout(function () {
        $('#my-alert').fadeIn('slow');
    }, 500);
    setTimeout(function () {
        $('#my-alert').fadeOut('slow');
    }, 3000);
};