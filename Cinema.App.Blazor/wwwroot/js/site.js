// Функция за вземане на antiforgery token от cookie
function getAntiforgeryToken() {
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i].trim();
        if (cookie.startsWith('__RequestVerificationToken=')) {
            return decodeURIComponent(cookie.substring('__RequestVerificationToken='.length));
        }
    }
    return '';
}

// Алтернативна функция за вземане от meta tag
function getAntiforgeryTokenFromMeta() {
    const metaTag = document.querySelector('meta[name="csrf-token"]');
    return metaTag ? metaTag.getAttribute('content') : '';
}