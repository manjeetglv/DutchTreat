jQuery(document).ready(function () {
    var x = 0;
    var s = "";

    console.log("Hello Plural sight");


    var emailForm = jQuery("#email-form");
    emailForm.hide();

    var buyButton = jQuery("#buy-button");
    buyButton.on("click", function () {
        console.log("Buying Item");
    });

    var productInfo = jQuery(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + jQuery(this).text());
    });
    
    var loginToggle = jQuery("#login-toggle");
    var popupForm = jQuery(".popup-form");
    
    loginToggle.on("click", function () {
        popupForm.slideToggle(500);
    })
    
});