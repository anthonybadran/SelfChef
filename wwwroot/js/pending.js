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

approveRecipe = (url) => {
    if (confirm('Are you sure you want to approve this recipe?')) {
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
                    toastr["error"](err, "Error");
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}

rejectRecipe = (url) => {
    if (confirm('Are you sure you want to reject this recipe?')) {
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
                    toastr["error"](err, "Error");
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}