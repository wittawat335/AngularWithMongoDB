function setSelect2() {
    $("#ddlRefNo").select2({
        placeholder: '---- Please Select ----',
        minimumInputLength: 3,
        dropdownAutoWidth: 'true',
        width: '100%',
        allowClear: true,
        ajax: {
            url: '/TodoList/searchRefNo',
            delay: 50,
            dataType: 'json',
            quietMillis: 1000,
            data: function (params) {
                return { query: params.term };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (obj) {
                        console.log(obj);
                        return {
                            id: obj.joB_ID,
                            text: obj.joB_ID
                                + ' : '
                                + obj.joB_CASE_NAME
                        };
                    })
                };
            }
        }
    });
    $("#ddlCustCode").select2({
        placeholder: '---- Please Select ----',
        minimumInputLength: 3,
        dropdownAutoWidth: 'true',
        width: '100%',
        allowClear: true,
        ajax: {
            url: '/TodoList/searchCustomer',
            delay: 50,
            dataType: 'json',
            quietMillis: 1000,
            data: function (params) {
                return { query: params.term };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (obj) {
                        console.log(obj);
                        return {
                            id: obj.cusT_CODE,
                            text: obj.cusT_CODE
                                + ' : '
                                + obj.cusT_NAME
                        };
                    })
                };
            }
        }
    });
    $("#ddlAdmin").select2({
        placeholder: '---- Please Select ----',
        minimumInputLength: 3,
        dropdownAutoWidth: 'true',
        width: '100%',
        allowClear: true,
        ajax: {
            url: '/TodoList/searchAdmin',
            delay: 50,
            dataType: 'json',
            quietMillis: 1000,
            data: function (params) {
                return { query: params.term };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (obj) {
                        console.log(obj);
                        return {
                            id: obj.joB_ID,
                            text: obj.joB_ID
                                + ' : '
                                + obj.joB_CASE_NAME
                        };
                    })
                };
            }
        }
    });
    $("#ddlOa").select2({
        placeholder: '---- Please Select ----',
        minimumInputLength: 3,
        dropdownAutoWidth: 'true',
        width: '100%',
        allowClear: true,
        ajax: {
            url: '/TodoList/searchOa',
            delay: 50,
            dataType: 'json',
            quietMillis: 1000,
            data: function (params) {
                return { query: params.term };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (obj) {
                        console.log(obj);
                        return {
                            id: obj.oA_CODE,
                            text: obj.oA_CODE
                                + ' : '
                                + obj.oA_NAME_TH
                        };
                    })
                };
            }
        }
    });
}