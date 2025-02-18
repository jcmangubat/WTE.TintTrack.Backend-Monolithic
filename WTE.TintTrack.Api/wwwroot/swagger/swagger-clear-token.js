// Function to clear the Swagger token from localStorage and sessionStorage
function clearSwaggerToken() {
    const tokenKey = 'Bearer'; // Change this to the exact key used by Swagger if needed
    const storedToken = window.localStorage.getItem(tokenKey) || window.sessionStorage.getItem(tokenKey);

    if (storedToken) {
        console.log("Clearing token:", storedToken);
        window.localStorage.removeItem(tokenKey);
        window.sessionStorage.removeItem(tokenKey);
    } else {
        console.log("No token found to clear.");
    }
}

// Monkey-patch Swagger's "try it out" function to detect the Logout response
const originalFetch = window.fetch;
window.fetch = async (url, options) => {
    const response = await originalFetch(url, options);

    // Check if the URL is the Logout endpoint
    if (url.includes("/logout") && response.ok) {
        console.log("Logout endpoint called, clearing token.");
        clearSwaggerToken();
    }

    return response;
};