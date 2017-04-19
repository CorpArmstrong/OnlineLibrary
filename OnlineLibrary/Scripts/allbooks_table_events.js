
initialize();

function initialize() {

    $('.take-item').on('click', function () {
        if (confirm("Are you sure you want to take this book?")) {
            var tr = $(this).parents('tr:first');
            var SelectedRow = tr.index();
            var BookID = $(this).prop('id');
            var ReaderId = $_GET('readerId');
            var Name = $(this).prop('Title');
            console.log("Title: " + Title);

            $.post(
                '/Home/TakeBook/',
                { BookID: BookID, ReaderId: ReaderId, Name: Name },
                function (item) {
                    var fp = tr.find('#takeSpan');
                    tr.find('#takeSpan').get(0).style.display = 'none';
                }, "json")
                .error(function () {
                    alert("error");
                });
        }
    });
}