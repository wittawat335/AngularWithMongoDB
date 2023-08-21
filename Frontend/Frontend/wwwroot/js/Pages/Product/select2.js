$(document).ready(function () {
    setSelect2();
});
function setSelect2() {
    $("#txtProduct").select2({
        placeholder: '---- Please Select ----',
        minimumInputLength: 3,
        dropdownAutoWidth: 'true',
        width: '100%',
        allowClear: true,
        ajax: {
            url: '/Product/select2Product',
            delay: 50,
            dataType: 'json',
            quietMillis: 1000,
            data: function (params) {
                return { query: params.term };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (obj) {
                        return {
                            id: obj.id,
                            text: obj.productName
                        };
                    })
                };
            }
        }
    });
}