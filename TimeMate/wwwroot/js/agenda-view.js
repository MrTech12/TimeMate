let appointmentData = [];
let descriptionData = null;
let taskIDs = [];
let taskNames = [];

$(document).ready(function() {
    $('[data-toggle="popover"]').popover();

    $(".appointment-name").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        CreatePopover();
    });

    $(".extra-modal").click(function () {
        appointmentData.length = 0;
        GetAppointmentInfo(this);
        DisplayExtra();
    });

    $(window).keydown(function (event) {
        if (event.keyCode == 27) { // Pressing escape
            HideModalElements();
        }
    });
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
}

function GetPopoverTitle() {
    return appointmentData[0] + " gegevens";
}

function GetPopoverContent() {
    let popoverContent = "Starttijd: " + appointmentData[2] + "<br>" + "Eindtijd: " + appointmentData[4]
        + "<br>" + "Agendanaam: " + appointmentData[6] + "<br>";
    return popoverContent;
}

async function GetAppointmentExtra() {
    return await fetch(`AgendaView/RetrieveAppointmentExtra/${appointmentData[1]}`, {
        method: 'GET',
        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
    })
    .then(response => response.json())
    .then(data => {
        taskIDs = [];
        taskNames = [];
        descriptionData = "";
        if (Array.isArray(data)) {
            for (var i = 0; i < data.length; i++) {
                taskIDs.push(data[i].taskID);
                taskNames.push(data[i].taskName);
            }
        }
        if (data.length == 0) 
            {descriptionData = "Er is/zijn geen beschrijving of taken voor deze afspraak gevonden."}
        else
            {descriptionData = "<b>Afspraakbeschrijving: </b> <br>" + data}
    }).catch((error => console.error('Cannot retrieve the appointment extra.', error)));
}

function DisplayExtra() {
    $.when(GetAppointmentExtra()).done(function () {

        if (taskNames[0] == undefined) {
            $("#modal-task-description").empty();
            $("#modal-task-description").html(descriptionData);
            $("#modal-task-description").removeClass("d-none");
        }
        else {
            console.info(taskIDs);
            console.info(taskNames);
            var selectElement = document.getElementById("tasks");
            $("#tasks").empty();
            for (var i = 0; i < taskNames.length; i++) {
                let optionElement = document.createElement("option");
                optionElement.id = taskIDs[i];
                optionElement.textContent = taskNames[i];
                selectElement.appendChild(optionElement);
            }

            $("#modal-task-info").removeClass("d-none");
            $("#tasks").removeClass("d-none");
            $("#checkoff-task").removeClass("d-none");
        }
    });
}

async function ChangeTaskStatus() {
    let selectInput = document.getElementById("tasks");
    let taskID = (selectInput.options[selectInput.selectedIndex].id);

    await fetch(`/ChecklistAppointment/ChangeTaskStatus/${taskID}`, {
        method: 'PATCH',
        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
    })
    .then(response => { window.location.href = "/AgendaView"})
    .catch((error => console.error('Cannot change the status of the task.', error)));
}

function HideModalElements() {
    $("#modal-task-info").addClass("d-none");
    $("#modal-task-description").addClass("d-none");
    $("#tasks").addClass("d-none");
    $("#checkoff-task").addClass("d-none");
}