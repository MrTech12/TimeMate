var agendaName;
var agendaID;
var selectInput;

$(document).ready(function () {
    $("#delete-agenda").click(function () {
        GetSelectionInfo();
        $("#agenda-name").text(agendaName);
    });

    $("#confirm-deletion").click(function () {
        SendDeleteRequest();
    });
});

function GetSelectionInfo() {
    selectInput = document.getElementById("agenda-select");
    agendaName = $("#agenda-select :selected").val();
    agendaID = $("#agenda-select :selected").attr("id");
}

async function SendDeleteRequest() {
    await fetch('/Agenda/DeleteAgenda', {
        method: 'DELETE',
        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
        body: JSON.stringify({"AgendaID": parseInt(agendaID)})
    })
    .then(response => {
        selectInput.remove(selectInput.selectedIndex);
        alert("Agenda succesvol verwijderd.");
    }).catch((error => console.error('Cannot remove the agenda.', error)));
}