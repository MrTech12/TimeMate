﻿var appointmentInfo = [];
var jsonData = 0;

$(document).ready(function () {
    $("#add-appointment").click(function () {
        if (!CheckEmptyAppointmentName()) {
            alert("U heeft de afspraaknaam niet ingevuld. Dit is verplicht.");
            return false;
        }
        GetEnteredInformation();
        jsonData = JSON.stringify(appointmentInfo);
        console.info(jsonData);
        SendAddRequest();
    });
});

function CheckEmptyAppointmentName() {
    var form = $("#appointment-form");

    form.validate({
        rules: {
            "appointment-name-input": {
                required: true,
            }
        }
    });

    return form.valid();
};

function GetEnteredInformation() {
    appointmentInfo = [];
    appointmentInfo.push($("#AppointmentViewModel_AppointmentName").val());
    appointmentInfo.push($("#AppointmentViewModel_StartDate").val());
    appointmentInfo.push($("#AppointmentViewModel_StartTime").val());
    appointmentInfo.push($("#AppointmentViewModel_EndDate").val());
    appointmentInfo.push($("#AppointmentViewModel_EndTime").val());

    var selectInput = document.getElementById("AppointmentViewModel_AgendaDTO_0__AgendaName");
    appointmentInfo.push(selectInput.options[selectInput.selectedIndex].value);
    appointmentInfo.push(selectInput.options[selectInput.selectedIndex].id);

    for (var i = 0; i < 4; i++) {
        appointmentInfo.push($("#Task_" + i + "_").val());
    }
}

function SendAddRequest() {
    $.ajax({
        type: "POST",
        async: "no",
        url: "ChecklistAppointment/Index",
        contenttype: "application/json; charset=utf-8",
        data: { json: jsonData },
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
}