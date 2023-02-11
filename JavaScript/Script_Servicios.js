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

function validarDescripcion(e) {
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key === 13)
    {
        alert("No se permiten saltos de línea en la descripción");
        return false;
    }
    if (key === 45) {
        alert("No se permiten guiones en la descripción");
        return false;
    }
    if (key === 32 || key === 40 || key === 41 || key === 44 ||
        key === 46 || key === 233 || key === 201 || key === 225 ||
        key === 237 || key === 243 || key === 250 || key === 241 ||
        key === 209 || key === 193 || key === 205 || key === 211 ||
        key === 218)
    {
        return true;
    }
    if (key < 48 || key > 57) //números
    {
        if (key < 65 || key > 90) { //letras mayúsculas
            if (key < 97 || key > 122) { //letras minúsculas
                return false;
            }
        }
    }

    return true;
}

function soloNumerosCostosNuevoServicio1(e) {
    var txtAgregarCostoTerceros = $("#MainContent_TxtAgregarCostoTerceros").val().length;
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
        return false;
    }
    if (txtAgregarCostoTerceros === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosCostosNuevoServicio2(e) {
    var txtAgregarHonorarios = $("#MainContent_TxtAgregarHonorarios").val().length;
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
        return false;
    }
    if (txtAgregarHonorarios === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosCostosNuevoServicio3(e) {
    var txtAgregarCostoRepuestos = $("#MainContent_TxtAgregarCostoRepuestos").val().length;
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
        return false;
    }
    if (txtAgregarCostoRepuestos === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosCostosModificarServicio1(e) {
    var txtListarCostoTerceros = $("#MainContent_TxtListarModificarCostoTerceros").val().length;
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
        return false;
    }
    if (txtListarCostoTerceros === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosCostosModificarServicio2(e) {
    var txtListarHonorarios = $("#MainContent_TxtListarModificarHonorarios").val().length;
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
        return false;
    }
    if (txtListarHonorarios === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosCostosModificarServicio3(e) {
    var txtListarCostoRepuestos = $("#MainContent_TxtListarModificarCostoRepuestos").val().length;
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
        return false;
    }
    if (txtListarCostoRepuestos === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

/*
 enter - 13
 space - 32
     ( - 40
     ) - 41
     , - 44
     . - 46
     é - 233
     É - 201
     á - 225
     í - 237
     ó - 243
     ú - 250
     ñ - 241
     Ñ - 209
     Á - 193
     Í - 205
     Ó - 211
     Ú - 218
*/