









$(document).ready(function () {
    

    var minDate, maxDate;

    // Custom filtering function which will search data in column four between two values
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var min = minDate.val();
            var max = maxDate.val();
            var date = new Date(data[3]);

            if (
                (min == null && max == null) ||
                (min == null && date <= max) ||
                (min <= date && max == null) ||
                (min <= date && date <= max)
            ) {
                return true;
            }
            return false;
        }
    );









    // Create date inputs
    minDate = new DateTime($('#min'), {
        format: 'MMMM Do YYYY'
    });
    maxDate = new DateTime($('#max'), {
        format: 'MMMM Do YYYY'
    });

    // DataTables initialisation
    var table = $('#example').DataTable();

    // Refilter the table
    $('#min, #max').on('change', function () {
        table.draw();
    });

    
    
    

    $('#myTable').DataTable({

        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true,
        "order": [3, 'desc'],
        "autoWidth": true
        





    });
});