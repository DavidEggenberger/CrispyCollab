// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function hideLoadingScreen() {
    document.getElementById('loadingBackground').remove();
}

function expandAside() {
    document.getElementById('aside').style.width = "120px";
}

function shrinkAside() {
    document.getElementById('aside').style.width = "80px";
}

window.onload = () => {
    if (document.getElementById("ExpandableNavMenuIcon") !== null) {
        document.getElementById("ExpandableNavMenuIcon").addEventListener("click", ExpandNavMenu);
    }
};

function ExpandNavMenu() {
    let height = getComputedStyle(document.getElementById("ExpandableNavMenu")).height;
    console.log(height);
    if (height === "250px") {
        document.getElementById("ExpandableNavMenu").style.height = "0px";
    }
    else {
        document.getElementById("ExpandableNavMenu").style.height = "250px";
    }
}