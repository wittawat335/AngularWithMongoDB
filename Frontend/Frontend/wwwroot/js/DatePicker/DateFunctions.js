function AddSubtractMonths(date, numMonths) { // function set MaxDate
    var month = date.getMonth();
    var milliSeconds = new Date(date).setMonth(month + numMonths);
    return new Date(milliSeconds);
}
