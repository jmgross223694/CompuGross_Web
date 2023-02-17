function errorConfirm() {
    var message = $('[id$=hfError]').val()
    if ($('[id$=hfError]').val() != '') {
        Swal.fire({
            position: 'center',
            icon: 'warning',
            title: message,
            showConfirmButton: false
        })
    }
    $('[id$=hfError]').val('')
}

$(function () {
    errorConfirm();
});