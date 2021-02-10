// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let checkoutBtn = document.getElementById('checkout-btn');
let checkoutDiv = document.getElementById('checkout-form');
let shippingBtn = document.getElementById('shipping-checkbox')
let shippingDiv = document.getElementById('shipping-form');
let checkbox = document.querySelector("input[name=checkbox]");

checkoutBtn.addEventListener("click", function() {
    OnDisplayBtnClickHandler(checkoutDiv);
});

shippingBtn.addEventListener("change", function() {
    OnDisplayBtnClickHandler(shippingDiv);
});

function OnDisplayBtnClickHandler(actionDiv)
{
    actionDiv.classList.toggle('d-none');
}