var latitude;
var longitude;
var companyName;
var bookingsWithDistance;
var companyId;

function myFunc(latitude, longitude, companyName, bookingsWithDistance, companyId){
    this.latitude = latitude;
    this.longitude = longitude;
    this.companyName = companyName;
    this.bookingsWithDistance = bookingsWithDistance;
    this.companyId = companyId;
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


L.marker([parseFloat(latitude), parseFloat(longitude)], { title: companyName }).bindPopup(companyName, { closeButton: false }).addTo(map).openPopup();

var markers = [{
    "lat": latitude, "long": longitude, "name": companyName, "url": "http://localhost:55341/BookingSystem/BookingSystem/" + companyId
}]

for (var i = 0; i < bookingsWithDistance.length; i++) {

    var lati = bookingsWithDistance[i].Latitude;
    var longi = bookingsWithDistance[i].Longitude;
    var anotherCompanyName = bookingsWithDistance[i].CompanyName;
    var anotherCompanyId = bookingsWithDistance[i].Id;

    markers.push({
        "lat": parseFloat(lati), "long": parseFloat(longi),
        "name": anotherCompanyName, "url": "http://localhost:55341/BookingSystem/BookingSystem/" + anotherCompanyId
    });
}

for (var j = 0; j < markers.length; j++) {

    if (markers[j].name != companyName) {

        L.marker([parseFloat(markers[j].lat), parseFloat(markers[j].long)], { title: markers[j].name })
            .bindPopup('<a href="' + markers[j].url + '" target="_blank">' + markers[j].name + '</a>')
            .addTo(map); 
    }
}

