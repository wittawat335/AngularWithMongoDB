$(document).ready(function () {
    $.fn.serializeObjectTable = function () {
        var o = {};

        var table = this.DataTable();
        var a = table.$('input, select').serialize();
        var parms = {};
        var items = a.split("&");
        for (var i = 0; i < items.length; i++) {
            var values = items[i].split("=");
            var key = decodeURIComponent(values.shift());
            var value = values.join("=")
            parms[key] = decodeURIComponent(value);
        }
        return (parms);
        return o;
    };
    $.fn.serializeObject = function () {
        var o = {};
        var disabledInput = this.find(':input:disabled').removeAttr('disabled')
        var a = this.find('input[name],select[name],textarea[name]').not('input[type=hidden]').serializeArray();
        //var a = this.find('input[name],select[name],textarea[name],input[type=hidden]').serializeArray();

        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        disabledInput.attr('disabled', 'disabled');
        return o;
    };
    ////Loadding
    $(document).ajaxStart(function () {
        $("#idLoading").fadeIn();
        $("#idLoader").fadeIn();
    }).ajaxStop(function () {
        $("#idLoading").removeAttr("style");
        $("#idLoader").removeAttr("style");
    }).ajaxError(function (e, xhr, settings, exception) {
        if (exception.toString() != '' && exception.toString() != 'abort') { // && exception.toString() != 'abort'
            //  alertMessage('Error ' + exception.toString());
            Swal.fire({
                title: "System error - Do you want error message?",
                icon: 'info',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Yes',
                showCancelButton: true,
                cancelButtonColor: '#d33',
                cancelButtonText: 'No',
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = "/ErrorPage/{0}";
                }
            });
        }
    });

    $(document).ajaxStart(function () {
        $('#loading').show();
    }).ajaxStop(function () {
        $('#loading').hide();
    });
});

function alertMessage(message) {
    if (!message)
        message = '';
    bootbox.alert(message, function () { });
}

function confirmMessage(message) {
    if (!message)
        message = "Confirm";
    bootbox.confirm(message, function (result) { return result; });
}

function modalPOST(caption, controller, action, data, isFull, med) {
    var url = '/' + controller + '/' + action;
    $.post(url, data, function (result) {
        $('#modalDialog > .modal-dialog > .modal-content > .modal-body').html(result);
        showModal(caption, isFull);
    });
}

function closeModal() {
    $('#modalDialog > .modal-dialog > .modal-content > .modal-body').html('');
    $('#modalDialog > .modal-dialog > .modal-content > .modal-header > .modal-title').text('');
    $('#modalDialog').modal('hide');
}

