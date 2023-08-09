$(document).ready(function () {
    ClearDataTable('dtResult');
    GetList();
});
function GetList() {
    $.ajax({
        type: "POST",
        url: '/ReceiveCar/GetList',
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
                data: 'null',
                render: function (data, type, row, meta) {
                    return '<a class="btn btn-info"'
                        + 'onclick=PopupTracking("' + row.jobB_ID + '","'+row.joB_CONTRACT_NO+'","Edit");>'
                        + '<i class="bi bi-search"></i></a>'
                }
            },
            {
                data: 'JOB_ID',
                render: function (data, type, row, meta) {
                    return row.joB_ID
                }
            },
            {
                data: 'JOB_CONTRACT_NO',
                render: function (data, type, row, meta) {
                    return row.joB_CONTRACT_NO
                }
            },
            {
                data: 'JOB_CARD_NO',
                render: function (data, type, row, meta) {
                    return row.joB_CARD_NO
                }
            },
            {
                data: 'JOB_CUST_NAME',
                render: function (data, type, row, meta) {
                    return row.joB_CUST_NAME;
                }
            },
            {
                data: 'null',
                render: function (data, type, row, meta) {
                    if (row.useR_FIRST_NAME === null && row.useR_LAST_NAME === null) {
                        return '';
                    }
                    return row.useR_FIRST_NAME + ' ' + row.useR_LAST_NAME
                }
            },
            {
                data: 'OA_NAME_TH',
                render: function (data, type, row, meta) {
                    return row.oA_NAME_TH
                }
            },
            {
                data: 'JOB_ASSIGN_OA_DATE',
                render: function (data, type, row, meta) {
                    return row.joB_ASSIGN_OA_DATE
                }
            },
            {
                data: 'STS_NAME',
                render: function (data, type, row, meta) {
                    if (row.stS_CODE === 'RE01') {
                        return '<span class="badge bg-primary">'
                            + '' + row.stS_NAME_TH + ''
                            + '</span > '
                    }
                    else if (row.stS_CODE === 'RE02') {
                        return '<span class="badge bg-warning text-dark">'
                            + '' + row.stS_NAME_TH + ''
                            + '</span > '
                    }
                    else if (row.stS_CODE === 'RE03') {
                        return '<span class="badge bg-success">'
                            + '' + row.stS_NAME_TH + ''
                            + '</span > '
                    }
                    else if (row.stS_CODE === 'RE04') {
                        return '<span class="badge bg-dark">'
                            + '' + row.stS_NAME_TH + ''
                            + '</span > '
                    }
                    else {
                        return '';
                    }
                }
            },
            {
                data: 'JOB_STATUS',
                render: function (data, type, row, meta) {
                    if (row.joB_STATUS === 'A') {
                        return '<span class="badge bg-success">'
                            + '' + 'Active' + ''
                            + '</span > '
                    }
                    else if (row.joB_STATUS === 'I') {
                        return '<span class="badge bg-dark">'
                            + '' + 'InActive' + ''
                            + '</span > '
                    }
                    else {
                        return "";
                    }
                }
            }
        ]
    });
}
function PopupTracking(code, contractNo, action) {
    var data = { "code": code, "contractNo": contractNo, "action": action };
    modalPOST(action + " " + "Tracking", "ReceiveCar", "_popUpTracking", data, 50);
};
function PopupUpload() {
    var data = { };
    modalPOST("Import" + " " + "File", "ReceiveCar", "_popUpUpload", data, 80);
};