var appointmentData = [];

$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointmentName").click(function () { //Main Entry
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        CreatePopover();
        console.info("From view: " + appointmentData);
    });

    function GetAppointmentInfo(selectedRow) {
        $(selectedRow).closest('tr').find('td').each(function () {
            appointmentData.push($(this).text());
        });
    }

    function CreatePopover() {
        $(".appointmentName").popover({
            title: GetPopoverTitle, content: GetPopoverContent, html: true,
        });
    };

    function GetPopoverTitle() {
        return appointmentData[0] + " gegevens";
    };

    function GetPopoverContent() {
        var popoverContent = "Starttijd: " + appointmentData[1] + "<br>" + "Eindtijd: " + appointmentData[2]
            + "<br>" + "Agendanaam: " + appointmentData[3] + "<br>";

        if (appointmentData[8] != "") {
            popoverContent += "Details: " + "<br>" + appointmentData[4];
        }
        return popoverContent;
    };
});




