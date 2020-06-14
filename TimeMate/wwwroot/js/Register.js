var jobAmount;

$(document).ready(function () {
    $("#job-amount").change(function () {
        jobAmount = $("#job-amount").val();
        ShowJobInputBoxes();
    });
});

function ShowJobInputBoxes() {
    if (jobAmount == 0) {
        if (!$("#job1").hasClass("d-none")) {
            $("#job1").addClass("d-none");
        }
        else if ($("#job1").hasClass("d-none") && !$("#job2").hasClass("d-none")) {
            $("#job2").addClass("d-none");;
        }
        else if (!$("#job1").hasClass("d-none") && !$("#job2").hasClass("d-none")) {
            $("#job1").addClass("d-none");
            $("#job2").addClass("d-none");
        }
    }
    if (jobAmount == 1) {
        if (!$("#job2").hasClass("d-none")) {
            $("#job2").addClass("d-none");
        }
        else if ($("#job1").hasClass("d-none")) {
            $("#job1").removeClass("d-none");
        }
    }
    if (jobAmount == 2) {
        if (!$("#job1").hasClass("d-none") && $("#job2").hasClass("d-none")) {
            $("#job1").removeClass("d-none");
            $("#job2").removeClass("d-none");
        }
        else if (!$("#job1").hasClass("d-none") && $("#job2").hasClass("d-none")) {
            $("#job2").removeClass("d-none");
        }
    }
};