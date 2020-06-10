var index = 0;

$(document).ready(function () {
    $("#bolding-text").click(function () {

        highlight = window.getSelection();
        
        if (index === 0) {
            MakeTextBold();
            index++;
        }
        else if (index === 1) {
            MakeTextNormal();
            index--;
        }
        GetDescriptionInput();
    });

    $("#save-description").click(function () {
        GetDescriptionInput();
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
    }
};

function GetDescriptionInput() {
    var descriptionText = document.getElementById('description-box').innerHTML;
    $("#description-controller").val(descriptionText);
};