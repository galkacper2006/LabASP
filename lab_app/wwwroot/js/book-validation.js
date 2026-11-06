(function ($) {
    "use strict";

    $.validator.addMethod("isbn", function (value, element, params) {
        if (this.optional(element)) return true;
        if (!/^[0-9 -]+$/.test(value)) return false;
        var digits = value.replace(/[ -]/g, "");
        if (digits.length !== 10 && digits.length !== 13) return false;
        if (!params.checksum || digits.length === 10) return true;

        var sum = 0;
        for (var i = 0; i < digits.length; i++) {
            sum += Number(digits[i]) * (i % 2 === 0 ? 1 : 3);
        }
        return sum % 10 === 0;
    });

    $.validator.unobtrusive.adapters.add("isbn", ["checksum"], function (options) {
        options.rules.isbn = { checksum: options.params.checksum === "true" };
        options.messages.isbn = options.message;
    });

    $.validator.addMethod("notinfuture", function (value, element) {
        if (this.optional(element)) return true;
        var parts = value.split("-");
        if (parts.length !== 3) return true;
        var date = new Date(Number(parts[0]), Number(parts[1]) - 1, Number(parts[2]));
        if (isNaN(date.getTime())) return true;
        var today = new Date();
        today.setHours(0, 0, 0, 0);
        return date <= today;
    });

    $.validator.unobtrusive.adapters.addBool("notinfuture");

    // Gdy formularz został wcześniej sparsowany, zastosuj nowe reguły.
    var $form = $("form[action*='/Book/Create']");
    if ($form.length) {
        $form.removeData("validator");
        $form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);
    }
})(jQuery);
