var AiErLan = {
    NewsPage: function (index) {

        $('#loadingContent').show();
        var name = $("#name").val();
        var type = $("#Type").val();

        $.post('/admin/news/index', { name: name, type: type, PageIndex: index }, function (data) {
            $('#list').html(data);
        })
    }, News: function (index) {
        var name = $("#name").val();
        var type = $("#Type").val();
        //   debugger;
        $.post('/news/index', { name: name, type: type, PageIndex: index }, function (data) {
            $('#list').html(data);
        })
    }, Product: function (index) {
        var name = $("#name").val();
        var type = $("#Type").val();
        //   debugger;
        $.post('/Product/index', { name: name, type: type, PageIndex: index }, function (data) {
            $('#list').html(data);
        })
    }, Customer: function (index) {
        var name = $("#name").val();
        var type = $("#Type").val();
        //   debugger;
        $.post('/Customer/index', { name: name, type: type, PageIndex: index }, function (data) {
            $('#list').html(data);
        })
    }

}