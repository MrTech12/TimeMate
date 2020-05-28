var jobAmount;

$(document).ready(function () {
    $("#job-amount").change(function () {
        jobAmount = document.getElementById("job-amount").value;
        ShowJobInputBoxes();
    });
});

function ShowJobInputBoxes() {
    if (jobAmount == 0) {
        if ($("#job1").hasClass("d-none") != true) {
            $("#job1").addClass("d-none");
        }
        else if ($("#job1").hasClass("d-none") == true && $("#job2").hasClass("d-none") != true) {
            $("#job2").addClass("d-none");;
        }
        else if ($("#job1").hasClass("d-none") != true && $("#job2").hasClass("d-none") != true) {
            $("#job1").addClass("d-none");
            $("#job2").addClass("d-none");
        }
    }
    else if (jobAmount == 1) {
        if ($("#job2").hasClass("d-none") != true) {
            $("#job2").addClass("d-none");
        }
        else if ($("#job1").toggleClass("d-none") == true) {
            $("#job1").removeClass("d-none");
        }
    }
    else if (jobAmount == 2) {
        if ($("#job1").toggleClass("d-none") == true && $("#job2").toggleClass("d-none") == true) {
            $("#job1").removeClass("d-none");
            $("#job2").removeClass("d-none");
        }
        else if ($("#job1").toggleClass("d-none") != true && $("#job2").hasClass("d-none") == true) {
            $("#job2").removeClass("d-none");
        }
    }
};