function nullvalues() {
    mission = document.getElementById("mis");
    mission.value = "";
    date = document.getElementById("dat");
    date.value = "";
    hour = document.getElementById("hour");
    hour.value = "";
    minute = document.getElementById("min");
    minute.value = "";
    description = document.getElementById("note");
    description.value = "";
    action = document.getElementById("action");
    action.value = "";
    mission1 = document.getElementById("miss");
    mission1.value = "";
    date1 = document.getElementById("dats");
    date1.value = "";
    description1 = document.getElementById("notes");
    description1.value = "";
}

function deletes(id) {
    console.log(id);
    $.ajax({
        url: '/VolunteeringTimesheet/Delete',
        type: 'POST',
        data: { 'id': id }
        //success: function (res) {
        //    alert("Succesfully Deleted Please Refresh");
        //},
        //error: function (res) {
        //    alert("error");
        //}
    });
    alert("Succesfully Deleted Please Refresh");

}
//function Modalopen(){

//    var modalgoal=document.getElementById("exampleModalADDGoal");
//    var modaltime=document.getElementById("timesheetADD");
//    modaltime.style.display="none";
//    modalgoal.style.display="block";
//}
function edit(id) {
    $.ajax({
        url: '/VolunteeringTimesheet/EditTimesheet',
        type: 'GET',
        data: { 'id': id },
        success: function (res) {
            $("#timesheetADD").html($(res).find("#timesheetADD").html());
        },
        error: function (res) {
            alert("error");
        }
    });
}

function editgoal(id) {
    $.ajax({
        url: '/VolunteeringTimesheet/EditTimesheet',
        type: 'GET',
        data: { 'id': id },
        success: function (res) {
            $("#exampleModalADDGoal").html($(res).find("#exampleModalADDGoal").html());
        },
        error: function (res) {
            alert("error");
        }
    });
}


