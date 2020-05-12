$(document).ready(function () {
$("#bold-text").click(function () {
    //var text = window.getSelection();
    console.log("test");
    console.log(document.getElementById("test").selectionStart, document.getElementById("test").selectionEnd)
    if (document.getSelection()) {
        alert(text);
    }
    var text = window.getSelection().toString();
    console.log(text);
    });
});