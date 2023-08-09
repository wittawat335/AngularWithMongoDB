var urlDelete = '/Master/Delete';
$(document).ready(function () {
    ClearDataTable("dtResult");
    GetList();
});
function GetList() {
    $.ajax({
        type: "POST",
        url: '/Master/GetList',
        dataType: "json",
        data: {
            masterType: $('#ddlMasterType').val(),
            masterCode: $('#txtCode').val(),
            MasterNameTH: $('#txtNameTH').val(),
            MasterNameEN: $('#txtNameEN').val(),
            masterStatus: $('#ddlStatus').val()
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
                data: 'MASTER_CODE',
                render: function (data, type, row, meta) {
                    return row.masteR_CODE
                }
            },
            {
                data: 'MASTER_TYPE',
                render: function (data, type, row, meta) {
                    return row.masteR_TYPE
                }
            },
            {
                data: 'MASTER_NAME_TH',
                render: function (data, type, row, meta) {
                    return row.masteR_NAME_TH
                }
            },
            {
                data: 'MASTER_NAME_EN',
                render: function (data, type, row, meta) {
                    return row.masteR_NAME_EN
                }
            },
            {
                data: 'MASTER_UPDATE_BY',
                render: function (data, type, row, meta) {
                    return row.masteR_UPDATE_BY
                }
            },
            {
                data: 'MASTER_UPDATE_DATE',
                render: function (data, type, row, meta) {
                    if (row.masteR_UPDATE_DATE === null) return "";
                    return moment(row.masteR_UPDATE_DATE)
                        .format('DD/MM/YYYY');                   
                }
            },
            {
                data: 'MASTER_STATUS',
                render: function (data, type, row, meta) {
                    if (row.masteR_STATUS === 'A') {
                        return '<span class="badge bg-success">'
                            + '' + 'Active' + ''
                            + '</span > '
                    }
                    else if (row.masteR_STATUS === 'I') {
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
                    return '<a class="btn btn-info me-2" onclick=PopupDetail("' + row.masteR_CODE + '","View");>'
                        +'<i class="bi bi-search"></i></a>'  
                        +'<a class="btn btn-warning me-2" onclick=PopupDetail("' + row.masteR_CODE + '","Edit");>'
                        +'<i class="bi bi-pencil-fill"></i></a>'
                        +'<a class="btn btn-danger" onclick=Delete("' + row.masteR_CODE + '","' + row.masteR_NAME_TH + '","' + urlDelete + '");>'
                        +'<i class="bi bi-trash-fill"></a>'
                }
            }      
        ]
    });
}
function PopupDetail(code, action) {
    var data = { "code": code, "action": action };
    modalPOST(action+ " " + "Master", "Master", "_Detail", data, 50);
};





