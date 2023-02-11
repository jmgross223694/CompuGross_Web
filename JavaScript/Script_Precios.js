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

function validacionPrecioPrincipal(e) {
    var key;
    var txtDolares = $("#MainContent_TxtPrecioDolar").val().length;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtDolares === 0) {
        if (key === 44 && key === 46) {
            return false;
        }
    }
    if (key < 48 || key > 57) {
        if (key != 44 && key != 46) {
            return false;
        }
    }

    return true;
}

function validacionPrecioAgregar(e) {
    var key;
    var txtDolares = $("#MainContent_TxtAgregarPrecio").val().length;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtDolares === 0) {
        if (key === 44 && key === 46) {
            return false;
        }
    }
    if (key < 48 || key > 57) {
        if (key != 44 && key != 46) {
            return false;
        }
    }

    return true;
}

function validacionPrecioModificar(e) {
    var key;
    var txtDolares = $("#MainContent_TxtModificarPrecio").val().length;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtDolares === 0) {
        if (key === 44 && key === 46) {
            return false;
        }
    }
    if (key < 48 || key > 57) {
        if (key != 44 && key != 46) {
            return false;
        }
    }

    return true;
}