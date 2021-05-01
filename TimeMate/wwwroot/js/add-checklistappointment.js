let taskIndex = 0;

$(document).ready(function () {
    $("#add-task-field").click(function () {
        AddTaskField()
    });
    $("#new-task-collection").on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
        taskIndex--;
    })
});

function AddTaskField() {
    var newTaskElement = document.createElement('div');
    newTaskElement.innerHTML = "Voer een taak in." +
        "<br><input type='text' class='form-control' id='Task_" + taskIndex + "' name='Task[" + taskIndex + "]'/><a href='#' class='delete'>Veld verwijderen</a><br>";
    document.getElementById("new-task-collection").appendChild(newTaskElement);
    taskIndex++;
};