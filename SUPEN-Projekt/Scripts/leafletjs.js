
//        mapboxgl.accessToken = 'pk.eyJ1IjoibmVociIsImEiOiJjanV4eGg1NWgwZjBsNDVtMjNqOGFlMjN2In0.6LFtJumVPCc9ncOf5JG4_A';
//var map = new mapboxgl.Map({
//            container: 'mapid',
//        style: 'mapbox://styles/mapbox/streets-v11'
//        });



//var marker = L.marker([59.26153, 15.20781]).addTo(mymap);



//var ports = $.ajax({
//    url: "http://localhost:55341/api/GetBooking/",
//    dataType: "json",
//    success: console.log("County data successfully loaded."),
//})

//var inBookingSystemId = document.getElementById('mapid').value;
//var inServiceId = document.childNodes.item.getElementById('inServiceId').value;
//var inBookingId = document.childNodes.item.getElementById('inBookingId').value;

var ports = $.ajax({
    url: '@Url.Action("Details", "Booking", new { inBookingSystemId = Model.bookingSystem.BookingSystemId, inServiceId = Model.service.ServiceId, inBookingId = Model.booking.BookingId })',
   type: 'GET',
    dataType: "json",
    success: function (mymap) {
        successful(mymap);
    }

})

var geojson = {
    type: "FeatureCollection",
    features: [],
};

for (var i in ports.data) {
    for (var j in i) {
        if (j.includes(inBookingSystemId))
            
                geojson.features.push({
                    "type": "Feature",
                    "geometry": {
                        "type": "Point",
                        "coordinates": [i[j].longitude, i[j].latitude]
                    },
                    "properties": {
                        "stationName": ports.data[i].port_name
                    }
                });        
       
    }
}

var mymap = L.map('mapid').setView([59.27894, 15.21134], 13);


L.tileLayer('https://{s}.tile.openstreetmap.se/hydda/full/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
    maxZoom: 18,
    subdomains: ['a', 'c', 'd'],
    //    //id: 'mapbox.streets', ?access_token={accessToken}
    //    //accessToken: 'pk.eyJ1IjoiaGVua2UiLCJhIjoiY2p1djFnMWJjMGdhdjQzbm9vODZ6bXBxOCJ9.dqk32dR_wLAotleAp15vfw'
}).addTo(mymap);
L.Marker(L.geoJSON(geojson)).addTo(mymap);
function successful(mymap) {
    $("mapid").geoJSON(mymap).replaceWith;
}
   


