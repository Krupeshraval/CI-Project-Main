function showList(e) {
    
    var $gridCont = $('.grid-container');
    e.preventDefault();
    $gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
}
function gridList(e) {
  
    var $gridCont = $('.grid-container')
    e.preventDefault();
    $gridCont.removeClass('list-view');
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);



function loginerr() {
    alert("Please Login First");
}

function coworker(Id, missionid)
{


    var Email = Array.from(document.querySelectorAll('input[name="email"]:checked')).map(e => e.id);
    //var currentURL = window.location.href;
    Swal.fire({
        icon: 'info',
        title: 'Sending...',
        text: 'sending mail...',

    });
    $.ajax
        ({
            url: '/Employee/User/sendRecom',
            type: 'POST',
            data: { missionid: missionid, Id: Id, Email: Email},
            success: function (result) {
                /* alert("Recomendations sent successfully!");*/
                const checkboxes = document.querySelectorAll('input[name="email"]:checked');
                checkboxes.forEach((checkbox) => {
                    checkbox.checked = false;
                });
                Swal.fire({
                    icon: 'success',
                    title: 'sent',
                    text: 'sent Successfully'

                });
            },
            error: function () {

                // Handle error response from the server, e.g. show an error message to the user
                alert('Error: Could not recommend mission.');
            }
        });

}

function AddtoFav(Dil) {

    const params = new URLSearchParams(window.location.search);
    console.log(params)
    const query = Dil.value;
    const Uid = params.get('id');
    alert(Uid);
    $.ajax({
        url: "/Employee/User/AddToFav",
        data: { missionId: query , id : Uid },
        success: function (result) {
            alert("Success");
            console.log(result)
            var $response = $(result);
            //query the jq object for the values
            var FilteredData = $response.find('#Dill').html();
            $('#Dill').html(FilteredData);
        }
    });
}


function coworker2(missionid) {

    var Email = Array.from(document.querySelectorAll('input[name="email"]:checked')).map(e => e.id);
    //var sendbtn = document.getElementById("sendbutton");
    //sendbtn.innerHTML = "Sending...";
    $.ajax
        ({



            url: '/Employee/User/sendRecomlanding',
            type: 'POST',
            data: { missionid: missionid, Email: Email },


            success: function (result) {

                const checkboxes = document.querySelectorAll('input[name="email"]:checked');
                checkboxes.forEach((checkbox) => {
                    checkbox.checked = false;

                });
                //sendbtn.innerHTML = "Send successfully";
                //setTimeout(() => {


                //    //sendbtn.innerHTML = "Send Recommandation";

                //}, 2000);

            },
            error: function () {
                // Handle error response from the server, e.g. show an error message to the user
                alert('Error: Could not recommend mission.');
            }
        });

}


//function pagination(jpg) {
//    //var country = [];
//    //$("input[type='checkbox'][name='country']:checked").each(function () {
//    //    country.push($(this).val());
//    //});

//    //var city = [];
//    //$("input[type='checkbox'][name='city']:checked").each(function () {
//    //    city.push($(this).val());
//    //});

//    //var theme = [];
//    //$("input[type='checkbox'][name='theme']:checked").each(function () {
//    //    theme.push($(this).val());
//    //});


//    $.ajax({
//        url: '/User/_LandingPageCards',
//        type: 'GET',
//        data: { 'jpg': jpg },
//        success: function (res) {
//            $("#Mycards").html(res);
//        },

//        error: function () {
//            alert("error");
//        }
//    });
//}

//$(document).ready(function () {
//    pagination(jpg = 1);
//    console.log("hello krupsi");
//});

//function coworker2(Id, missionid) {

//        var Email = Array.from(document.querySelectorAll('input[name="email"]:checked')).map(e => e.id);
//        var sendbtn = document.getElementById("sendbutton");
//        sendbtn.innerHTML = "Sending...";
//        $.ajax
//            ({



//                url: 'Employee/User/sendRecomlanding',
//                type: 'POST',
//                data: { missionid: missionid, Email: Email },


//                success: function (result) {

//                    const checkboxes = document.querySelectorAll('input[name="email"]:checked');
//                    checkboxes.forEach((checkbox) => {
//                        checkbox.checked = false;

//                    });
//                    sendbtn.innerHTML = "Send successfully";
//                    setTimeout(() => {


//                        sendbtn.innerHTML = "Send Recommandation";

//                    }, 2000);

//                },
//                error: function () {
//                    // Handle error response from the server, e.g. show an error message to the user
//                    alert('Error: Could not recommend mission.');
//                }
//            });

//    }