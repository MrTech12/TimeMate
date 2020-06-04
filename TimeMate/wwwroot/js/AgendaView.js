var appointmentData = [];

var taskData = [];

$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointmentName").click(function () { //Main Entry
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        CreatePopover();
        console.info("From view: " + appointmentData);
    });

    $(".changeAppointment").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        DisplayTasks();
    });

    function GetAppointmentInfo(selectedRow) {
        $(selectedRow).closest('tr').find('td').each(function () {
            appointmentData.push($(this).text());
            appointmentData.push($(this).attr('id'));
        });
    }

    function GetTasks() {
        return $.ajax({
            type: "get",
            async: "no",
            url: "/Agenda/RetrieveTasks",
            contenttype: "application/json; charset=utf-8",
            data: { json: appointmentData[1]},
            datatype: "text",
            traditional: true,
            success: function (data) {
                taskData = [];
                if (Array.isArray(data)) {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i] === "False") {
                            taskData+= "niet afgevinkt."
                        }
                        else if (data[i] === "True") {
                            taskData += "wel afgevinkt."
                        }
                        else {
                            taskData += data[i];
                        }
                    }
                    if (data.length == 0) {
                    console.log("Got no tasks.");
                    }
                }
            },
            error: function (ts) {
                onerror(console.info(ts));
            }
        });
    };

    function DisplayTasks() {
        $.when(GetTasks()).done(function () {
            console.info(taskData);
            console.info("Ajax executed");

        });
    };

    function CheckOffTask() {

    };


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




