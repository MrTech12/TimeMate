let taskIndex = 0;

function AddTaskField() {
    let newTaskElement = $("<div />");
    newTaskElement.html(GenerateTextField());
    $("#task-collection").append(newTaskElement);
    taskIndex++;
}

function GenerateTextField() {
    return "<label class='control-label'>Voer de naam van de taak in.</label><input type='text' class='form-control' name='Task[" + taskIndex + "]' required/>";
}

function RemoveTaskField() {
    if(taskIndex <= 0) {
        alert("Er zijn geen taakvelden.");
    }
    else {
        $("#task-collection").children().last().remove();
        taskIndex--;
    }
}