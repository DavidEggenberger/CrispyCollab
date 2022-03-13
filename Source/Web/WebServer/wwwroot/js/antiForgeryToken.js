function getAntiForgeryToken() {
    var antiForgeryTokenInput = document.querySelector('.AntiforgeryTokenContainer').querySelectorAll('input');
    return antiForgeryTokenInput[0].Value;
}