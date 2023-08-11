function ClearDataTable(id) {
    $("#" + id).dataTable().fnClearTable();
    ('. select2-container').select2('val', '');
}