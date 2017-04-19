
initialize();

function initialize() {

    $('.edit-mode').hide();

    $('.edit-item').on('click', function () {
        $('.edit-mode').hide();
        $('.delete-mode').hide();
        $('.display-mode').show();
        var tr = $(this).parents('tr:first');
        tr.find('.edit-mode, .display-mode').toggle();
    });
    $('.cancel-item').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.display-mode,.edit-mode').toggle();
    });
    $('.delete-item').on('click', function () {
        if (confirm("Are you sure you want delete this book?")) {
            var tr = $(this).parents('tr:first');
            var BookID = $(this).prop('id');
            //Deletes the record with ID sent below
            $.post(
                '/Home/DeleteBook/',
                { BookID: BookID },
                function (item) {
                    if (item.IsDeleted) {
                        tr.remove();
                    }
                }, "json");
        }
    });

    $('.save-item').on('click', function () {
        var tr = $(this).parents('tr:first');
        var BookID = $(this).prop('id');
        var NormQuantity = tr.find('#NormQuantity-Edit').val();
        //sending the posted data to Controller to save chages in DB
        $.post(
            '/Home/SaveBook/',
            { BookID: BookID, NormQuantity: NormQuantity },
            function (item) {
                tr.find('#NormQuantity').text(item.NormQuantity);
                tr.find('#RealQuantity').text(item.RealQuantity);
            }, "json");
        tr.find('.edit-mode, .display-mode').toggle();
    });
}
