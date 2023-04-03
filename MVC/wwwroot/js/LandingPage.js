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



function apply() {
    alert("Please Login First");
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
