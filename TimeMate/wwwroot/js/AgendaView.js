$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

function GetDetails() {
    var table = document.getElementById('appointmentTable');
    var cells = table.getElementsByTagName('td');

    for (var i = 0; i < cells.length; i++) {
        // Take each cell
        var cell = cells[i];

        // do something on onclick event for cell
        cell.onclick = function () {

            var appointmentData = [];
            appointmentData.length = 0;

            // Get the row id where the cell exists
            var rowId = this.parentNode.rowIndex;

            var rowSelected = table.getElementsByTagName('tr')[rowId];
            //rowSelected.style.backgroundColor = "yellow";
            //rowSelected.className += " selected";

            appointmentData.push(rowSelected.cells[0].innerHTML);
            appointmentData.push(rowSelected.cells[1].innerHTML);
            appointmentData.push(rowSelected.cells[2].innerHTML);
            appointmentData.push(rowSelected.cells[3].innerHTML);

            msg = 'The ID of the company is: ' + rowSelected.cells[0].innerHTML;
            msg += '\nThe cell value is: ' + this.innerHTML;

            //alert(appointmentData);

            var jsonData = JSON.stringify(appointmentData);

            $.ajax({
                type: "GET",
                url: "Agenda/RetrieveAppointmentDetails",
                contentType: "application/json; charset=utf-8",
                data: { json: jsonData },
                datatype: "text",
                traditional: true,
                success: function (data) {
                    $(".appointmentName").popover({
                        title: rowSelected.cells[0].innerHTML + " details",
                        content: rowSelected.cells[1].innerHTML + "<br>" + rowSelected.cells[2].innerHTML + "<br>" + rowSelected.cells[3].innerHTML + "<br>" + data,
                        html: true
                    });
                },
                error: function (ts) {
                    onError(checkbox, ts)
                }
            });
        }
    }
}