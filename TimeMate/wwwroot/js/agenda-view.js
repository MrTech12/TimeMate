var appointmentData = [];

var taskID = [];
var taskName = [];

var descriptionData = null;

$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointment-name").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        CreatePopover();
        console.info("From view: " + appointmentData);
    });

    $(".extra-modal").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        DisplayExtra();
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
});

function GetAppointmentInfo(selectedRow) {
    $(selectedRow).closest('tr').find('td').each(function () {
        appointmentData.push($(this).text());
        appointmentData.push($(this).attr('id'));
    });
};

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
    return popoverContent;
};

function GetAppointmentExtra() {
    return $.ajax({
        type: "get",
        async: "no",
        url: "/Agenda/RetrieveAppointmentExtra",
        contenttype: "application/json; charset=utf-8",
        data: { json: appointmentData[1]},
        datatype: "text",
        traditional: true,
        success: function (data) {
            taskID = [];
            taskName = [];
            descriptionData = "";
            if (Array.isArray(data)) {
                for (var i = 0; i < data.length; i++) {

                    if (i % 2 === 0) {
                        taskID.push(data[i]);
                    }
                    else if (i % 2 === 0 === false) {
                        taskName.push(data[i]);
                    }
                }
            }
            if (data == "") {
                descriptionData = "Er is geen beschrijving en taken voor deze afspraak gevonden.";
            }
            else {
                descriptionData = "Beschrijving: <br>" + data;
            }
        },
        error: function (ts) {
            alert('Een error is ontstaan. Probeer het laten opnieuw a.u.b.');
            onerror(console.info(ts));
        }
    });
};

function DisplayExtra() {
    $.when(GetAppointmentExtra()).done(function () {
        console.info(taskID);
        console.info(taskName);
        console.info("Ajax executed");

        if (taskName[0] == undefined) {
            $("#modal-task-description").empty();
            $("#modal-task-description").html(descriptionData);
            $("#modal-task-description").removeClass("d-none");
        }
        else {
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

            $("#modal-task-info").removeClass("d-none");
            $("#tasks").removeClass("d-none");
            $("#checkoff-task").removeClass("d-none");
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
            alert('Een error is ontstaan. Probeer het laten opnieuw a.u.b.');
            onerror(console.info(ts));
        }
    });
};

function HideModalElements() {
    $("#modal-task-info").addClass("d-none");
    $("#modal-task-description").addClass("d-none");
    $("#tasks").addClass("d-none");
    $("#checkoff-task").addClass("d-none");
};




