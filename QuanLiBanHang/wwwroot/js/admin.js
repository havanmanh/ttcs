$(document).ready(function () {

    $.ajax({
        url: '/Home/AD_QLThongKe',
        type: 'GET',
        success: function (result) {
            $('#AD-container').html(result);
        },
    });

    $(document).on('click', '.QL', function () {

        var tap = $(this).data('target');
            
        $.ajax({
            url: '/Home/' + tap,
            type: 'GET',
            success: function (result) {
                $('#AD-container').html(result);
            },
        });
    });

    $(document).on('click', '.view-billDetail', function () {
        // Hiển thị div chứa chi tiết hóa đơn
        var tableId = $(this).data('bill-id');
        document.querySelector('#full').setAttribute("style", "display: block");
        $.ajax({
            url: '/Home/AD_View_BillDetail',
            type: 'GET',
            data: {
                MaHD: tableId,
            },
            success: function (result) {
                $('#bill-out').html(result);
            },
        });
    });

    $(document).on('click', '.dell-bill', function () {
        // Hiển thị div chứa chi tiết hóa đơn
        var tableId = $(this).data('bill-id');
        if (confirm("Bạn có chắc chắn muốn xóa hóa đơn này?")) {
            // Làm gì đó nếu người dùng chọn "OK"
            $.ajax({
                url: '/Home/AD_deleteBill',
                type: 'GET',
                data: {
                    MaHD: tableId,
                },
                success: function () {
                    $.ajax({
                        url: '/Home/AD_QLBill',
                        type: 'GET',
                        success: function (result) {
                            $('#AD-container').html(result);
                        },
                    });
                    alert("Đã xóa hóa đơn");
                },
            });
        } 
    });

    $(document).on('click', '#searchBillBtn', function () {
        var date = $('#date').val();
        console.log(date)
        $.ajax({
            url: '/Home/AD_View_Bill_byDate',
            type: 'GET',
            data: {
                date: date,
            },
            success: function (result) {
                $('#AD-container').html(result);
            },
        });
    });

    $(document).on('click', '#searchMenuBtn', function () {
        var name = $('#name').val();
        $.ajax({
            url: '/Home/AD_getMenu_byName',
            type: 'GET',
            data: {
                name: name,
            },
            success: function (result) {
                $('#AD-container').html(result);
            },
        });
    });

    // ADMIN xóa món
    $(document).on('click', '.dell-menu', function () {
        var itemId = $(this).data('item-id');
        if (confirm("Bạn có chắc chắn muốn xóa món này trong thực đơn?")) {
            // Làm gì đó nếu người dùng chọn "OK"
            $.ajax({
                url: '/Home/AD_deleteMenu',
                type: 'GET',
                data: {
                    MaMon: itemId,
                },
                success: function () {
                    $.ajax({
                        url: '/Home/AD_QLMenu',
                        type: 'GET',
                        success: function (result) {
                            $('#AD-container').html(result);
                        },
                    });
                    alert("Đã xóa món");
                },
            });
        } 
    });

    //ADMIN hiển thị form sửa thông tin món
    $(document).on('click', '.update-menu', function () {
        // Hiển thị div chứa chi tiết hóa đơn
        var MaMon = $(this).data('item-id');
        document.querySelector('#full').setAttribute("style", "display: block");
        $.ajax({
            url: '/Home/AD_showFormExchangeMenu',
            type: 'GET',
            data: {
                MaMon: MaMon,
            },
            success: function (result) {
                $('#full').html(result);
            },
        });
    });


    // bấm sửa thông tin món
    $(document).on('click', '.updateMenuBtn', function () {
        var id = $(this).data('item-id');
        var TenMon = $('#Name').val();
        var Gia = $('#price').val();
        var Loai = $('#type').val();

        if (confirm("Bạn có chắc chắn muốn sửa thông tin món này ?")) {
            $.ajax({
                url: '/Home/AD_updateMenu',
                type: 'GET',
                data: {
                    id: id,
                    TenMon: TenMon,
                    Gia: Gia,
                    Loai: Loai,
                },
                success: function (result) {
                    console.log(id);
                    console.log(TenMon);
                    console.log(Gia);
                    console.log(Loai);
                    $('#AD-container').html(result);
                },
            });
        }
    });

    $(document).on('click', '.cancer', function () {
        document.querySelector('#full').setAttribute("style", "display: none");
    });

    //mở form thêm món
    $(document).on('click', '#insert-menu', function () {
        document.querySelector('#full').setAttribute("style", "display: block");
            $.ajax({
                url: '/Home/AD_insertMenu',
                type: 'GET',
                data: {
                },
                success: function (result) {
                    $('#full').html(result);
                },
            });
    });

    $(document).on('click', '#insertMenuBtn', function () {
        var TenMon = $('#Name').val();
        var Gia = $('#price').val();
        var Loai = $('#type').val();

        if (confirm("Bạn có chắc chắn muốn thêm món này ?")) {
            $.ajax({
                url: '/Home/AD_Action_insertMenu',
                type: 'POST',
                data: {
                    TenMon: TenMon,
                    Gia: Gia,
                    Loai: Loai,
                },
                success: function (result) {
                    $('#AD-container').html(result);
                },
            });
        }
    });

});