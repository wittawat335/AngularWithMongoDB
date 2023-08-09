var formId = '';
var urlAction = '';
function Save(formId, urlAction) {
    if (formId == '')
        formId = 'frmDetail';
    var msg = FormValidate();
    if (msg != '') {
        swalMessage('warning', msg);
    } else {
        var url = urlAction;
        var data = $('#' + formId).serializeObject();

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