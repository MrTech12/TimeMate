let taskIndex = 0;

function AddTaskField() {
    let newTaskElement = $("<div />");
    newTaskElement.html(GenerateTextField());
    $("#task-collection").append(newTaskElement);
    taskIndex++;
}

function GenerateTextField() {
    return "<label class='control-label'>Taak "+ taskIndex + "</label><input type='text' class='form-control' id='Task_" + taskIndex + "' name='Task[" + taskIndex + "]'/>";
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