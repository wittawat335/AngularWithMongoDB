$(document).ready(function () {
    setSelect2();
    GetList();
    setDatePicker();
    ClearDataTable('dtResult');
});
function GetList() {
    $.ajax({
        type: "POST",
        url: '/TodoList/GetList',
        dataType: "json",
        data: {
            refNo: $('#ddlRefNo').val(),
            contractNo: $('#ddlCustCode').val(),
            adminCode: $('#ddlAdmin').val(),
            oaCode: $('#ddlOa').val(),
            adminDateStart: $('#dpAssignDateFrom').val(),
            adminDateEnd: $('#dpAssignDateTo').val(),
            caseCode: $('#ddlLegalCode').val(),
            succStatus: $('#ddlStatus').val()
        },
        success: OnSuccess
    })
}
function OnSuccess(response) {
    console.log(response.value);
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
                data: 'null',
                render: function (data, type, row, meta) {
                    if (row.joB_DOC_FLAG == "0" && row.joB_STATUS == "A") {
                        return row.joB_ID
                    }
                    else {
                        if (row.joB_STATUS == "I") {
                            return '<span class="badge bg-warning text-dark">'
                                + '<i class="ri-car-fill"></i>' 
                                + ' ' + row.joB_CASE_NAME + ''
                                + '</span > '
                        }
                        else if (row.joB_CASE_CODE == "LC011") {
                            return '<span class="badge bg-warning text-dark">'
                                + '<i class="ri-car-fill"></i>'
                                + ' ' + row.joB_CASE_NAME + ''
                                + '</span > '
                        }
                        else {
                            return '<span class="badge bg-warning text-dark">'
                                + '<i class="ri-car-fill"></i>'
                                + ' ' + row.joB_CASE_NAME + ''
                                + '</span > '
                        }
                    }
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
                data: 'JOB_CUST_NAME',
                render: function (data, type, row, meta) {
                    return row.joB_CUST_NAME
                }
            },
            {
                data: 'JOB_CASE_CODE',
                render: function (data, type, row, meta) {
                    return row.joB_CASE_CODE;
                }
            },
            {
                data: 'JOB_BRAND',
                render: function (data, type, row, meta) {
                    return row.joB_BRAND
                }
            },
            {
                data: 'JOB_MODEL',
                render: function (data, type, row, meta) {
                    return row.joB_MODEL
                }
            },
            {
                data: 'JOB_PLATE_NO',
                render: function (data, type, row, meta) {
                    return row.joB_PLATE_NO
                }
            },
            {
                data: 'JOB_OS_AMT_COL',
                render: function (data, type, row, meta) {
                    if (data === null) return "";
                    return commaSeparateNumber(row.joB_OS_AMT_COL);
                }
            },
            {
                data: 'JOB_OVD_DAY_COL',
                render: function (data, type, row, meta) {
                    return row.joB_OVD_DAY_COL
                }
            },
            {
                data: 'JOB_ASSIGN_ADMIN_DATE',
                render: function (data, type, row, meta) {
                    if (data === null) return "";
                    return moment(row.joB_ASSIGN_ADMIN_DATE)
                        .format('DD/MM/YYYY');
                }
            },
            {
                data: 'JOB_HANDLE_DAY',
                render: function (data, type, row, meta) {
                    if (row.joB_CASE_STATUS_COLOR != null) {
                        return '<td style="text-align: center">' 
                            +'<span class="badge">' +row.joB_HANDLE_DAY+'' 
                            +'</span></td>'
                    }
                    else 
                        return row.joB_HANDLE_DAY                   
                }
            },
            {
                data: 'JOB_REPO_STATUS_NAME',
                render: function (data, type, row, meta) {
                    if (row.joB_REPO_STATUS === 'RE01') {
                        return '<span class="badge bg-danger">'
                            + '' + row.joB_REPO_STATUS_NAME + ''
                            + '</span > '
                    }
                    else if (row.joB_REPO_STATUS === 'RE02') {
                        return '<span class="badge bg-primary">'
                            + '' + row.joB_REPO_STATUS_NAME + ''
                            + '</span > '
                    } 
                    else if (row.joB_REPO_STATUS === 'RE03') {
                        return '<span class="badge bg-success">'
                            + '' + row.joB_REPO_STATUS_NAME + ''
                            + '</span > '
                    }  
                    else if (row.joB_REPO_STATUS === 'RE04') {
                        return '<span class="badge bg-dark">'
                            + '' + row.joB_REPO_STATUS_NAME + ''
                            + '</span > '
                    }  
                }
            }
        ]
    });
}
function commaSeparateNumber(val) {
    while (/(\d+)(\d{3})/.test(val.toString())) {
        val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
    }

    return val;
}
