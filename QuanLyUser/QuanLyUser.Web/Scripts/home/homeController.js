var homeconfig = {
    pageSize: 3,
    pageIndex: 1,
}
var homeController = {
    init: function () {
        homeController.loadData(true);
        homeController.registerEvent();
    },
    registerEvent: function () {
        $('#frmSaveData').validate({
            rules: {
                txtName: {
                    required: true,
                    minlength: 2
                },
                txtEmail: {
                    required: true,
                    email: true                   
                },
                txtTel: {
                    required: true,
                    maxlength: 14
                },
                txtUserName: {
                    required: true,
                    minlength: 2
                },
                txtPassword: {
                    required: true,
                    minlength: 5
                }
            },
            messages: {
                txtName: {
                    required: "Vui lòng nhập tên.",
                    minlength: "Tên của bạn phải gồm ít nhất 2 ký tự."
                },
                txtEmail: {
                    required: "Vui lòng nhập Email.",
                    email: "Vui lòng nhập địa chỉ email hợp lệ."
                },
                txtTel: {
                    required: "Vui lòng nhập số điện thoại.",
                    maxlength: "Vui lòng nhập không quá 14 ký tự."
                },
                txtUserName: {
                    required: "Vui lòng nhập tên tài khoản.",
                    minlength: "Tên của bạn phải gồm ít nhất 2 ký tự."
                },
                txtPassword: {
                    required: "Vui lòng nhập mật khẩu.",
                    minlength: "Tên của bạn phải gồm ít nhất 5 ký tự."
                }
            }
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            homeController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#frmSaveData').valid()) {
                homeController.saveData();
            }
        });
        $('.btnEdit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            homeController.loadDetail(id);
        });
        $('.btnDelete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.dialog({
                message: "Bạn có chắc chắn xóa người dùng này không?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success',
                        callback: function () {
                            homeController.deleteUser(id);
                            homeController.loadData(true);
                        }
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                }
            });
        });
        $('#btnSearch').off('click').on('click', function () {
            homeController.loadData(true);
        });
        $('#txtKeyword').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                homeController.loadData(true);
            }
        });
        $(function () {
            $(".datepicker").datepicker();
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Home/GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#hidID').val(data.ID);
                    $('#txtName').val(data.Name);
                    $('#txtEmail').val(data.Email);
                    $('#txtTel').val(data.Phone);
                    $('#txtUserName').val(data.UserName);
                    $('#txtPassword').val(data.Password);
                    $('#ckStatus').prop('checked', data.Status);
                }
                else {
                    bootbox.alert("Tải không thành công!");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    saveData: function () {
        var name = $('#txtName').val();
        var email = $('#txtEmail').val();
        var tel = $('#txtTel').val();
        var user = $('#txtUserName').val();
        var password = $('#txtPassword').val();
        var status = $('#ckStatus').prop('checked');
        var id = parseInt($('#hidID').val());
        var employee = {
            Name: name,
            Email: email,
            Phone: tel,
            Status: status,
            UserName: user,
            Password: password,
            ID: id
        }
        $.ajax({
            url: '/Home/SaveData',
            data: {
                strEmployee: JSON.stringify(employee)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Lưu thành công", function () {
                        $('#modalAddUpdate').modal('hide');
                        homeController.loadData(true);
                    });
                }
                else {
                    bootbox.alert("Lưu không thành công!");
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    },

    resetForm: function () {
        $('#hidID').val('0');
        $('#txtName').val('');
        $('#txtEmail').val('');
        $('#txtTel').val('');
        $('#txtUserName').val('');
        $('#txtPassword').val('');
        $('#ckStatus').prop('checked', true);
    },
    deleteUser: function (id) {
        $.ajax({
            url: '/Home/Delete',
            type: 'POST',
            data: {
                id: id
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    bootbox.alert("Xóa thành công", function () {
                        homeController.loadData(true);
                    });
                    homeController.loadData(true)
                }
                else {
                    bootbox.alert("Xóa không thành công!");
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },

    loadData: function (changePageSize) {
        var keyword = $('#txtKeyword').val();
        var fromDate = $('#fromDate').val();
        var toDate = $('#toDate').val();
        $.ajax({
            url: '/Home/LoadData',
            type: 'GET',
            data: {
                fromDate: fromDate,
                toDate: toDate,
                keyword: keyword,
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    if (data.length == 0) {
                        bootbox.alert("Không tìm thấy người dùng thích hợp!");
                    }
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Email: item.Email,
                            UserName: item.UserName,
                            Phone: item.Phone,
                            CreatedDate: homeController.formatDatetime(item.CreatedDate),
                            Status: item.Status == true ? "<span class=\"label label-success\">Actived</span>" : "<span class=\"label label-danger\">Locked</span>"
                        });
                    });
                    $('#tblData').html(html);
                    homeController.paging(response.total, function () {
                        homeController.loadData();
                    }, changePageSize);
                    homeController.registerEvent();
                }
            }
        })
    },
    //phân trang
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                homeconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },

    formatDatetime: function (jsonDt) {
        var MIN_DATE = -62135578800000; // const

        var date = new Date(parseInt(jsonDt.substr(6, jsonDt.length - 8)));
        return date.toString() == new Date(MIN_DATE).toString() ? "" : date.getDate() + "/" + (date.getMonth() + 1) + "//" + date.getFullYear(); 
    }
}
homeController.init();