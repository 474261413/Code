var AiErLan = {
    NewsPage: function (index) {

        $('#loadingContent').show();
        var name = $("#name").val();
        var type = $("#Type").val();
     //   debugger;
        $.post('/admin/news/index', { name: name, type: type, PageIndex: index }, function (data) {
            $('#list').html(data);
        })
    }
}