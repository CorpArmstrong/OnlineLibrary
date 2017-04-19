init();

function init() {

    $('.return-item').on('click', function () {
        var tr = $(this).parents('tr:first');
        var BookID = $(this).prop('id');
        var ReaderId = $_GET('readerId');
        
        $.post(
            '/Home/ReturnBook/',
            { BookID: BookID, ReaderId: ReaderId, Name : Name },
            function (item) {
                var convertedDate = eval(item.ReturnDate.slice(1, -1));
                tr.find('#ReturnDate').text(convertedDate);
                tr.find('#ReturnAction').hide();

            }, "json");
    });
}