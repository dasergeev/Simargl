let map, marker;

export function init(lat, lng) {
    // если контейнер ещё не в DOM – ждём
    const container = document.getElementById('map');
    if (!container) return;

    // если карта уже есть – пересоздаём
    if (map) {
        map.remove();
        map = null;
        marker = null;
    }

    const savedZoom = sessionStorage.getItem('mapZoom');
    const zoom = savedZoom ? parseInt(savedZoom, 10) : 13;

    map = L.map(container).setView([lat, lng], zoom);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    marker = L.marker([lat, lng]).addTo(map);

    map.on('zoomend', () =>
        sessionStorage.setItem('mapZoom', map.getZoom()));
}

export function update(lat, lng) {
    if (!map) return;
    marker.setLatLng([lat, lng]);
    map.setView([lat, lng], map.getZoom());
}

export function dispose() {
    if (map) {
        map.off();
        map.remove();
        map = null;
        marker = null;
    }
}