function uc_Left(str, n) {
    if (n <= 0)
        return '';
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function uc_Right(str, n) {
    if (n <= 0)
        return '';
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}
function NumKey(evt, obj, mode, dec) {
    // 8  backspace
    // 9  tab
    // 46 delete
    // 37 left
    // 39 right
    // 45 minus
    // 46 dot
    var key = evt.which;
    if (key >= 48 && key <= 57 || key == 8 || key == 9 || key == 37 || key == 39) {
        return true;
    }
    else if (key == 45 && (mode != 'inteingerpos' && mode != 'doublepos')) {
        var val = obj.value;
        if (val.length == val.replace('-', '').length) {
            obj.value = '-' + obj.value;
            return false;
        }
        else
            return false;
    }
    else if (key == 46 && (mode != 'integer' && mode != 'integerpos')) {
        var val = obj.value;
        if (val.length == val.replace('.', '').length)
            return true;
        else
            return false;
    }
    else {
        return false;
    }
}
function NumFormat(obj, dec) {
    if (obj.value != '') {
        if (obj.value.indexOf('.') == -1 && dec > 0) obj.value += '.00';
        var num = parseFloat(obj.value.split('.')[0].replace(/,/g, ''));
        var strnum2 = '';
        if (obj.value.indexOf('.') > -1 && dec != 0)
            strnum2 = '.' + uc_Left(obj.value.split('.')[1] + '00000000', dec);
        var n = 0;
        numArr = new String(num).split('').reverse();
        if (num < 0) {
            n = numArr.length - 1;
        }
        else {
            n = numArr.length;
        }
        for (i = 3; i < n; i += 3) {
            numArr[i] += ',';
        }

        obj.value = numArr.reverse().join('') + strnum2;

    }
}
function OnFocus(obj) {
    var val = obj.value.replace(/,/g, '');
    if (val.length > 0) {
        obj.value = val;
        if (obj.createTextRange) {
            var fieldRange = obj.createTextRange();
            fieldRange.moveStart('character', obj.value.length);
            fieldRange.collapse();
            fieldRange.select();
        }
    }
}
function EngNumber() {
    var inputKey = event.keyCode;
    var returnCode = true;
    var result = true;

    if ((inputKey > 47 && inputKey < 58) || (inputKey > 64 && inputKey < 91) || (inputKey > 96 && inputKey < 123)) {
        return;
    }
    else if (inputKey == 13) { }
    else
        result = false;

    if (!result) {
        returnCode = false;
        event.keyCode = 0;
    }
    event.returnValue = returnCode;
}
function setValidateById(id) {
    var result = false;
    if ($('#' + id).val() == '') {
        $('#' + id).css("border-color", "red");
        result = true;
    }

    return result;
}
