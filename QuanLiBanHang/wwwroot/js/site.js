$(document).ready(function () {

    $.ajax({
        url: '/Home/PartTables',
        type: 'GET',
        success: function (result) {
            $('#left-layout').html(result);
        }
    });
    var invoiceId = 1;
    $('#table-id').html("Bàn 1");

    $.ajax({
        url: '/Home/billDetail?id=' + invoiceId,
        type: 'GET',
        success: function (result) {
            $('#billDetail').html(result);
            $('#table-id').html("Bàn " + invoiceId);
            $.ajax({
                url: "/Home/getTotalItem",
                data: {
                    tableId: invoiceId,
                },
                method: "POST",
                success: function (result) {
                    $('#cash').val(result.toLocaleString());
                },
            });
        }
    });

    $(document).on('click', '.tap-nav', function () {
        // xóa class active khỏi tất cả các nav 
        $('.tap-nav').removeClass('active');

        // thêm class active vào nav  được click
        $(this).addClass('active');
        var tap = $(this).data('target');
        $.ajax({
            url: '/Home/' + tap,
            type: 'GET',
            success: function (result) {
                $('#left-layout').html(result);
            }
        });
    });

    $(document).on('click', '.table', function () {
        // xóa class active khỏi tất cả các  table
        $('.table').removeClass('active');

        // thêm class active vào table được click
        $(this).addClass('active');

        invoiceId = $(this).data('invoice-id');
        // Lấy đối tượng HTML có id là "table-id"
        var table = document.getElementById("table-id");

        // Thay đổi giá trị của thuộc tính data-table-id thành 2
        table.dataset.tableId = invoiceId;
        $.ajax({
            url: '/Home/billDetail?id=' + invoiceId,
            type: 'GET',
            success: function (result) {
                $('#billDetail').html(result);
                $('#table-id').html("Bàn "+ invoiceId);
                $.ajax({
                    url: "/Home/getTotalItem",
                    data: {
                        tableId: invoiceId,
                    },
                    method: "POST",
                    success: function (result) {
                        $('#cash').val(result.toLocaleString());
                    },
                });
            }
        });
    });

    $(document).on('click', '.logOut-btn', function () {
        $.ajax({
            url: '/Home/SignIn',
            success: function (result) {
                $('#contain').html(result);
            }
        });
    });
   

});
