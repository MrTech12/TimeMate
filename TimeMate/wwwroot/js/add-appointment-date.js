function ValidateDates() {
    let startDateValueRaw = $("#AppointmentViewModel_StartDate").val();
    let endDateValueRaw = $("#AppointmentViewModel_EndDate").val();
    let startDate = new Date(startDateValueRaw);
    let endDate = new Date(endDateValueRaw);

    if (startDate > endDate) {
        $("#date-error").text("Incorrecte datum(s)");
        $("#submit-appointment-btn").prop('disabled', true);
    }
    else {
        $("#date-error").text("");
        $("#submit-appointment-btn").prop('disabled', false);
    }
}