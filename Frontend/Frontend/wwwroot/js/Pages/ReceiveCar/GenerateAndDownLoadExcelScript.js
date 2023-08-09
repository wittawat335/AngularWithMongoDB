var objLists = [],

    objList = null,

    _headers = [];
$(document).ready(function () {
    Init();
});

function Init() {
    objList = NewObj();
    $("#btnDownloadExcel").click(function () {
        GenerateAndDownloadExcel();
    });

    $("#btnSave").click(function () {
        Save();
    });

    $("#input").on("change", function (e) {
        var file = e.target.files[0];
        if (!file) return;
        var FR = new FileReader();
        FR.onload = function (e) {

            var data = new Uint8Array(e.target.result);
            var workbook = XLSX.read(data, { type: 'array' });
            var firstSheet = workbook.Sheets[workbook.SheetNames[0]];
            var result = XLSX.utils.sheet_to_json(
                firstSheet, { header: 1 });

            GenerateTable(result);
        };

        FR.readAsArrayBuffer(file);
        $("#input").val("");
    });
}

function GenerateAndDownloadExcel() {
    var StudentId = 0,
        name = "";
    $.ajax({
        url: "../ReceiveCar/GenerateAndDownloadExcel",
        //data: { 'Id': groupId },
        type: 'GET',

        success: function (result) {
            ExcelFromBase64("Excel.xlsx", result);
        },
        error: function (result) { }
    });
}

function Reset() {
    objLists = [];
    objList = null;
    _headers = [];
    $("#tblMain thead tr,#tblMain tbody tr").remove();
}

function GenerateTable(exportStatus) {
    Reset();
    if (exportStatus.length > 0) {
        var sTemp = "";
        var headers = exportStatus[0];
        /*  console.log(headers);*/
        sTemp = "<tr>";
        sTemp += "<th style='text-align:center;vertical-align:middle;'>No</th>";

        $.map(headers, function (header) {
            _headers.push(header);
            sTemp += "<th style='text-align:center;vertical-align:middle;min-width:100px;'>" + header + "</th>";
        });
        sTemp += "</tr>";
        $("#tblMain thead").append(sTemp);
        exportStatus = exportStatus.slice(1);
        exportStatus = exportStatus != null ?
            exportStatus.filter(x => x.length > 0) : exportStatus;

        var nSL = 0;

        for (var i = 0; i < exportStatus.length; i++) {
            nSL++;
            objList = NewObj();
            sTemp = "<tr>";
            sTemp += "<td style='text-align:center;vertical-align:middle;'>" + nSL + "</td>";
            var valueIndex = 0;
            var es = exportStatus[i];

            for (var j = 0; j < _headers.length; j++) {
                propValue = es[j];
                propValue =
                    typeof (propValue) == "undefined" ? "" : propValue;

                var propName = _headers[valueIndex];
                objList[propName] = propValue;



                sTemp += "<td style='text-align:center;vertical-align:middle;' title='" + _headers[valueIndex] + "'>" + propValue + "</td>";

                valueIndex++;

            }

            sTemp += "</tr>";

            $("#tblMain tbody").append(sTemp);
            objLists.push(objList);
        }
    }
}

function ExcelFromBase64(fileName, bytesBase64) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function Save() {
    if (objLists.length > 0) {
        var ajaxRequest = $.ajax({
            url: "../ReceiveCar/SaveFile/",
            type: "POST",
            data: { model: objLists },
            dataType: "json",
            beforeSend: function () { },
        });
        ajaxRequest.done(function (result) {
            if (result.status) {
                swalMessage('success', result.message);
                closeModal();
            }
        });
        ajaxRequest.fail(function (jqXHR, textStatus) { alert("Error Found"); alerts('error title', 'error info', 'error'); });
    }
    else {
        alert("No Data Found.");
    }
}

function NewObj() {
    var obj = {
        ContractNo: "",
        TrackingBy: "",
        TrackingDate: "",
        Remark: ""
    };

    return obj;

}

