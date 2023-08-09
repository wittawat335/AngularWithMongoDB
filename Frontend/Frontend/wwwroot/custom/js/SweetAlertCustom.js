function swalMessage(icon, message) {
    Swal.fire({
        icon: icon,
        title: message,
        showConfirmButton: false,
        timer: 1000
    });
}
function ConfirmMessage() {
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
            window.location.href = "../Error/Index";
        }
    });
}
function Delete(code, name, url) {
    Swal.fire({
        title: 'Do you want to delete' + ' ' + ' " ' + name + ' " ' + ' ' + '?',
        icon: 'warning',
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Yes',
        showCancelButton: true,
        cancelButtonColor: '#d33',
        cancelButtonText: 'No',
    }).then((result) => {
        if (result.isConfirmed) {
            realDelete(code, url);
        }
    });
}
function realDelete(code, url) {
    var url = url;
    var data = { "code": code };

    $.post(url, data, function (result) {
        if (result.status) {
            swalMessage('success', result.message);
            closeModal();
            GetList();
        }
        else {
            swalMessage('error', result.message);
        }
    });
}