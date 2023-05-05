let slideIndex = 1;
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
            url: '/Employee/User/sendRecom',
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


function apply(mid, uid) {
    $.ajax({
        url: '/Employee/User/apply',
        type: 'POST',
        data: { MissionId: mid, UserId: uid },
        success: function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Applied',
                text: 'Applied Successfully',

            });

        },
        error: function (result) { }
    });
}

function checkEmail() {
    var data = document.getElementsByName("coworker-email");
    var suiiEmail = [];
    for (var i = 0; i < data.length; i++) {
        if (data[i].ariaChecked) {
            suiiEmail.push(data[i].value);
        }
    }
    console.log(suiiEmail)
}

const countEl = document.getElementById('count');

updateVisitCount();

function updateVisitCount() {

    //Develop a namespace and key for your API, in my case "megs" is my namespace and "count" is my key
    fetch('https://api.countapi.xyz/update/megs/count/?amount=1')
        .then(res => res.json())
        .then(res => {
            countEl.innerHTML = res.value + "  Views";
        })
}