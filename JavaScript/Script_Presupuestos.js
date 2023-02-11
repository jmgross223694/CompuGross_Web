function alertaConfirm() {
    var message = $('[id$=hfMessage]').val()

    if ($('[id$=hfMessage]').val() != '') {
        Swal.fire({
            allowOutsideClick: false,
            allowEscapeKey: false,
            allowEnterKey: false,
            title: message,
            icon: 'success',
        })
    }
    $('[id$=hfMessage]').val('')
}

function errorConfirm() {
    var message = $('[id$=hfError]').val()
    if ($('[id$=hfError]').val() != '') {
        Swal.fire({
            position: 'center',
            icon: 'warning',
            title: message,
            showConfirmButton: false,
            timer: 2000
        })
    }
    $('[id$=hfError]').val('')
}

$(function () {
    errorConfirm();
    alertaConfirm();
});

function validacionCantidad_y_Precio(e) {
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key < 48 || key > 57)
    {
        if (key != 46)
        {
            return false;
        }
    }

    return true;
}

function validacionCodigo(e) {
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key < 48 || key > 57) {
        if (key < 65 || key > 90) {
            if (key < 97 || key > 122) {
                if (key != 95) {
                    return false;
                }
            }
        }
    }

    return true;
}

function validacionCliente(e) {
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key < 48 || key > 57) {
        if (key < 65 || key > 90) {
            if (key < 97 || key > 122) {
                if (key != 32) {
                    return false;
                }
            }
        }
    }

    return true;
}