var agendaName;
var agendaID;
var selectInput;

$(document).ready(function () {
    $("#delete-agenda").click(function () {
        GetSelectedOptionInformation();
        $("#agenda-name").text(agendaName);
        console.log("Name: " + agendaName + "; Id: " + agendaID);
    });

    $("#confirm-deletion").click(function () {
        if (CheckAgendaName()) {
            SendDeleteRequest();
        }
        else {
            alert("Deze agenda mag u niet verwijderen.");
        }

    });
});

function GetSelectedOptionInformation() {
    selectInput = document.getElementById("agenda-select");
    agendaName = $("#agenda-select :selected").val();
    agendaID = $("#agenda-select :selected").attr("id");
};

function CheckAgendaName() {
    if (agendaName == "Bijbaan") {
        return false;
    }
    else {
        return true;
    }
};

function SendDeleteRequest() {
    $.ajax({
        type: "GET",
        url: "/Agenda/DeleteAgenda",
        contenttype: "application/json; charset=utf-8",
        data: { "agendaID": agendaID },
        datatype: "text",
        traditional: true,
        success: function (data) {
            selectInput.remove(selectInput.selectedIndex);
            alert("Agenda succesvol verwijderd.");
        },
        error: function (ts) {
            alert('Een error is ontstaan. Probeer het laten opnieuw a.u.b.');
            onerror(console.info(ts));
        }
    });
};