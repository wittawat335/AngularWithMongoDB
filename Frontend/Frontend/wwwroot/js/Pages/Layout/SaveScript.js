var formId = '';
var urlAction = '';
function Save(formId, urlAction) {
    alert('3');
    if (formId == '')
        formId = 'frmDetail';
    var msg = FormValidate();
    if (msg != '') {
        swalMessage('warning', msg);
    } else {
        var url = urlAction;
        var data = $('#' + formId).serializeObject();

        $.post(url, data, function (response) {
            console.log(response);
            if (response.isSuccess) {
                swalMessage('success', response.message);
                closeModal();
                GetList();
            }
            else {
                swalMessage('error', response.message);
            }
        });
    }
};

function SaveDataTable(id, url) {
    var url = url;
    var table = $('#' + id).DataTable();
    var data = table.$('input, select').serialize();
    $.post(url, data, function (result) {
        if (result.status) {
            swalMessage('success', result.message);
            BindData('Reset');
        }
    });
};