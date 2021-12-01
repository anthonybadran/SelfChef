deleteRecipe = (url) => {
    if (confirm('Are you sure to delete this recipe?')) {
        try {
            $.ajax({
                type: 'POST',
                url: url,
                success: function (res) {
                    console.log(res);
                    location.reload();
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