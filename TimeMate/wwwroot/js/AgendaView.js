var appointmentData = [];
var jsonData = null;
var popoverData = null;

$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointmentName").click(function () { //Main Entry
        appointmentData.length = 0;
        GetAppointmentInfo(this);

        jsonData = JSON.stringify(appointmentData);
        console.info("From view: " + appointmentData);

        RetrieveAppointmentDetails();
        console.log("From DB: " + popoverData);

        if (popoverData != null) {
            CreatePopover();
        }
    });

    function GetAppointmentInfo(selectedRow) {
        $(selectedRow).closest('tr').find('td').each(function () {
            appointmentData.push($(this).text());
        });
    }

    /*
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
    */

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
                         if (i % 2 == 0 == false) {
                             if (data[i] == "False") {
                                 popoverData += '<img id="taskImage" src="https://img.icons8.com/ios/50/000000/unchecked-checkbox.png"/>';
                                 popoverData += " <br> ";
                             }
                             if (data[i] == "True") {
                                 popoverData += '<img id="taskImage" src="https://img.icons8.com/ios/50/000000/checked-checkbox.png"/>';
                                 popoverData += " <br> ";
                             }
                         }
                         else {
                             popoverData += '<div id="taskName">' + data[i];
                         }
                     };
                 }
                 else {
                     popoverData += data;
                 }
            },
            error: function (ts) {
                onerror(console.info(ts));
            }
        });
    };

    function CreatePopover() {
        $(".appointmentName").popover({
            title: GetPopoverTitle, content: GetPopoverContent, html: true,
        });
    };

    function GetPopoverTitle() {
        return appointmentData[0] + " Details";
    };

    function GetPopoverContent() {
        var popoverContent = popoverContent = "Starttijd: " + appointmentData[1] + "<br>" + "Eindtijd: " + appointmentData[2] + "<br>" + "Agendanaam: " + appointmentData[3] + "<br>";

        if (popoverData != "") {
            popoverContent+= "Details: " + "<br>" + popoverData
        }
        return popoverContent;
    };
});




