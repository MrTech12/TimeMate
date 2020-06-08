var index = 0;
var appointmentInfo = [];
var jsonData = 0;

$(document).ready(function () {
    $("#bolding-text").click(function () {

        highlight = window.getSelection();
        
        if (index === 0) {
            MakeTextBold();
            index++;
        }
        else if (index === 1) {
            MakeTextNormal();
            index--;
        }
    });
    $("#add-appointment").click(function () {
        GetEnteredInformation();
        jsonData = JSON.stringify(appointmentInfo);
        console.info(jsonData);
        SendAddRequest();
    });

});

function MakeTextBold() {
    if (highlight.rangeCount) {
        var e = document.createElement('span');
        e.classList.add("bolding");
        e.innerHTML = highlight.toString();

        var range = highlight.getRangeAt(0);
        range.deleteContents(); 
        range.insertNode(e);
    }
};

function MakeTextNormal() {
    if (highlight.rangeCount) {
        var e = document.createElement('span');
        e.classList.add("normal-text");
        e.innerHTML = highlight.toString();

        var range = highlight.getRangeAt(0);
        range.deleteContents();
        range.insertNode(e);
    }
};


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
    appointmentInfo.push(document.getElementById('description-box').innerHTML);
}

function SendAddRequest() {
    $.ajax({
        type: "POST",
        async: "no",
        url: "NormalAppointment/Index",
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