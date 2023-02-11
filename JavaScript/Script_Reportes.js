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