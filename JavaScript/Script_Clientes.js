//const btnAgregarCliente = document.querySelector('.agregar-cliente')
//const btnModificarCliente = document.querySelector('.modificar-cliente')
//const btnLocalidades = document.querySelector('.abm-localidades')
//const sectionBotonesPrincipales = document.querySelector('.stl-section-botones-principales')
//const sectionAgregarCliente = document.querySelector('.stl-section-agregar-cliente')
//const sectionModificarCliente = document.querySelector('.stl-section-modificar-cliente')
//const sectionCamposModificarCliente = document.querySelector('.stl-section-campos-modificar-cliente')
//const sectionLocalidades = document.querySelector('.stl-section-localidades')
//const btnCancelarAgregar = document.querySelector('.stl-btn-cancelar-agregar')
//const btnCancelarModificar = document.querySelector('.stl-btn-cancelar-modificar')
//const btnCancelarLocalidades = document.querySelector('.stl-btn-cancelar-localidades')

//btnAgregarCliente.addEventListener('click', () => {
//    sectionAgregarCliente.classList.toggle('activo')
//    sectionModificarCliente.classList.remove('activo')
//    sectionLocalidades.classList.remove('activo')
//    sectionBotonesPrincipales.classList.remove('activo')
//})
//btnModificarCliente.addEventListener('click', () => {
//    sectionAgregarCliente.classList.remove('activo')
//    sectionModificarCliente.classList.toggle('activo')
//    sectionLocalidades.classList.remove('activo')
//    sectionBotonesPrincipales.classList.remove('activo')
//})
//btnLocalidades.addEventListener('click', () => {
//    sectionAgregarCliente.classList.remove('activo')
//    sectionModificarCliente.classList.remove('activo')
//    sectionLocalidades.classList.toggle('activo')
//    sectionBotonesPrincipales.classList.remove('activo')
//})
//btnCancelarAgregar.addEventListener('click', () => {
//    sectionAgregarCliente.classList.remove('activo')
//    sectionModificarCliente.classList.remove('activo')
//    sectionLocalidades.classList.remove('activo')
//    sectionBotonesPrincipales.classList.add('activo')
//})
//btnCancelarModificar.addEventListener('click', () => {
//    sectionAgregarCliente.classList.remove('activo')
//    sectionModificarCliente.classList.remove('activo')
//    sectionLocalidades.classList.remove('activo')
//    sectionBotonesPrincipales.classList.add('activo')
//})
//btnCancelarLocalidades.addEventListener('click', () => {
//    sectionAgregarCliente.classList.remove('activo')
//    sectionModificarCliente.classList.remove('activo')
//    sectionLocalidades.classList.remove('activo')
//    sectionBotonesPrincipales.classList.add('activo')
//})

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

function soloNumeros(e)
{
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
        return false;
    }

    return true;
}

function soloNumerosNuevoCliente(e) {
    var txtTelefono = $("#MainContent_TxtNuevoClienteTelefono").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key === 45 && txtTelefono != 0)
    {
        return true;
    }
    if (key < 48 || key > 57) {
        return false;
    }
    if (txtTelefono === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloNumerosModificarCliente(e) {
    var txtTelefono = $("#MainContent_TxtModificarClienteTelefono").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key === 45 && txtTelefono != 0) {
        return true;
    }
    if (key < 48 || key > 57) {
        return false;
    }
    if (txtTelefono === 0) {
        if (key === 48) {
            return false;
        }
    }

    return true;
}

function soloLetrasNuevoCliente(e) {
    var txtApenom = $("#MainContent_TxtNuevoClienteApenom").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtApenom === 0) {
        if (key === 209)
        {
            return true;
        }
        else if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key === 241 || key === 209)
    {
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

function soloEspacioLetrasNumerosNuevoCliente(e) {
    var txtDireccion = $("#MainContent_TxtNuevoClienteDireccion").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtDireccion === 0) {
        if (key === 209) {
            return true;
        }
        if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key === 241 || key === 209) {
        return true;
    }
    if (key != 32) {
        if (key < 48 || key > 122) {
            return false;
        }
        if (key > 57 && key < 65) {
            return false;
        }
        if (key > 90 && key < 97) {
            return false;
        }
    }

    return true;
}

function soloLetrasModificarCliente(e) {
    var txtApenom = $("#MainContent_TxtModificarClienteApenom").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtApenom === 0) {
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

function soloEspacioLetrasNumerosModificarCliente(e) {
    var txtDireccion = $("#MainContent_TxtModificarClienteDireccion").val().length;
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (txtDireccion === 0) {
        if (key < 65 || key > 90) {
            return false;
        }
    }
    if (key != 32) {
        if (key < 48 || key > 122) {
            return false;
        }
        if (key > 57 && key < 65) {
            return false;
        }
        if (key > 90 && key < 97) {
            return false;
        }
    }

    return true;
}