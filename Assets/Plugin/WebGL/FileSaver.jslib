mergeInto(LibraryManager.library, {
  SyncFiles: function () {
    FS.syncfs(false, function (err) {
      // This forces the Emscripten file system to sync with IndexedDB
      console.log("IndexedDB Synced!");
    });
  },
});