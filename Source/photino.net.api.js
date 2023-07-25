
function $BindMethod(name) {
    window.photino[name] = (args = [], callback = null) => {
        if (callback != null) {
            window.$photino.callbacks[name] = callback;
        }
        
        var req = JSON.stringify({
            "job": name, "args": args
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

            for (var name of data.methods) {
                $BindMethod(name);
            }
        }
        else {
            var callback = window.$photino.callbacks[data.job];
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
    window.$photino = { "callbacks": {} };

    window.external.receiveMessage($ReceiveMessage);
    window.external.sendMessage(JSON.stringify({
        job: "init"
    }));
});
