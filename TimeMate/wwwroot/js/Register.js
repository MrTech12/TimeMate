var jobAmount;

$(document).ready(function () {
    $("#job-amount").change(function () {
        jobAmount = document.getElementById("job-amount").value;
        ShowJobInputBoxes();
    });
});

function ShowJobInputBoxes() {
    if (jobAmount == 0) {
        if ($("#job1-wage").hasClass("d-none") != true) {
            $("#job1-wage").toggleClass("d-none");
            $("#job1-dayType").toggleClass("d-none");
        }
        else if ($("#job1-wage").hasClass("d-none") == true && $("#job2-wage").hasClass("d-none") != true) {
            $("#job2-wage").toggleClass("d-none");
            $("#job2-dayType").toggleClass("d-none");
        }
        else if ($("#job1-wage").hasClass("d-none") != true && $("#job2-wage").hasClass("d-none") != true) {
            $("#job1-wage").toggleClass("d-none");
            $("#job1-dayType").toggleClass("d-none");
            $("#job2-wage").toggleClass("d-none");
            $("#job2-dayType").toggleClass("d-none");
        }
    }
    else if (jobAmount == 1) {
        if ($("#job2-wage").hasClass("d-none") != true) {
            $("#job2-wage").toggleClass("d-none");
            $("#job2-dayType").toggleClass("d-none");
        }
        else if ($("#job1-wage").toggleClass("d-none") == true){
            $("#job1-wage").toggleClass("d-none");
            $("#job1-dayType").toggleClass("d-none");
        }
    }
    else if (jobAmount == 2) {

        if ($("#job1-wage").toggleClass("d-none") == true) {
            $("#job1-wage").toggleClass("d-none");
            $("#job1-dayType").toggleClass("d-none");
            $("#job2-wage").toggleClass("d-none");
            $("#job2-dayType").toggleClass("d-none");
        }

        else if ($("#job1-wage").toggleClass("d-none") != true && $("#job2-wage").hasClass("d-none") == true) {
            $("#job2-wage").toggleClass("d-none");
            $("#job2-dayType").toggleClass("d-none");
        }
    }
};