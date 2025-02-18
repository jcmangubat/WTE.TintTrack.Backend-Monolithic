/*document.addEventListener('DOMContentLoaded', function () {
    // Ensure the document.body is ready
    var body = document.body;

    if (body) {
        // Function to modify the topbar after it's rendered
        function modifySwaggerTopbar() {
            var topbar = document.querySelector('.topbar-wrapper');

            if (topbar) {
                // Modify the content
                topbar.innerHTML = '<a href="https://your-link.com" class="link"><img src="https://your-image.com/logo.png" alt="New Logo" height="40"></a>';
                return true;  // Return true if modification was successful
            }
            return false;
        }

        // MutationObserver to detect changes in the DOM
        var observer = new MutationObserver(function (mutations, observer) {
            // Check if the topbar is available and modify it
            if (modifySwaggerTopbar()) {
                observer.disconnect();  // Stop observing once modification is done
            }
        });

        // Start observing the body for changes
        observer.observe(body, { childList: true, subtree: true });
    }
});
*/

document.addEventListener('DOMContentLoaded', function () {
    // Ensure the document.body is ready
    var body = document.body;

    if (body) {
        // Function to modify the topbar after it's rendered
        function modifySwaggerTopbar() {
            var topbar = document.querySelector('.topbar-wrapper');

            if (topbar) {
                // Find the Swagger logo within the topbar and replace it
                var logoLink = topbar.querySelector('a.link');

                if (logoLink) {
                    logoLink.innerHTML = '<img src="/swagger/wte-tinttrack-logo.png" alt="New Logo" height="40">';
                    logoLink.href = "https://wte-tinttrack-backend-dev.azurewebsites.net";  // Update link if necessary
                }

                return true;  // Return true if modification was successful
            }
            return false;
        }

        // MutationObserver to detect changes in the DOM
        var observer = new MutationObserver(function (mutations, observer) {
            // Check if the topbar is available and modify it
            if (modifySwaggerTopbar()) {
                observer.disconnect();  // Stop observing once modification is done
            }
        });

        // Start observing the body for changes
        observer.observe(body, { childList: true, subtree: true });
    }
});
