﻿var agendaName;
var agendaID;
var selectInput;

$(document).ready(function () {
    $("#delete-agenda").click(function () {
        GetSelectedOptionInformation();
        $("#agenda-name").text(agendaName);
        console.log("Name: " + agendaName + "; Id: " + agendaID);
    });

    $("#confirm-deletion").click(function () {
        SendDeleteRequest();
    });
});

function GetSelectedOptionInformation() {
    selectInput = document.getElementById("agenda-select");
    agendaName = selectInput.options[selectInput.selectedIndex].value;
    agendaID = selectInput.options[selectInput.selectedIndex].id;
};

function SendDeleteRequest() {
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