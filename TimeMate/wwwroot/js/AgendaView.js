var appointmentData = [];
var jsonData;
var popoverData = null;

$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointmentName").click(function () { //Main Entry
        GetAppointmentInfo();
        jsonData = JSON.stringify(appointmentData);
        console.info("From view: " + appointmentData);

        if (appointmentData.length != 0) {
            RetrieveAppointmentDetails();
        }

        console.log("From DB: " + popoverData);
        CreatePopover();
    });

    function GetAppointmentInfo() {
        var table = document.getElementById('appointmentTable');
        var cells = table.getElementsByTagName('td');

        for (var i = 0; i < cells.length; i++) {
            // Take each cell
            var cell = cells[i];

            cell.onclick = function () {

                appointmentData.length = 0;

                // Get the row id where the cell exists
                var rowId = this.parentNode.rowIndex;

                var rowSelected = table.getElementsByTagName('tr')[rowId];

                appointmentData.push(rowSelected.cells[0].innerHTML);
                appointmentData.push(rowSelected.cells[1].innerHTML);
                appointmentData.push(rowSelected.cells[2].innerHTML);
                appointmentData.push(rowSelected.cells[3].innerHTML);
            }
        }
    }

    function RetrieveAppointmentDetails() {
        $.ajax({
            type: "get",
            async: "no",
            url: "agenda/retrieveappointmentdetails",
            contenttype: "application/json; charset=utf-8",
            data: { json: jsonData },
            datatype: "text",
            traditional: true,
            success: function (data) {
                popoverData = "";

                 if (Array.isArray(data)) {
                     for (var i = 0; i < data.length; i++) {
                         popoverData += data[i] + "<br>";
                     };
                     //data.foreach(function (entry) {
                     //    console.log(entry);
                     //});
                 }
                 else {
                     popoverData += data;
                 }
            },
            error: function (ts) {
                onerror(checkbox, ts)
            }
        });
    };

    function CreatePopover() {
        $(".appointmentName").popover({
            title: GetPopoverTitle, content: GetPopoverContent, html: true
        });
    };

    function GetPopoverTitle() {
        return appointmentData[0]
    };

    function GetPopoverContent() {
        console.log("pop: " + popoverData)
        return appointmentData[1] + "<br>" + appointmentData[2] + "<br>" + appointmentData[3] + "<br>" + popoverData;
    };
});




