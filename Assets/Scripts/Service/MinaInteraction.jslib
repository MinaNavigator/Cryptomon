mergeInto(LibraryManager.library, {   
    GetAccount: async function (callback) {
      const account = await window.mina.requestAccounts();
      var returnValue = account[0];     
      var result = "";
      if(returnValue !== null) {
            var bufferSize = lengthBytesUTF8(returnValue) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(returnValue, buffer, bufferSize);
            result = buffer;
      }
      dynCall_vi(callback, result);
    },
    SignMessage: async function (data, callback) {
     var signContent = {
            message : UTF8ToString(data)
        };
      var returnValue = await window.mina.signMessage(signContent);
      var result = "";
      if(returnValue !== null) {
            var bufferSize = lengthBytesUTF8(returnValue) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(returnValue, buffer, bufferSize);
            result = buffer;
      }
      dynCall_vi(callback, result);
    }   
});