// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let checkoutBtn = document.getElementById('checkout-btn');
let checkoutDiv = document.getElementById('checkout-form');
let shippingBtn = document.getElementById('shipping-checkbox')
let shippingDiv = document.getElementById('shipping-form');
let checkbox = document.querySelector("input[name=checkbox]");

let billCountry = document.getElementById('bill-country');
let billCity = document.getElementById('bill-city');
let billZip = document.getElementById('bill-zip');
let billAddress = document.getElementById('bill-address');
let shippCountry = document.getElementById('shipp-country');
let shippCity = document.getElementById('shipp-city');
let shippZip = document.getElementById('shipp-zip');
let shippAddress = document.getElementById('shipp-address');

checkoutBtn.addEventListener("click", function() {
    OnDisplayBtnClickHandler(checkoutDiv);
});

shippingBtn.addEventListener("change", function() {
    // OnDisplayBtnClickHandler(shippingDiv);
    if (this.checked) {
        shippCountry.value = billCountry.value;
        shippCity.value = billCity.value;
        shippAddress.value = billAddress.value;
        shippZip.value = billZip.value;
    }
    else {
        shippCountry.value = null;
        shippCity.value = null;
        shippAddress.value = null;
        shippZip.value = null;
    }
    
});

function OnDisplayBtnClickHandler(actionDiv)
{
    actionDiv.classList.toggle('d-none');
}