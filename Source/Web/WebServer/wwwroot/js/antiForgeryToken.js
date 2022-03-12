function getAntiForgeryToken() {
    var antiForgeryTokenInput = document.getElementsByClassName('.AntiforgeryTokenContainer');
    return antiForgeryTokenInput[0].Value;
}