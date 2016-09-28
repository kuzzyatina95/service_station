var telInput = $("#Phone"),
  errorMsg = $("#error-msg"),
  validMsg = $("#valid-msg");

// initialise plugin
telInput.intlTelInput({
    utilsScript: "/Scripts/utils.js",
    preferredCountries: ["by"],
    nationalMode: false,
    autoFormat: false,
    formatOnInit: false
});

var reset = function () {
    telInput.removeClass("error");
    errorMsg.addClass("hide");
    validMsg.addClass("hide");
};

// on blur: validate
telInput.blur(function () {
    reset();
    if ($.trim(telInput.val())) {
        if (telInput.intlTelInput("isValidNumber")) {
            validMsg.removeClass("hide");
        } else {
            telInput.addClass("error");
            errorMsg.removeClass("hide");
        }
    }
});

// on keyup / change flag: reset
telInput.on("keyup change", reset);


$("#VIN").inputmask({
    mask: "V{13}9{4}",
    definitions: {
        'V': {
            validator: "[A-HJ-NPR-Za-hj-npr-z\\d]",
            cardinality: 1,
            casing: "upper"
        }
    },
    clearIncomplete: true,
    autoUnmask: true
});