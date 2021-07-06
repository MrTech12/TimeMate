let agendaName;
let agendaID;
let selectInput;

$(document).ready(function () {
    $("#confirm-deletion").click(function () {
        if(agendaName === undefined) {
            alert("Er zijn geen agenda's om te verwijderen.");
        }
        else if (agendaName !== undefined) {
            SendDeleteRequest();
        }
    });
});

function GetSelectInfo() {
    agendaName = $("#agenda-select :selected").val();
    agendaID = $("#agenda-select :selected").attr("id");
    $("#agenda-name").text(agendaName);
    selectInput = document.getElementById("agenda-select");
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
        $("#agenda-name").text("");
    }).catch((error => console.error('Cannot remove the agenda.', error)));
}