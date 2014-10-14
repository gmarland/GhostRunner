// ---- [start_url] is taken from the Initialization task
// ---- [initialization_script] the content of the initialization task will replace this placeholder

var page = require('webpage').create();
page.open('[start_url]', function (status) {
    page.includeJs("http://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js", function () {
        var initializeScript = page.evaluate(function () {
            [initialization_script]
        });

        console.log(initializeScript);
    });
    window.setTimeout(function () {
        phantom.exit();
    }, 3000);
});