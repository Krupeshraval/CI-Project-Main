﻿document.getElementById('imgdiv').addEventListener("click", e => {	document.getElementById('imginput').click();});document.getElementById('imginput').addEventListener("change", e => {	const reader = new FileReader(); // Create a new FileReader object	reader.onload = function () {		document.getElementById('user-profile-img').src = reader.result; // Set the source of the image tag to the selected image	}	reader.readAsDataURL(e.target.files[0]); // Read the selected file as a data URL});function arrow1() {    var a = document.getElementById("s1");    var c = document.getElementById("s2");    for (var i = 0; i < a.length; i++) {        if (a.options[i].selected == true) {            a.options[i].selected = false            c.add(a.options[i])            arrow1()        }    }}function arrow2() {    var a = document.getElementById("s1");    var c = document.getElementById("s2");    for (var i = 0; i < c.length; i++) {        if (c.options[i].selected == true) {            c.options[i].selected = false            a.add(c.options[i])            arrow2()        }    }}function arrow3() {    var a = document.getElementById("s1");    var c = document.getElementById("s2");    for (var i = 0; i < a.length;) {        c.add(a.options[c, i])    }}function arrow4() {    var a = document.getElementById("s1");    var c = document.getElementById("s2");    for (var i = 0; i < c.length;) {        a.add(c.options[a, i])    }}document.getElementById('SavingSkills').addEventListener("click", e => {    var selectedSkills = [];    const skillsSelected = $('#s2 option');    for (var i = 0; i < skillsSelected.length; i++) {        selectedSkills.push(skillsSelected[i].value);    }    console.log(selectedSkills);    $.ajax({        url: '/Employee/UserProfile/SaveUserSkills',        method: 'POST',        data: { selectedSkills: selectedSkills },        success: function (response) {            debugger;            $('#userskilldiv').html($(response).find('#userskilldiv').html());            window.location.href = '/UserProfile/UserProfile';            //document.getElementById('close').click();        },        error: function () {            alert("could not comment");        }    });});function oldp() {    $('#old-val').addClass('d-none');}function validatePassword(password) {    // password must be at least 8 characters long    if (password.length < 8) {        return false;    }    // password must contain at least one uppercase letter    if (!/[A-Z]/.test(password)) {        return false;    }    // password must contain at least one lowercase letter    if (!/[a-z]/.test(password)) {        return false;    }    // password must contain at least one special character    if (!/[\W_]/.test(password)) {        return false;    }    // password is valid    return true;} function ChangePassword() {    var oldpass = document.getElementById('old').value;    var newpass = document.getElementById('new').value;    var confirmpass = document.getElementById('conf').value;    const isValid = validatePassword(newpass)    if (oldpass == "") {        $('#old-val').removeClass('d-none');        $('#old-val').addClass('d-inline!important');    }    else if (newpass == "") {        Swal.fire('Please Enter New Password')    }    else if (confirmpass == "") {        Swal.fire('Please Enter Confrim Password')    }    else if (newpass != confirmpass) {        Swal.fire('Passowrd and Confrim Password Does not Match')    }    else if (isValid == false) {        Swal.fire('Password is not Valid')    }    else {        $.ajax({            url: '/Employee/UserProfile/ChangePassword',            type: 'POST',            data: { old: oldpass, newp: newpass, conf: confirmpass },            success: function (response) {                if (response == true) {                    alert("password change successfully");                }                else {                    alert("Password Don't Get Change Due to Some Issues");                }            },            error: function () {                alert("could not comment");            }        });    }}