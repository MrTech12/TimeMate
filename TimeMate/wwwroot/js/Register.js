var jobAmount;

$(document).ready(function () {
    $("#job-amount").change(function () {
        jobAmount = $("#job-amount").val();
        ShowJobInputBoxes();
    });
});

function ShowJobInputBoxes() {
    if (jobAmount == 0) {
        HideAllJobInputs();
    }
    if (jobAmount == 1) {
        ShowOneJobInput();
    }
    if (jobAmount == 2) {
        ShowTwoJobInputs();
    }
};

function HideAllJobInputs() {
    if (!$("#job1").hasClass("d-none")) {
        $("#job1").addClass("d-none");
    }
    else if ($("#job1").hasClass("d-none") && !$("#job2").hasClass("d-none")) {
        $("#job2").addClass("d-none");
    }
    else if (!$("#job1").hasClass("d-none") && !$("#job2").hasClass("d-none")) {
        $("#job1").addClass("d-none");
        $("#job2").addClass("d-none");
    }
}

function ShowOneJobInput() {
    if (!$("#job2").hasClass("d-none")) {
        $("#job2").addClass("d-none");
    }
    else if ($("#job1").hasClass("d-none")) {
        $("#job1").removeClass("d-none");
    }
}

function ShowTwoJobInputs() {
    if (!$("#job1").hasClass("d-none") && $("#job2").hasClass("d-none")) {
        $("#job1").removeClass("d-none");
        $("#job2").removeClass("d-none");
    }
    else if (!$("#job1").hasClass("d-none") && $("#job2").hasClass("d-none")) {
        $("#job2").removeClass("d-none");
    }
}