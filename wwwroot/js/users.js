toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": 300,
    "hideDuration": 100,
    "timeOut": 5000,
    "extendedTimeOut": 1000,
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(document).ready(function () {
    // initialize datatable
    $('#index-table').dataTable({
        destroy: true,
        autoWidth: false,
        scrollX: true,
        paging: 20,
        lengthChange: false,
        fixedHeader: true,
        order: [[2, 'asc']],
        columnDefs: [
            { orderable: false, targets: [0, 1] },
            { searchable: false, targets: [0, 1] }
        ],
        dom:
            "<'row mb-3'<'col-sm-12 col-md-6 d-flex align-items-center justify-content-start'f><'col-sm-12 col-md-6 d-flex align-items-center justify-content-end'lB>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"
    });
});

deleteUser = (url) => {
    if (confirm('Are you sure to remove this user?')) {
        try {
            $.ajax({
                type: 'POST',
                url: url,
                success: function (res) {
                    console.log(res);
                    toastr["success"](res, "Done");
                    setTimeout(function () { location.reload(); }, 2000);
                },
                error: function (err) {
                    console.log(err);
                    toastr["error"](res, "Error");
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}
