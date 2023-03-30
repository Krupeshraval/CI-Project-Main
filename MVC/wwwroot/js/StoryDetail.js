﻿let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("demo");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

function coworker(Id, missionid) {

    var Email = Array.from(document.querySelectorAll('input[name="email"]:checked')).map(e => e.id);
    Swal.fire({
        icon: 'info',
        title: 'Sending...',
        text: 'sending mail...',

    });
    $.ajax
        ({
            url: '/User/sendRecom',
            type: 'POST',
            data: { missionid: missionid, Id: Id, Email: Email },
            success: function (result) {
                /* alert("Recomendations sent successfully!");*/
                const checkboxes = document.querySelectorAll('input[name="email"]:checked');
                checkboxes.forEach((checkbox) => {
                    checkbox.checked = false;
                });
                Swal.fire({
                    icon: 'success',
                    title: 'sent',
                    text: 'sent Successfully',

                });
            },
            error: function () {

                // Handle error response from the server, e.g. show an error message to the user
                alert('Error: Could not recommend mission.');
            }
        });

}