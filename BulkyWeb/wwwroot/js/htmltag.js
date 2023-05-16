var dataTable;

$(document).ready(function () {
    loadDataTable();
});
  

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/htmlTag/getall'},
        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'aboutMe', "width": "15%" },
            { data: 'age', "width": "15%" },
            { data: 'isPublic', "width": "10%" },
            { data: 'adult', "width": "15%" },
            { data: 'isActive', "width": "10%" },
            { data: 'createDate', "width": "10%" },
          
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/htmlTag/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/htmlTag/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}