var urlDalete = '/Program/Delete';
$(document).ready(function () {
    ClearDataTable("dtResult");
    GetList();
});
function GetList() {
    $.ajax({
        type: "POST",
        url: '/Program/GetList',
        dataType: "json",
        data: {},
        success: OnSuccess
    })
}
function OnSuccess(response) {
    $('#dtResult').DataTable({
        responsive: true,
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
                data: 'PROG_CODE',
                render: function (data, type, row, meta) {
                    return row.proG_CODE
                }
            },
            {
                data: 'PROG_NAME',
                render: function (data, type, row, meta) {
                    return row.proG_NAME
                }
            },
            {
                data: 'PROG_LEVEL',
                render: function (data, type, row, meta) {
                    return row.proG_LEVEL
                }
            },
            {
                data: 'PROG_PARENT_CODE',
                render: function (data, type, row, meta) {
                    return row.proG_PARENT_CODE;
                }
            },
            {
                data: 'PROG_TARGET_URL',
                render: function (data, type, row, meta) {
                    return row.proG_TARGET_URL
                }
            },
            {
                data: 'PROG_ORDER',
                render: function (data, type, row, meta) {
                    return row.proG_ORDER
                }
            },
            {
                data: 'PROG_ICON',
                render: function (data, type, row, meta) {
                    return row.proG_ICON
                }
            },
            {
                data: 'PROG_STATUS',
                render: function (data, type, row, meta) {
                    if (row.proG_STATUS === 'A') {
                        return '<span class="badge bg-success">'
                            + '' + 'Active' + ''
                            + '</span > '
                    }
                    else if (row.proG_STATUS === 'I') {
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
                    return '<a class="btn btn-info me-2" onclick=PopupDetail("' + row.proG_CODE + '","View");>'
                        + '<i class="bi bi-search"></i></a>'
                        + '<a class="btn btn-warning me-2" onclick=PopupDetail("' + row.proG_CODE + '","Edit");>'
                        + '<i class="bi bi-pencil-fill"></i></a>'
                        + '<a class="btn btn-danger" onclick=Delete("' + row.proG_CODE + '","' + row.proG_NAME + '","' + urlDalete +'");>'
                        + '<i class="bi bi-trash-fill"></i></a>'
                }
            }    
        ]
    });
}
function PopupDetail(code, action) {
    var data = { "code": code, "action": action };
    modalPOST(action + " " + "Program", "Program", "_Detail", data, 50);
};