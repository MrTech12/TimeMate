var agendaName;
var agendaID;
var selectInput;
var newAgendaName;

$(document).ready(function () {
    $("#deleteAgenda").click(function () {
        GetSelectedInformation();
        $("#agendaName").text(agendaName);
        console.log("Name: " + agendaName + "; Id: " + agendaID);
    });

    $("#confirmDeletion").click(function () {
        SendDeletionRequest();
    });

    $("#renameAgenda").click(function () {
        GetSelectedInformation();
        $("#agendaName").text(agendaName);
        console.log("Name: " + agendaName + "; Id: " + agendaID);
    });

    $("#confirmRenaming").click(function () {
        SendRenameRequest();
    });

});

function GetSelectedInformation() {
    selectInput = document.getElementById("agendaList");
    agendaName = selectInput.options[selectInput.selectedIndex].value;
    agendaID = selectInput.options[selectInput.selectedIndex].id;
};

function SendDeletionRequest() {
    $.ajax({
        type: "POST",
        url: "/Agenda/DeleteAgenda",
        contenttype: "application/json; charset=utf-8",
        data: { json: agendaID },
        datatype: "text",
        traditional: true,
        success: function (data) {
            selectInput.remove(selectInput.selectedIndex);
        },
        error: function (ts) {
            onerror(console.info(ts));
        }
    });
};

function SendRenameRequest() {
    $.ajax({
        type: "POST",
        url: "/Agenda/DeleteAgenda",
        contenttype: "application/json; charset=utf-8",
        data: { json: agendaID },
        datatype: "text",
        traditional: true,
        success: function (data) {
            selectInput.remove(selectInput.selectedIndex);
        },
        error: function (ts) {
            onerror(console.info(ts));
        }
    });
};