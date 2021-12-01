getFavorite = () => {
    var url = '/Favorites/AddToFavorite/' + $("#recipe-id").val();
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#favorite').html(res);
        }
    })
}

favoritesList = () => {
    var url = '/Favorites/FavoritesList?user=' + $("#user-id").val();
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#favorites-list").html(res);
        }
    })
}

addToFavorites = (url) => {
    try {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (res) {
                console.log(res);
                getFavorite();
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
}

manageFavorites = (url) => {
    if (confirm('Are you sure to remove this recipe?')) {
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