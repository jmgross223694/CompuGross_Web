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

function soloLetrasAgregarApellido(e) {
    var txtApellido = $("#MainContent_TxtAgregarApellido").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtApellido === 0) {
        if (key === 209) {
            return true;
        }
        else if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key === 241 || key === 209) {
        return true;
    }
    if (key != 32) {
        if (key < 65 || key > 122) {
            return false;
        }
        if (key > 90 && key < 97) {
            return false;
        }
    }

    return true;
}

function soloLetrasAgregarNombre(e) {
    var txtNombre = $("#MainContent_TxtAgregarNombre").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtNombre === 0) {
        if (key === 209) {
            return true;
        }
        else if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key === 241 || key === 209) {
        return true;
    }
    if (key != 32) {
        if (key < 65 || key > 122) {
            return false;
        }
        if (key > 90 && key < 97) {
            return false;
        }
    }

    return true;
}

function soloLetrasModificarApellido(e) {
    var txtApellido = $("#MainContent_TxtModificarApellido").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtApellido === 0) {
        if (key === 209) {
            return true;
        }
        else if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key === 241 || key === 209) {
        return true;
    }
    if (key != 32) {
        if (key < 65 || key > 122) {
            return false;
        }
        if (key > 90 && key < 97) {
            return false;
        }
    }

    return true;
}

function soloLetrasModificarNombre(e) {
    var txtNombre = $("#MainContent_TxtModificarNombre").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtNombre === 0) {
        if (key === 209) {
            return true;
        }
        else if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key === 241 || key === 209) {
        return true;
    }
    if (key != 32) {
        if (key < 65 || key > 122) {
            return false;
        }
        if (key > 90 && key < 97) {
            return false;
        }
    }

    return true;
}

function soloNumerosAgregarUsername(e) {
    var txtUsername = $("#MainContent_TxtAgregarUsername").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key === 45 && txtUsername != 0) {
        return true;
    }
    if (key < 48 || key > 57) {
        return false;
    }
    if (txtUsername === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosModificarUsername(e) {
    var txtUsername = $("#MainContent_TxtModificarUsername").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key === 45 && txtUsername != 0) {
        return true;
    }
    if (key < 48 || key > 57) {
        return false;
    }
    if (txtUsername === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}