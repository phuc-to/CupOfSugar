﻿//code to enable the dark mode when the dark mode button on the navbar is toggled
var darkSwitch = document.getElementById("darkSwitch");
window.addEventListener("load", (function () {
    if (darkSwitch) {
        initTheme();
        darkSwitch.addEventListener("change", (function () {
            resetTheme()
        }))
    }
}));

function initTheme() {
    var darkThemeSelected = localStorage.getItem("darkSwitch") !== null && localStorage.getItem("darkSwitch") === "dark";
    darkSwitch.checked = darkThemeSelected;
    darkThemeSelected ? document.body.setAttribute("data-theme", "dark") : document.body.removeAttribute("data-theme")
}

function resetTheme() {
    if (darkSwitch.checked) {
        document.body.setAttribute("data-theme", "dark");
        localStorage.setItem("darkSwitch", "dark")
    } else {
        document.body.removeAttribute("data-theme");
        localStorage.removeItem("darkSwitch")
    }
}