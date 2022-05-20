$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');
    PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        event.stopImmediatePropagation();
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
})

function close_modal(){
    var PlaceHolderElement = $('#PlaceHolderHere');
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        PlaceHolderElement.html(data);
        PlaceHolderElement.find('modal').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        PlaceHolderElement.empty();
    })
}

function process_sign_up() {

    var activityId = document.getElementById("ActivityID").value;
    var name = document.getElementById("Name").value;
    var email = document.getElementById("Email").value;
    var phoneNumber = document.getElementById("PhoneNumber").value;
    var current = document.getElementById("CurrentParticipants").value;
    var max = document.getElementById("MaxParticipants").value;
    let remaining = max - current;
    console.log(remaining);

    let list = [ activityId, name, email, phoneNumber ];

    if (name == "" || name.length <= 1) {
        window.alert("Must enter a name");
        return;
    }

    if (email == "" || email.length <= 16) {
        window.alert("Must enter a valid email");
        return;
    }

    var phoneno = new RegExp(/^\d{10}$/);
    if (!phoneNumber.match(phoneno)) {
        window.alert("Must enter a valid phone number");
        return;
    }

    $.post("/Home/processSignUp", { id: activityId, name: name, email: email, phone: phoneNumber, remainingParticipants: remaining }), function (data) {
        alert(data);
        window.alert("Error");
    }

    close_modal();
    window.alert("You have successfully signed up!");
}

function edit_event(ActivityId) {
    $.post("/Admin/EditEvent", { id: ActivityId }), function (data) {
        alert(data);
        window.alert("Error");
    }
    window.location = 'Admin/EditEvent/' + ActivityId;
}

function delete_event(ActivityId) {
    if (confirm("Are you sure you want to delete this event?") == true) {
        
        $.post("/Admin/DeleteEvent", { id: ActivityId }), function (data) {
            alert(data);
            window.alert("Error");
        }
        window.alert("Successfully deleted the event");
    }
}

