var urlDelete = '/User/Delete';
$(document).ready(function () {
    ClearDataTable("dtResult");
    GetList();
});
function GetList() {
    $.ajax({
        type: "POST",
        url: '/User/GetList',
        dataType: "json",
        data: {
            firstName: $('#txtFirstName').val(),
            lastName: $('#txtLastName').val(),
            loginId: $('#txtLogin').val(),
            status: $('#ddlStatus').val()
        },
        success: OnSuccess
    })
}

function OnSuccess(response) {
    $('#dtResult').DataTable({
        bDestroy: true,
        bProcessing: true,
        bLenghtChange: true,
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        bfilter: true,
        bSort: true,
        bPaginate: true,
        data: response.value,
        columns: [
            {
                data: 'USER_LOGIN',
                render: function (data, type, row, meta) {
                    return row.useR_LOGIN
                }
            },
            {
                data: 'USER_FIRST_NAME',
                render: function (data, type, row, meta) {
                    return row.useR_FIRST_NAME
                }
            },
            {
                data: 'USER_LAST_NAME',
                render: function (data, type, row, meta) {
                    return row.useR_LAST_NAME
                }
            },
            {
                data: 'USER_UPDATE_BY',
                render: function (data, type, row, meta) {
                    return row.useR_UPDATE_BY
                }
            },
            {
                data: 'USER_UPDATE_DATE',
                render: function (data, type, row, meta) {
                    if (row.useR_UPDATE_DATE === null) return "";
                    return moment(row.useR_UPDATE_DATE)
                        .format('DD/MM/YYYY');
                }
            },
            {
                data: 'USER_STATUS',
                render: function (data, type, row, meta) {
                    if (row.useR_STATUS === 'A') {
                        return '<span class="badge bg-success">'
                            + '' + 'Active' + ''
                            + '</span > '
                    }
                    else if (row.useR_STATUS === 'I') {
                        return '<span class="badge bg-dark">'
                            + '' + 'InActive' + ''
                            + '</span > '
                    }
                    else {
                        return "";
                    }
                }
            },
            {
                data: 'null',
                render: function (data, type, row, meta) {
                    return '<a class="btn btn-info me-2" onclick=PopupDetail("' + row.useR_LOGIN + '","View");>'
                        + '<i class="bi bi-search"></i></a>'
                        + '<a class="btn btn-warning me-2" onclick=PopupDetail("' + row.useR_LOGIN + '","Edit");>'
                        + '<i class="bi bi-pencil-fill"></i></a>'
                        + '<a class="btn btn-danger" onclick=ComfirmDelete("' + row.useR_LOGIN + '","' + row.useR_FIRST_NAME + '","' + urlDelete + '");>'
                        + '<i class="bi bi-trash-fill"></a>'
                }
            }
        ]
    });
}

function PopupDetail(code, action) {
    var data = { "code": code, "action": action };
    modalPOST(action + " " + "User", "User", "_Detail", data, 50);
};

