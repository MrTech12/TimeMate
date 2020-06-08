var appointmentInfo = [];
var jsonData = 0;

$(document).ready(function () {
    $("#add-appointment").click(function () {
        GetEnteredInformation();
        jsonData = JSON.stringify(appointmentInfo);
        console.info(jsonData);
        SendDeletionRequest();
    });
});

function GetEnteredInformation() {
    appointmentInfo = [];
    appointmentInfo.push($("#AppointmentViewModel_Name").val());
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

function SendDeletionRequest() {
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
            onerror(console.info(ts));
        }
    });
}