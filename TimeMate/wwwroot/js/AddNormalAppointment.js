var index = 0;

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
        GetDataForTransfer();
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
        GetDescriptionInput();
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
        GetDescriptionInput();
    }
};

function GetDescriptionInput() {
    var descriptionText = document.getElementById('descriptionBox').innerHTML;
    $("#sendController").val(descriptionText);
};

function GetDataForTransfer()
{
    var appointmentInfo = [];
    appointmentInfo.push($("#AppointmentViewModel_Name").val());
    appointmentInfo.push($("#AppointmentViewModel_StartDate").val());
    appointmentInfo.push($("#AppointmentViewModel_StartTime").val());
    appointmentInfo.push($("#AppointmentViewModel_EndDate").val());
    appointmentInfo.push($("#AppointmentViewModel_EndTime").val());

    var selectInput = document.getElementById("AppointmentViewModel_AgendaDTO_0__AgendaName");
    appointmentInfo.push(selectInput.options[selectInput.selectedIndex].value);
    appointmentInfo.push(selectInput.options[selectInput.selectedIndex].id);
    appointmentInfo.push(document.getElementById('descriptionBox').innerHTML);
    console.info(appointmentInfo);
    //Get data from input fields.
    //Get selected value option from select box.
    //Get id from select value option.
    //Get description.
    //Write AJAX method to send data to controller. Data gets send via array.
    //Controller puts data in AppointmentDTO.
    //Data gets saved into DB.
    //AJAX send user to Agenda Page.
    //**Do the same for adding checklist appointment.**
}