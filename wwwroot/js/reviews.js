getReviews = () => {
    var url = '/Reviews/ViewReviews/' + $("#recipe-id").val();
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#all-reviews').html(res);
        }
    })
}

getReviewsSummary = () => {
    var url = '/Recipes/ReviewsSummary/' + $("#recipe-id").val();
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#reviews-summary').html(res);
        }
    })
}

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    getReviews();
                    getReviewsSummary();
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = id => {
    if (confirm('Are you sure to delete this review?')) {
        try {
            $.ajax({
                type: 'POST',
                url: '/Reviews/Delete',
                data: {
                    "id": id,
                },
                success: function (res) {
                    getReviews();
                    getReviewsSummary();
                    alert(res);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

helpful = (url) => {
    try {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (res) {
                console.log(res);
                getReviews();
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
}

$(document).ready(function () {
    getReviewsSummary();
});