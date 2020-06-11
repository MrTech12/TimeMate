var taskIndex = 0;

$(document).ready(function () {
    $("#add-task-field").click(function () {
        ChangeTaskInput()
    });
    $("#task-name-collection").on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
        taskIndex--;
    })
});

function ChangeTaskInput() {
    var newdiv = document.createElement('div');
    newdiv.innerHTML = "Voer een taak in." +
        "<br><input type='text' class='form-control' id='Task_" + taskIndex + "_' name='Task[" + taskIndex + "]'/><a href='#' class='delete'>Veld verwijderen</a><br>";
    document.getElementById("task-name-collection").appendChild(newdiv);
    taskIndex++;
};