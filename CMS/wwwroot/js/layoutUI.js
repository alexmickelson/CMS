

function startFrameEdit() {
    $('#frameEditButton').text("stop edditing");
    $('.hFrame').addClass("editFrame");

}
function endFrameEdit() {
    $('#frameEditButton').text("Edit Frames");
    $('.hFrame').endClass("editFrame");

}

//makes an element resizable and ensures that it and its more right sibling share the parents height
function VResizable(JQObject) {
    JQObject = $(JQObject);
    //vertical panels
    var maxWidth = 0;
    JQObject.resizable({
        handles: 'e', minWidth: 0,
        start: function () {
            maxWidth = $(this).outerWidth() + $(this).next().outerWidth();
        },
        resize: function (event, ui) {

            //resizes the vertical panels sibling
            var mySpace = 100 * $(this).outerWidth() / maxWidth;
            var remainingSpace = 100 - mySpace;

            //work in units of percent for responsiveness if parents get resized
            $(this).css('width', mySpace + '%');
            $(this).next().css('width', remainingSpace + '%');


        }
    });
}
//makes an element resizable and ensures that it and its more right sibling share the parents height
function HFrameResizable(JQObject) {
    JQObject = $(JQObject);
    //vertical panels
    JQObject.resizable({
        handles: 's',
        resize: function (event, ui) {
            var emHeight = $(this).height() / parseFloat($("body").css("font-size"));

            //work in units of percent for responsiveness if parents get resized
            $(this).css('height', emHeight + 'em');


        }
    });
}

//makes an element resizable and ensures that it and its lower sibling share the parents height
function HResizable(JQObject) {
    JQObject = $(JQObject);
    var maxHeight = 0;
    JQObject.resizable({
        handles: 's', minHeight: '2',
        start: function () {
            maxHeight = $(this).outerHeight() + $(this).next().outerHeight();
        },
        resize: function () {
            //function resizes the horizontal panels sibling
            var mySpace = 100 * $(this).outerHeight() / maxHeight;
            var remainingSpace = 100 - mySpace;

            //work in units of percent for responsiveness if parents get resized
            $(this).css('height', mySpace + '%');
            $(this).next().css('height', remainingSpace + '%');
        }
    });

}


$(function () {
    //vertical panels
    $('.leftPane').each(function () {
        VResizable(this);
    });
    //horizontal panels
    $('.topPane').each(function () {
        HResizable($(this));
    });
    $('.hFrame').each(function () {
        HFrameResizable($(this));
    });
});

function drop(event) {
    //get horizontal or vertical
    var type = event.dataTransfer.getData("text");
    //add the element and add the resizable function to it
    if (type == "horizontal") {
        event.target.insertAdjacentHTML('afterbegin',
            `<div class="topPane HPane" ondrop="drop(event)" ondragover="allowDrop(event)">
                </div>
                <div class="bottomPane HPane" ondrop="drop(event)" ondragover="allowDrop(event)">
                </div>`);
        HResizable(event.target.firstElementChild);
        //unbind drop so that when things are dropped in the child element, drop() doesnt get called twice
        $(event.target).prop("ondrop", null).off("drop");

    } else if (type == "vertical") {
        event.target.insertAdjacentHTML('afterbegin',
            `<div class="leftPane VPane" ondrop="drop(event)" ondragover="allowDrop(event)">
                </div>
                <div class="rigtPane VPane" ondrop="drop(event)" ondragover="allowDrop(event)">
                </div>`);
        VResizable(event.target.firstElementChild);
        //unbind drop so that when things are dropped in the child element, drop() doesnt get called twice
        $(event.target).prop("ondrop", null).off("drop");
    }

}

function dropStructure(event) {
    //get horizontal or vertical
    var type = event.dataTransfer.getData("text");
    if (type == "hFrame") {
        var newDiv = jQuery('<div/>', {
            class: 'hFrame',
            ondrop: 'drop(event)',
            ondragover: 'allowDrop(event)'
        });
        $('#frames').append(newDiv);
        HFrameResizable(newDiv);
        //remove edit class if it is on
        endFrameEdit();
    }

}


function allowDrop(event) {
    event.preventDefault();
}

function dragStartH(event) {
    event.dataTransfer.setData("Text", "horizontal");
}
function dragStartV(event) {
    event.dataTransfer.setData("Text", "vertical");
}
function dragStartHFrame(event) {
    event.dataTransfer.setData("Text", "hFrame");
}

function editFrames() {
    startFrameEdit();
}