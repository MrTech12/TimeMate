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
    let textFields = "<div class='form-group'><label class='control-label'>Voer het uurloon van bijbaan " + jobIndex + " in.</label><input type='text' class='form-control' name='JobHourlyWage[" + jobIndex + "]' oninput='ValidateHourlyWageInput(this.value)' required/></div>";

    if (jobIndex === 0) {
        textFields +=  "<div class='form-group'><label class='control-label'>Is de bijbaan doordeweeks of in het weekend?</label><select class='form-control' name='JobDayType[" + jobIndex + "]' onchange='SetSecondJobDayType()'><option>Doordeweeks</option><option>Weekend</option></select></div>";
    }
    else if (jobIndex === 1) {
        let secondJobType = SetSecondJobDayType();
        textFields +=  "<div class='form-group'><label class='control-label'>Is de bijbaan doordeweeks of in het weekend?</label><input class='form-control' type='text' name='JobDayType[" + jobIndex + "]' value='" + secondJobType + "' readonly></input></div>";
    }
    return textFields;
}

function SetSecondJobDayType() {
    let firstJobDayType = $('[name="JobDayType[0]"] :selected').text();

    if (firstJobDayType === "Doordeweeks" && $('[name="JobDayType[1]"]').length > 0) {
        $('[name="JobDayType[1]"]').val("Weekend");
    }
    else if (firstJobDayType === "Doordeweeks" && $('[name="JobDayType[1]"]').length === 0) {
        return "Weekend";
    }
    else if (firstJobDayType === "Weekend" && $('[name="JobDayType[1]"]').length > 0) {
        $('[name="JobDayType[1]"]').val("Doordeweeks");
    }
    else if (firstJobDayType === "Weekend" && $('[name="JobDayType[1]"]').length === 0) {
        return "Doordeweeks";
    }
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