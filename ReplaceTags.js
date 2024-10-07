    document.addEventListener('DOMContentLoaded', function() {
        // Function to replace the elements
        function replaceElements() {
            console.log("Checking for elements to replace...");

            // Replace spans containing "ReplaceTag[...]"
            const replaceTagElements = document.querySelectorAll('span');
            replaceTagElements.forEach(span => {
                const match = span.textContent.match(/ReplaceTag\[(.*?)\]/);
                if (match && match[1]) {
                    console.log("Found replace tag in span:", span.textContent);
                    // Decode HTML entities
                    const decodedHTML = match[1]
                        .replace(/&lt;/g, '<')
                        .replace(/&gt;/g, '>')
                        .replace(/&amp;/g, '&'); // Handle any ampersands if present

                    // Create a new span with the decoded content
                    const newElement = document.createElement('span');
                    newElement.innerHTML = decodedHTML; // Set the inner HTML to the decoded content
                    if (span.parentNode) {
                        console.log("Replacing span with new content...");
                        span.parentNode.replaceChild(newElement, span); // Replace the old span with the new one
                    }
                }
            });
        }

        // Create a MutationObserver to monitor DOM changes
        const observer = new MutationObserver(mutations => {
            mutations.forEach(mutation => {
                if (mutation.type === 'childList') {
                    console.log("DOM changed. Checking for new elements...");
                    replaceElements();
                }
            });
        });

        // Start observing the body or a specific parent element
        observer.observe(document.body, {
            childList: true,
            subtree: true // Observe all descendants
        });

        // Button click to trigger immediate replacements (for testing)
        const outputButton = document.querySelector('button[type="button"][role="button"]');
        if (outputButton) {
            outputButton.addEventListener('click', function() {
                console.log("Button clicked. Triggering replacements...");
                replaceElements();
            });
        }
    });
