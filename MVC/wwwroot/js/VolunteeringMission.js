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



function checkFav(missionid,id) {
  //  alert("Hello");
    const params = new URLSearchParams(window.location.search);
    const query = params.get('missionid');
    $.ajax({
        url: "/User/AddToFav",
        data: { missionid: missionid,id:id },
        success: function (response) {
            console.log(response)
            //if (response.isFav) {
            //    document.getElementById("addToFavorite").style.color = "gray";
            //}
            //else {
            //    document.getElementById("addToFavorite").style.color = "red";
            //}
            $('#abcd').html($(response).find('#abcd').html());
        },
        error: function () {
            alert("Error : unable to like mission");
        }
    });

}



//function ratemission(starId, missionId, id) {
//    $.ajax({
//        url: '/User/AddRating',
//        type: 'POST',
//        data: { missionId: missionId, id: id, rating: starId },
//        success: function (result) {
//            if (parseInt(result.ratingExists.rating, 10)) {
//                //     Update the heart icon to show that the mission has been liked
//                for (i = 1; i <= result.ratingExists.rating; i++) {
//                    console.log('.star-' + i);
//                    var starbtn = document.querySelector('.star-' + i);
//                    console.log(starbtn);
//                    starbtn.style.color = "#F88634";
//                }
//                for (i = result.ratingExists.rating + 1; i <= 5; i++) {

//                    var starbtn = document.querySelector('.star-' + i);;
//                    console.log(starbtn);
//                    starbtn.style.color = "black";
//                }
//            } else {
//                //    Update the heart icon to show that the mission has been unliked
//                for (i = 1; i <= parseInt(result.newRating.rating, 10); i++) {

//                    var starbtn = document.getElementById(String(i));

//                    starbtn.style.color = "#F88634";
//                }
//                for (i = parseInt(result.newRating.rating, 10) + 1; i <= 5; i++) {

//                    var starbtn = document.getElementById(String(i));

//                    starbtn.style.color = "black";
//                }
//            }
//        },
//        error: function () {
//            // Handle error response from the server, e.g. show an error message to the user
//            alert('Error: Could not like mission.');
//        }
//    });
//}


function ratemission(starId, missionId, id) {
    $.ajax({
        url: '/User/AddRating',
        type: 'POST',
        data: { missionId: missionId, id: id, rating: starId },
        success: function (result) {
            console.log(result)

            $('#givenrating').html($(result).find('#givenrating').html());
        },
        error: function () {
            // Handle error response from the server, e.g. show an error message to the user
            alert('Error: Could not rate mission.');
        }
    });
}



function AddComment() {
    var textBox = document.getElementById("commentbox");
    var commentVal = textBox.value;
    const params = new URLSearchParams(window.location.search);
    const query = params.get('missionid');
    $.ajax({
        url: "/User/AddComment",
        data: { missionId: query, commenttext: commentVal },
        success: function (result)
        {
            $('.commentdiv').html($(result).find('.commentdiv').html());
            /*window.location.reload();*/
        },
        error: function () {
            alert("Error : Mission has not been liked");
        }
    });
}



//function AddtoFav() {
   
//    const params = new URLSearchParams(window.location.search);
//    const query = params.get('missionid');
//    $.ajax({
//        url: "/User/AddToFav",
//        data: { missionId: query },
//        success: function (result) {
//            alert("Success");
//            console.log(result)
//        }
//    });
//}