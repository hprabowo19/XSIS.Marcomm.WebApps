$(document).ready(function () {
    ListSouvenirStock();
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };
});

$(document).on("click", ".btn-search", function () {
    var data = {
        "code": $("#code").val(),
        "requestBy": $("#requestBy").val(),
        "requestDate": $("#requestDate").val().replace(/\//g, "-"),
        "dueDate": $("#dueDate").val().replace(/\//g, "-"),
        "status": $("#status").val(),
        "createdDate": $("#createdDate").val().replace(/\//g, "-"),
        "createdBy": $("#createdBy").val(),
    };
    ListSouvenirStock(data);
});

$(document).on("click", ".modalMenuAccess", function () {
    var action = $(this).attr("id");
    if (action == "Create" || action == "Detail") {
        swalError(action + " feature will not be implemented.");
        return false;
    }

    var SessionLogin = $("#SessionLogin").val();
    if (SessionLogin == "") {
        swalError("Login Please !")
        return false;
    }

    var id = $(this).attr("data-id");
    var status = $(this).attr("data-status");
    var statusText = $(this).attr("data-text");
    var accessId = $(this).attr("data-access");

    if (action == "Edit" && (status == 3 || status == 5)) {
        if (accessId != SessionLogin) {
            swalError("Not Allowed !")
            return false;
        }
        var data = { "id": id };
        View(data, status);
    } else if (action == "Edit" && status == 4) {
        var SessionRole = $("#RoleSessionLogin").val();
        if (SessionLogin != 1) {
            swalError("Not Allowed !")
            return false;
        } else {
            $.ajax({
                type: "GET",
                url: "/SouvenirRequest/SettlementSouvenirRequest/" + id,
                dataType: "html",
                success: function (data) {
                    ListSouvenirBySouvenirTransactionId(id, status);
                    $(".modal-content").html(data);
                    $("#modalMenuAccess").modal("show");
                }
            });
        }
    } else {
        swalError("Action edit with " + statusText + " status will not be implemented.");
    }
});

$(document).on("click", ".btn-settlement", function () {
    var QtySettlements = $("input[name='qtySettlement']").map(function () {
        return parseInt(this.value);
    }).get().filter(function (n) {
        return n || n === 0;
    });

    var SouvenirItemId = $("input[name='item.id']").map(function () {
        return parseInt(this.value);
    }).get();

    if (SouvenirItemId.length == 0) {
        swalError("Data is not valid !");
        return false;
    }

    var code = $("input[name='code']").val();
    if (QtySettlements.length == SouvenirItemId.length) {
        swal({
            title: "Submit Settlement ?",
            customClass: "swal-wide",
            showCancelButton: true,
            confirmButtonText: "Yes",
            cancelButtonText: "Cancel",
            confirmButtonColor: "#2196F3",
            cancelButtonColor: "#FF9800",
        }).then(isConfirm => {
            if (isConfirm.value) {
                var data = { "id": SouvenirItemId, "qtySettlement": QtySettlements };
                Settlement(data, code);
            }
        });
    } else {
        swalError("Please fill all columns !");
    }
});

$(document).on("click", ".btn-approve", function () {
    var action = $(this).attr("id");
    var idSouvenir = $(this).attr("data-id");
    var rejectReason = "";
    var code = $("input[name='code']").val();
    if (action == "rejected") {
        swal({
            title: "Reject Reason",
            customClass: "swal-wide",
            input: "textarea",
            inputPlaceholder: "Input Reject Reason",
            showCancelButton: true,
            preConfirm: (reason) => {
                if (reason == "") {
                    return swal.showValidationError(
                        `Reject reason is required`
                    )
                }
            },
        }).then(reason => {
            if (reason.value) {
                var data = { "id": idSouvenir, "reason": reason.value };
                Approval(data, code);
            }
        });
    } else {
        var data = { "id": idSouvenir, "reason": "" };
        Approval(data, code);
    }
});

$(document).on("click", ".btn-close", function () {
    var id = $(this).attr("data-id");
    var code = $("input[name='code']").val();
    swal({
        title: "Close Request ?",
        customClass: "swal-wide",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "Cancel",
        confirmButtonColor: "#2196F3",
        cancelButtonColor: "#FF9800",
    }).then(isConfirm => {
        if (isConfirm.value) {
            var data = { "id": id };
            Close(data, code);
        }
    });
});

function View(data, status) {
    $.ajax({
        type: "GET",
        data: data,
        url: "/SouvenirRequest/SettlementSouvenirRequest/",
        dataType: "html",
        success: function (result) {
            ListSouvenirBySouvenirTransactionId(data.id, status);
            $(".modal-content").html(result);
            $("#modalMenuAccess").modal("show");
        }
    });
}

function Settlement(data, code) {
    $.ajax({
        type: "POST",
        data: data,
        url: "/SouvenirRequest/UpdateSettlementSouvenirRequest/",
        dataType: "html",
        success: function (data) {
            ListSouvenirStock();
            $("#modalMenuAccess").modal("hide");
            var alertTitle = "Data Settlement Saved !";
            var alertText = "Transaction souvenir settlement with code <strong>" + code + "</strong> has been submitted !";
            Notify("info", alertTitle, alertText);
        },
        error: function (data) {
            $("#modalMenuAccess").modal("hide");
            var alertTitle = "Error !";
            var alertText = "Submit transaction souvenir settlement with code <strong>" + code + "</strong> has been failed !";
            Notify("danger", alertTitle, alertText);
        }
    });
}

function Close(data, code) {
    $.ajax({
        type: "POST",
        data: data,
        url: "/SouvenirRequest/CloseSettlementSouvenirRequest/",
        dataType: "html",
        success: function (data) {
            ListSouvenirStock();
            $("#modalMenuAccess").modal("hide");
            var alertTitle = "Data Close Request !";
            var alertText = "Transaction souvenir request with code <strong>" + code + "</strong> has been close request !";
            Notify("info", alertTitle, alertText);
        },
        error: function (data) {
            $("#modalMenuAccess").modal("hide");
            var alertTitle = "Error !";
            var alertText = "Close request transaction souvenir with code <strong>" + code + "</strong> has been failed !";
            Notify("danger", alertTitle, alertText);
        }
    });
}

function Approval(data, code) {
    $.ajax({
        type: "POST",
        data: data,
        url: "/SouvenirRequest/ApprovalSettlementSouvenirRequest/",
        dataType: "html",
        success: function (result) {
            ListSouvenirStock();
            $("#modalMenuAccess").modal("hide");
            if (data.reason == "") {
                var alertTitle = "Data Settlement Approved !";
                var alertText = "Transaction settlement souvenir request with code <strong>" + code + "</strong> has been approved !";
                Notify("info", alertTitle, alertText);
            } else {
                var alertTitle = "Data Settlement Rejected !";
                var alertText = "Transaction settlement souvenir request with code <strong>" + code + "</strong> is rejected by Administrator !";
                Notify("danger", alertTitle, alertText);
            }
        },
        error: function (result) {
            $("#modalMenuAccess").modal("hide");
            var alertTitle = "Error !";
            var alertText = "Approval action settlement souvenir transaction with code <strong>" + code + "</strong> has been failed !";
            Notify("danger", alertTitle, alertText);
        }
    });
}

function ListSouvenirStock(data) {
    $.ajax({
        type: "GET",
        data: data,
        url: "/SouvenirRequest/ListSouvenirStock/",
        dataType: "html",
        success: function (data) {
            $(".tbody").html(data);
        },
    });
};

function ListSouvenirBySouvenirTransactionId(id, status) {
    $.ajax({
        type: "GET",
        data: { "id": id, "status": status},
        url: "/SouvenirRequest/ListSouvenirBySouvenirTransactionId/",
        dataType: "html",
        success: function (data) {
            $(".tbody-souvenir").html(data);
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