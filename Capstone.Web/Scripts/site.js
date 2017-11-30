var baseUrl = "http://localhost:55601/";
var infoWindow;
var markers = [];

function loadMap() {

    var uniqueId = 1;

    var boxItin = $("#itinModal");
    boxItin.hide();

    var wholeThing = $('.wholePage');

    $("#sortable").sortable();
    $(".sortableItems").sortable({
        update: function (event, ui) { }
    });
    $(".sortableItems").on("sortupdate", function (event, ui) {
        var inputs = $("#sortable").find("input[type='hidden']");

        for (var i = 0; i < inputs.length; i++) {

            $(inputs[i]).attr("name", `placeIds[${i}]`)
        }
    });

    $('#my_popup').popup({
        color: 'white',
        opacity: .95,
        transition: '0.3s',
        scrolllock: true
    });

    $('#delete_popup').popup({
        backgroundactive: false,
        scrolllock: true,

    });

    $('#iHateModals').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: "http://localhost:55601/" + "Home/DeleteItinerary",
            method: "POST",
            data: $("#iHateModals").serialize(),
            success: function (data) {
                alert("Successfully deleted.")
            }
        });
    });

    var icons = {
        header: "ui-icon-circle-arrow-e",
        activeHeader: "ui-icon-circle-arrow-s"
    };

    var mapOptions = {
        center: new google.maps.LatLng(41.499320, -81.694361),
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    $(".accordion").accordion({
        header: "h3",
        heightStyle: "content",
        icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" },
    });
    $(".itinName.ui-accordion-header").css("background", "maroon").css("height", "50px").css("color", "white").css("font-size", "25px");

    var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

    //Attach click event handler to the map.
    google.maps.event.addListener(map, 'click', function (e) {

        //Determine the location where the user has clicked.
        var location = e.latLng;

        //Create a marker and placed it on the map.
        var marker = new google.maps.Marker({
            position: location,
            map: map
        });

        //Set unique id
        marker.id = uniqueId;
        uniqueId++;

        //Attach click event handler to the marker.
        google.maps.event.addListener(marker, "click", function (e) {
            var content = 'Latitude: ' + location.lat() + '<br />Longitude: ' + location.lng();
            content += `<br /><input data-id='name-${marker.id}' type = 'text' placeholder = 'Name your location' onclick = 'Save(${marker.id});' />`;
            content += `<br /><input type = 'button' value = 'Save to Itinerary' onclick = 'SaveMarker(${marker.id}, ${location.lat()}, ${location.lng()});' value = 'Save' />`;
            content += `<br /><input type = 'button' value = 'Delete' onclick = 'DeleteMarker(${marker.id});' value = 'Delete' />`;

            if (infoWindow) {
                infoWindow.close();
            }

            infoWindow = new google.maps.InfoWindow({
                content: content
            });
            infoWindow.open(map, marker);
        });

        //Add marker to the array.
        markers.push(marker);
    });
}

function DeleteMarker(id) {
    //Find and remove the marker from the Array
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].id == id) {
            //Remove the marker from Map
            markers[i].setMap(null);

            //Remove the marker from array.
            markers.splice(i, 1);
            return;
        }
    }
};

function SaveMarker(id, lat, lng) {
    var name = $(`input[data-id=name-${id}]`).val();

    var geocoder = new google.maps.Geocoder;

    geocoder.geocode({
        location: {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        }
    }, function (results, status) {
        console.log(results);

        infoWindow.close();
        infoWindow = null;

        if (status === 'OK') {

            var components = results[0].address_components;
            var city = $("#city").val();
            var streetAddress = $("#streetAddress").val();
            var streetName = $("#streetName").val();
            var state = $("#state").val();
            var zip = $("#zip").val();
            var fullAddress = $("#fullAddress").val();
            var total = $("#total").val();

            $.each(components, function (index, component) {
                if (component.types[0] === 'locality') {
                    city = component.long_name;
                }
                if (component.types[0] === 'street_number') {
                    streetAddress = component.long_name;
                }
                if (component.types[0] === 'route') {
                    streetName = component.long_name;
                }
                if (component.types[0] === 'administrative_area_level_1') {
                    state = component.long_name;
                }
                if (component.types[0] === 'postal_code') {
                    zip = component.long_name;
                }
                fullAddress = streetAddress + streetName;
                total = { streetAddress: fullAddress, city: city, state: state, latitude: lat, longitude: lng, placeName: name, zip: zip }
            });

            console.log(`City = ${city}`);
            console.log(JSON.stringify(total));

            console.log(results[0].formatted_address);
        }

        $.ajax({
            url: baseUrl + 'Home/SavedItinerary',
            method: 'POST',
            data: {
                streetAddress: fullAddress,
                city: city,
                state: state,
                latitude: lat,
                longitude: lng,
                placeName: name,
                Category: name,
                zip: zip
            }
        }).then(function (data) {

        });
    });
}

// This example adds a search box to a map, using the Google Place Autocomplete
// feature. People can enter geographical searches. The search box will return a
// pick list containing a mix of places and predicted search terms.

function initialize() {

    var markers = [];
    var map = new google.maps.Map(document.getElementById('map-canvas'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var defaultBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(41.495820, -81.702458),
        new google.maps.LatLng(41.499320, -81.694361)
    );
    map.fitBounds(defaultBounds);

    //Create the search box and link it to the UI element.*/
    var input =/* @*@type {HTMLInputElement}*@*/ (
        document.getElementById('pac-input'));
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(input);

    var searchBox = new google.maps.places.SearchBox(
             /*@*type {HTMLInputElement}*@*/(input));

    // [START region_getplaces]
    // Listen for the event fired when the user selects an item from the
    // pick list. Retrieve the matching places for that item.
    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        // For each place, get the icon, place name, and location.
        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var image = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location
            });

            markers.push(marker);

            bounds.extend(place.geometry.location);
        }

        map.fitBounds(bounds);
    });
    // [END region_getplaces]

    // Bias the SearchBox results towards places that are within the bounds of the
    // current map's viewport.
    google.maps.event.addListener(map, 'bounds_changed', function () {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    });
}

google.maps.event.addDomListener(window, 'load', initialize);