function showModal(caption, isFull, med) {

    $('#modalDialog > .modal-dialog').removeClass('modal-full');
    $('#modalDialog > .modal-dialog').removeClass('modal-20');
    $('#modalDialog > .modal-dialog').removeClass('modal-30');
    $('#modalDialog > .modal-dialog').removeClass('modal-40');
    $('#modalDialog > .modal-dialog').removeClass('modal-50');
    $('#modalDialog > .modal-dialog').removeClass('modal-55');
    $('#modalDialog > .modal-dialog').removeClass('modal-60');
    $('#modalDialog > .modal-dialog').removeClass('modal-60');
    $('#modalDialog > .modal-dialog').removeClass('modal-70');
    $('#modalDialog > .modal-dialog').removeClass('modal-75');
    $('#modalDialog > .modal-dialog').removeClass('modal-80');
    $('#modalDialog > .modal-dialog').removeClass('modal-90');
    $('#modalDialog > .modal-dialog').removeClass('modal-99');

    if (typeof (isFull) === "boolean") {
        if (isFull)
            $('#modalDialog > .modal-dialog').addClass('modal-full');
        else
            $('#modalDialog > .modal-dialog').removeClass('modal-full');
    } else {
        if (typeof (isFull) === "number") {
            var x = isFull;
            switch (true) {
                case (x >= 20 && x < 30):
                    $('#modalDialog > .modal-dialog').addClass('modal-20');
                    break;
                case (x >= 30 && x < 38):
                    $('#modalDialog > .modal-dialog').addClass('modal-36');
                    break;
                case (x >= 30 && x < 40):
                    $('#modalDialog > .modal-dialog').addClass('modal-30');
                    break;
                case (x >= 40 && x < 50):
                    $('#modalDialog > .modal-dialog').addClass('modal-40');
                    break;
                case (x >= 50 && x < 60):
                    $('#modalDialog > .modal-dialog').addClass('modal-50');
                    break;
                case (x >= 55 && x < 60):
                    $('#modalDialog > .modal-dialog').addClass('modal-55');
                    break;
                case (x >= 60 && x < 70):
                    $('#modalDialog > .modal-dialog').addClass('modal-60');
                    break;
                case (x >= 70 && x < 80):
                    $('#modalDialog > .modal-dialog').addClass('modal-70');
                    break;
                case (x >= 70 && x < 76):
                    $('#modalDialog > .modal-dialog').addClass('modal-75');
                    break;
                case (x >= 80 && x < 90):
                    $('#modalDialog > .modal-dialog').addClass('modal-80');
                    break;
                case (x >= 90 && x < 95):
                    $('#modalDialog > .modal-dialog').addClass('modal-90');
                    break;
                case (x >= 95):
                    $('#modalDialog > .modal-dialog').addClass('modal-99');
                    break;
                default:
                    $('#modalDialog > .modal-dialog').addClass('modal-full');
                    break;
            }
        }
    }

    $('#modalDialog > .modal-dialog > .modal-content > .modal-header > .modal-title').text(caption);
    $('#modalDialog').modal('show');
}
//LogOut
function logout() {
    bootbox.confirm('Do you want to logout?',
        function (result) {
            if (result) {
                document.execCommand("ClearAuthenticationCache");
                window.location.href = urlLogout;
            }
        });
}

function SelectRole() {
    var url = urlSelectRole;
    var data = { "role": $('#ddlRole').val() };
    $.post(url, data, function (result) {
        if (result.Result == resultTrue) {
            alertMessage(result.Message);
            clearModal();
            window.location.href = result.url;
        }
        else {
            alertMessage(result.Message);
        }
    });
}

function SwapRole() {
    modalPOST("Swap Role", "Home", "_SwitchRole", null, false);
}

function ClearValueByDiv(div) {
    try {
        $('#' + div + ' input').val("");
        $('#' + div + ' select').val("");
        $('#' + div + ' textarea').val(" ");
        $('#ddlStatus').val('A');
        //  $('#' + div + ' select').select2("val", "");
    } catch (e) {

    }
}
function SetReadOnlyByDiv(div, x) {
    if (x) {
        $('#' + div).find('input[name],select[name],textarea[name]').not('input[type=hidden]').removeAttr('disabled').removeAttr('readonly')
            .attr('disabled', 'disabled').attr('readonly', 'readonly');
    }
    else {
        $('#' + div).find('input[name],select[name],textarea[name]').not('input[type=hidden]').removeAttr('disabled').removeAttr('readonly');
    }
}
function SetReadOnly(div, x) {
    if (!x) {
        $('#' + div).removeAttr('disabled').removeAttr('readonly')
            .attr('disabled', 'disabled').attr('readonly', 'readonly');
    }
    else {
        $('#' + div).removeAttr('disabled').removeAttr('readonly');
    }
}
function SetReqByDiv(div, x) {
    if (x) {
        $('#' + div + ' input,select').removeAttr('required').attr('required', 'required');
    }
    else {
        $('#' + div + ' input,select').removeAttr('required');
    }
}
function SetReq(div, x) {
    if (x) {
        $('#' + div).removeAttr('required').attr('required', 'required');
    }
    else {

        $('#' + div).removeAttr('required');
        $('#' + div).closest('div.form-group').removeClass('has-error has-danger');
    }
}
//function setRole(roleCode) {
//    var url = urlSelectRole;
//    var data = { "role": roleCode };
//    $.post(url, data, function (result) {
//        if (response.status) {
//            alert('1');
//            alert(response.url);
//            swalMessage('success', response.message);
//            window.location.href = response.url;
//        }
//        else {
//            swalMessage('error', response.message);
//        }
//    });
//}
