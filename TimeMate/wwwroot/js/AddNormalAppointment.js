var index = 0;

$(document).ready(function () {
    $("#bolding-text").click(function () {
        ////var text = window.getSelection();
        //console.log("Version 1: " + document.getElementById("test").selectionStart, document.getElementById("test").selectionEnd)
        //if (document.getSelection()) {
        //    alert("Version 2: " + text);
        //}
        //var text = window.getSelection().toString();
        //var highlight = window.getSelection();
        //console.log("Version 3: " + highlight);
        //var span = '<span class="bold">' + highlight + '</span>';
        //var field = $('#test').html();
        //console.log(field);
        //var newTextField = field.replace(highlight, span);
        //$('#test').html(newTextField);
        //$('#test').html(field.replace(highlight, span));

        highlight = window.getSelection();
        
        if (index === 0) {
            MakeTextBold();
            index++;
        }
        else if (index === 1) {
            MakeTextNormal();
            index--;
        }
    });
});

function MakeTextBold() {
    if (highlight.rangeCount) {
        var e = document.createElement('span');
        e.classList.add("bolding");
        e.innerHTML = highlight.toString();

        var range = highlight.getRangeAt(0);
        range.deleteContents(); 
        range.insertNode(e);
        GetDescriptionInput();
    }
};

function MakeTextNormal() {
    if (highlight.rangeCount) {
        var e = document.createElement('span');
        e.classList.add("normal-text");
        e.innerHTML = highlight.toString();

        var range = highlight.getRangeAt(0);
        range.deleteContents();
        range.insertNode(e);
        GetDescriptionInput();
    }
};

function GetDescriptionInput() {
    var descriptionText = document.getElementById('descriptionBox').innerHTML;
    $("#sendController").val(descriptionText);
};