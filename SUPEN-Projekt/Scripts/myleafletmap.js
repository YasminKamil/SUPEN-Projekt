var latitude;
var longitude;
var companyName;
var bookingsWithDistance;

function myFunc(latitude, longitude, companyName, bookingsWithDistance) {
    this.latitude = latitude;
    this.longitude = longitude;
    this.companyName = companyName;
    this.bookingsWithDistance = bookingsWithDistance;
}

myFuncWrapper();

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
    .setContent(companyName)
    .openOn(map);

var markers = [{ "lat": latitude, "long": longitude }];


for (var i = 0; i < bookingsWithDistance.length; i++) {

    var lati = bookingsWithDistance[i].Latitude;
    var longi = bookingsWithDistance[i].Longitude;

    markers.push({ "lat": parseFloat(lati), "long": parseFloat(longi) });
    
}

for (var j = 0; j < markers.length; j++) {

    L.marker([parseFloat(markers[j].lat), parseFloat(markers[j].long)]).addTo(map);
    
}

