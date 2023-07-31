
function $BindMethod(apiName, methodName) {
    if (window.photino[apiName] == undefined) {
        window.photino[apiName] = {};
    }

    window.photino[apiName][methodName] = (args = [], callback = null) => {
        if (callback != null) {
            window.$photino.callbacks[`${apiName}-${methodName}`] = callback;
        }
        
        var req = JSON.stringify({
            "job": "invoke", "api": apiName, "method": methodName, "args": args
        });
        window.external.sendMessage(req);
    };
}

function $ReceiveMessage(message) {
    var data = null;
    try {
        data = JSON.parse(message);
    }
    catch {
        throw new Error("cannot parse message!");
    }

    if (data.state == "success") {
        if (data.job == "init") {
            if (!data.contextMenuEnabled) {
                document.body.addEventListener("contextmenu", e => e.preventDefault());
            }

            for (var apiName in data.apis) {
                for (var methodName of data.apis[apiName]) {
                    $BindMethod(apiName, methodName);
                }
            }
        }
        else {
            var callback = window.$photino.callbacks[`${data.api}-${data.method}`];
            if (callback != undefined) {
                callback(data.message);
            }
        }
    }
    else {
        throw new Error(data.message);
    }
}

window.addEventListener("DOMContentLoaded", () => {
    window.photino = {};
    window.$photino = { callbacks: {} };

    window.external.receiveMessage($ReceiveMessage);
    window.external.sendMessage(JSON.stringify({
        job: "init"
    }));
});
