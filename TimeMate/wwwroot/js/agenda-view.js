var appointmentData = [];

let taskID = [];
let taskName = [];


$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointment-name").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        CreatePopover();
        console.info("From view: " + appointmentData);
    });

    $(".task-modal").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        DisplayTasks();
    });

    $("#checkoff-task").click(function () {
        CheckOffTask();
    });

    $(".close").click(function () {
        HideModalElements();
    });

    $(window).keydown(function (event) {
        if (event.keyCode == 27) {
            HideModalElements();
        }
    });

    function GetAppointmentInfo(selectedRow) {
        $(selectedRow).closest('tr').find('td').each(function () {
            appointmentData.push($(this).text());
            appointmentData.push($(this).attr('id'));
        });
    }

    function CreatePopover() {
        $(".appointment-name").popover({
            title: GetPopoverTitle, content: GetPopoverContent, html: true,
        });
    };

    function GetPopoverTitle() {
        return appointmentData[0] + " gegevens";
    };

    function GetPopoverContent() {
        var popoverContent = "Starttijd: " + appointmentData[2] + "<br>" + "Eindtijd: " + appointmentData[4]
            + "<br>" + "Agendanaam: " + appointmentData[6] + "<br>";

        if (appointmentData[8] != "") {
            popoverContent += "Details: " + "<br>" + appointmentData[8];
        }
        return popoverContent;
    };

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
                if (Array.isArray(data)) {
                    taskID = [];
                    taskName = [];
                    for (var i = 0; i < data.length; i++) {

                        if (i % 2 === 0) {
                            taskID.push(data[i]);
                        }
                        else if (i % 2 === 0 === false) {
                            taskName.push(data[i]);
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
            console.info(taskID);
            console.info(taskName);
            console.info("Ajax executed");

            if (taskName[0] == undefined) {
                $("#modal-no-tasks").removeClass("d-none");
            }

            else {
                $("#modal-task-info").removeClass("d-none");
                $("#tasks").removeClass("d-none");
                $("#checkoff-task").removeClass("d-none");

                var selectElement = document.getElementById("tasks");
                $("#tasks").empty();
                for (var i = 0; i < taskName.length; i++) {
                    var option = taskName[i];
                    var el = document.createElement("option");
                    el.textContent = option;
                    el.value = option;
                    el.id = taskID[i];
                    selectElement.appendChild(el);
                }
            }
        });
    };

    function CheckOffTask() {
        var selectInput = document.getElementById("tasks");
        var taskID = (selectInput.options[selectInput.selectedIndex].id);

        $.ajax({
            type: "get",
            async: "no",
            url: "/Agenda/ChangeTaskStatus",
            contenttype: "application/json; charset=utf-8",
            data: { json: taskID },
            datatype: "text",
            traditional: true,
            success: function (data) {
                window.location.href = "/Agenda/Index";
            },
            error: function (ts) {
                onerror(console.info(ts));
            }
        });
    };

    function HideModalElements() {
        $("#modal-task-info").addClass("d-none");
        $("#modal-no-tasks").addClass("d-none");
        $("#tasks").addClass("d-none");
        $("#checkoff-task").addClass("d-none");
    };
});




