$(document).ready(function () {
    filters();

});
function filters(sortValue,jpg=1) {
    var Search = $("input[name='SearchingMission']").val();
    //if (Search == '')
    //    Search = '';
    console.log(jpg);


    var country = [];

    //$('#countryDropdown').find("input:checked").each(function (i, ob) {
    //    country.push($(ob).val());
    //});
    $("input[type='checkbox'][name='country']:checked").each(function () {
        country.push($(this).val());
    });
    console.log(country)


    var city = [];
    $("input[type='checkbox'][name='city']:checked").each(function () {
        city.push($(this).val());
    });
    console.log(city)

    var theme = [];
    $("input[type='checkbox'][name='theme']:checked").each(function () {
        theme.push($(this).val());
    });
    console.log(theme)

    //add

    $.ajax({
        url: "/User/_LandingPageCards",
        type: "GET",
        data: { 'search': Search, 'jpg': jpg, 'sortValue': sortValue, 'country': country.toString(), 'city': city.toString(), 'theme': theme.toString() },

        success: function (res) {
            $('#Mycards').html('');
            $('#Mycards').html(res);
        },
        error: function () {
            alert("some Error");
        }
        })
}


//Filter selected divs
var checkboxes = document.querySelectorAll(".tick");
for (var checkbox of checkboxes) {
    checkbox.addEventListener("click", function () {
        if (this.checked == true) {
            addElement(this, this.value);
        }
        else {
            removeElement(this.value);
        }
    })
}

/*this function will add filters*/



function addElement(current, value) {
    let filtersSection = document.querySelector("#filters-section");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('p-1');
    createdTag.classList.add('ps-3');
    createdTag.classList.add('border');
    createdTag.classList.add('border-1');
    createdTag.classList.add('rounded-pill');
    createdTag.classList.add('pe-1');
    createdTag.classList.add('me-2');
    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    let crossButton = document.createElement('button');
    crossButton.classList.add("filter-close-button");
    crossButton.classList.add("border-0");
    crossButton.classList.add("me-2");
    crossButton.classList.add("bg-white");
    let cross = '&times';

    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        console.log(elementToBeRemoved);
        console.log(current);
        elementToBeRemoved.remove();

        current.checked = false;

        filters();
    })

    crossButton.innerHTML = cross;

    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);

    filters();

}

function removeElement(value) {

    let filtersSection = document.querySelector("#filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);

    filters();

}
//var cbs = document.querySelectorAll('input[type=checkbox]');
//for (var i = 0; i < cbs.length; i++) {
//    cbs[i].addEventListener('change', function () {
//        if (this.checked) {
//            addElement(this, this.value);
//        }
//        else {

//            removeElement(this.value);
//            console.log("unchecked");
//        }
//    });
//}

//function addElement(current, value) {
//    let filtersSection = document.querySelector(".filters-section");

//    let createdTag = document.createElement('span');
//    createdTag.classList.add('filter-list');
//    createdTag.classList.add('ps-3');
//    createdTag.classList.add('pe-1');
//    createdTag.classList.add('me-2');
//    createdTag.innerHTML = value;

//    createdTag.setAttribute('id', value);
//    let crossButton = document.createElement('button');
//    crossButton.classList.add("filter-close-button");
//    let cross = '&times;'


//    crossButton.addEventListener('click', function () {
//        let elementToBeRemoved = document.getElementById(value);

//        console.log(elementToBeRemoved);
//        console.log(current);
//        elementToBeRemoved.remove();

//        current.checked = false;




//    })

//    crossButton.innerHTML = cross;


//    // let crossButton = '&times;'

//    createdTag.appendChild(crossButton);
//    filtersSection.appendChild(createdTag);

//}
//function ClearAllElement() {

//    var filtersSection = document.querySelector(".filters-section");

//    for (var i = 0; i < filtersSection.length; i++) {
//        filtersSection.pop();
//    }

//}


//function removeElement(value) {

//    let filtersSection = document.querySelector(".filters-section");

//    let elementToBeRemoved = document.getElementById(value);
//    filtersSection.removeChild(elementToBeRemoved);
//}

//$.ajax({
    //    url: "/User/navbarfilters",
    //    type: "POST",
    //    data: { 'search': Search, 'sortValue': sortValue, 'country': country, 'city': city, 'theme': theme },

    //    success: function (res) {

    //        $("#LandingPageCards").html('');
    //        $("#LandingPageCards").html(res);
    //    },
    //    error: function () {
    //        alert("some Error");
    //    }
    //})