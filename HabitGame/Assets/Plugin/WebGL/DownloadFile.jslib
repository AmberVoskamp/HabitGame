mergeInto(LibraryManager.library, {
 DownloadFile: function (filenamePtr, contentPtr) {
        var filename = UTF8ToString(filenamePtr);
        var content = UTF8ToString(contentPtr);

        // Create a blob from the string
        var blob = new Blob([content], { type: 'application/json' });
        var url = window.URL.createObjectURL(blob);

        // Create a temporary link element
        var trigger = document.createElement('a');
        trigger.href = url;
        trigger.download = filename;

        // Trigger the click and clean up
        document.body.appendChild(trigger);
        trigger.click();
        document.body.removeChild(trigger);
        window.URL.revokeObjectURL(url);
    }
});