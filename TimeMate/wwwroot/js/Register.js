let jobIndex = 0;

function AddJobFields() {
    if(jobIndex === 2) {
        alert("Je kan alleen twee bijbanen doorgeven.");
    }
    else {
        let newJobElement = $("<div />");
        newJobElement.html(GenerateTextFields());
        $("#job-collection").append(newJobElement);
        jobIndex++;
        ControlAgendaNameFields();
    }
}

function GenerateTextFields() {
    return "<div class='form-group'><label class='control-label'>Voer het uurloon van bijbaan " + jobIndex + " in.</label><input type='text' class='form-control' name='JobHourlyWage[" + jobIndex + "]' oninput='ValidateHourlyWageInput(this.value)' required/></div>" +
        "<div class='form-group'><label class='control-label'>Is de bijbaan doordeweeks of in het weekend?</label><select class='form-control' name='JobDayType[" + jobIndex + "]'><option>Doordeweeks</option><option>Weekend</option></select></div>";
}

function RemoveJobFields() {
    if (jobIndex <= 0) {
        alert("Er zijn geen bijbaanvelden.");
    }
    else {
        $("#job-collection").children().last().remove();
        jobIndex--;
        ControlAgendaNameFields();
    }
}

function ValidateHourlyWageInput(userInput){
    if (!userInput.match(/\d{1,}(\,\d)%?/g)) {
        $("#hourlywage-error").text("Gebruik uurloon nummers met een komma.");
        $("#register-button").prop('disabled', true);
    }
    else {
        $("#hourlywage-error").text("");
        $("#register-button").prop('disabled', false);
    }
}

function ControlAgendaNameFields() {
    if (jobIndex <= 0 && !$("#JobAgendaElement").hasClass("d-none")) {
        $("#JobAgendaElement").addClass("d-none");
    }
    else if (jobIndex > 0 && $("#JobAgendaElement").hasClass("d-none")) {
        $("#JobAgendaElement").removeClass("d-none");
    }
}