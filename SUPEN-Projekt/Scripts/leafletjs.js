var map = L.map('mapid', {
    zoomControl: false
}).setView([parseFloat(latitude), parseFloat(longitude)], 14);


L.control.zoom({
    position: 'bottomright'
}).addTo(map);

L.tileLayer('https://{s}.tile.openstreetmap.se/hydda/full/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    maxZoom: 18,
    subdomains: ['a', 'b', 'c']
}).addTo(map);

L.marker([parseFloat(latitude), parseFloat(longitude)]).addTo(map);

var popup = L.popup()
    .setLatLng([parseFloat(latitude), parseFloat(longitude)])
            .setContent("companyName")
    .openOn(map);

var markers = [{ "lat":latitude , "long": longitude}];



for (var j = 0; j < markers.length; j++) {

    L.marker([parseFloat(markers[j].lat), parseFloat(markers[j].long)]).addTo(map);

}


