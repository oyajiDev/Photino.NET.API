
using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;


namespace PhotinoNET {
    public class PhotinoAPIWindow: PhotinoWindow {
        public object JS_API;
        private List<String> excludeNames = new List<String>() { "GetType", "ToString", "Equals", "GetHashCode" };
        private String apiJsFile = null;
        private String tempHtmlFile = null;
        private String tempIconFile = null;
        private Boolean removeTempFile = false;

        private List<String> apiMethodNames {
            get => this.JS_API.GetType().GetMethods().Where(
                x => !this.excludeNames.Contains(x.Name)
            ).Select(x => x.Name).ToList();
        }

        private object InvokeAPIMethod(String name, object[] parameters = null) {
            System.Reflection.MethodInfo info = this.JS_API.GetType().GetMethod(name);
            return info.Invoke(this.JS_API, parameters);
        }
        private void onMessageReceive(object sender, String message) {
            var data = JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(message);
            var result = new Dictionary<String, dynamic>() { { "job", data["job"] } };

            if (data["job"] == "init") {
                result["state"] = "success";
                result["methods"] = this.apiMethodNames;

                if (this.removeTempFile || !this.DevToolsEnabled) {
                    if (this.apiJsFile != null) {
                        File.Delete(this.apiJsFile);
                    }

                    if (this.tempHtmlFile != null) {
                        File.Delete(this.tempHtmlFile);
                    }

                    if (this.tempIconFile != null) {
                        File.Delete(this.tempIconFile);
                    }
                }
            }
            else if (this.apiMethodNames.Contains(data["job"])) {
                try {
                    var msg = this.InvokeAPIMethod(data["job"], data["args"].ToObject<object[]>());
                    if (msg is null) {
                        result["message"] = new Dictionary<String, dynamic>();
                    }
                    else {
                        result["message"] = msg;
                    }

                    result["state"] = "success";
                }
                catch (Exception e) {
                    result["state"] = "fail";
                    result["message"] = e.Message;
                }
            }

            (sender as PhotinoWindow).SendWebMessage(
                JsonConvert.SerializeObject(result)
            );
        }

        public PhotinoAPIWindow(): base() {
            this.RegisterWebMessageReceivedHandler(this.onMessageReceive);
        }

        public PhotinoAPIWindow RegisterAPI(object jsapi) {
            this.JS_API = jsapi;
            this.Log($".RegisterAPI({jsapi})");
            return this;
        }
        public new PhotinoAPIWindow Center() {
            base.Center();
            return this;
        }
        public new PhotinoAPIWindow ClearBrowserAutoFill() {
            base.ClearBrowserAutoFill();
            return this;
        }
        public new PhotinoAPIWindow Invoke(Action workItem) {
            base.Invoke(workItem);
            return this;
        }
        public new PhotinoAPIWindow Load(String path) {
            throw new NotImplementedException();
        }
        public PhotinoAPIWindow LoadFile(String path, Boolean includeApiScript = true) {
            if (includeApiScript) {
                // String jsScript;
                this.apiJsFile = Path.Join(Path.GetDirectoryName(path), ".photino.net.api.min.js");
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.photino.net.api.min.js")) {
                    using (var reader = new StreamReader(stream)) {
                        File.WriteAllText(this.apiJsFile, reader.ReadToEnd());
                    }
                }

                var html = File.ReadAllText(path, encoding: System.Text.Encoding.UTF8);
                html = html.Replace("<head>", $@"
    <head>
        <script type='text/javascript' src='{this.apiJsFile}'></script>
");

                this.tempHtmlFile = Path.Join(Path.GetDirectoryName(path), "." + Path.GetFileName(path));
                File.WriteAllText(this.tempHtmlFile, html);
                base.Load(this.tempHtmlFile);
            }
            else {
                base.Load(path);
            }

            return this;
        }
        public PhotinoAPIWindow LoadUri(Uri uri) {
            base.Load(uri);
            return this;
        }
        public new PhotinoAPIWindow LoadRawString(String content) {
            base.LoadRawString(content);
            return this;
        }
        public new PhotinoAPIWindow MoveTo(Int32 left, Int32 top, Boolean allowOutsideWorkArea = false) {
            base.MoveTo(left, top, allowOutsideWorkArea);
            return this;
        }
        public new PhotinoAPIWindow MoveTo(Point location, Boolean allowOutsideWorkArea = false) {
            base.MoveTo(location, allowOutsideWorkArea);
            return this;
        }
        public new PhotinoAPIWindow Offset(Int32 left, Int32 top) {
            base.Offset(left, top);
            return this;
        }
        public new PhotinoAPIWindow Offset(Point offset) {
            base.Offset(offset);
            return this;
        }
        public new PhotinoAPIWindow RegisterCustomSchemeHandler(String scheme, NetCustomSchemeDelegate handler) {
            base.RegisterCustomSchemeHandler(scheme, handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterFocusInHandler(EventHandler handler) {
            base.RegisterFocusInHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterFocusOutHandler(EventHandler handler) {
            base.RegisterFocusOutHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterLocationChangedHandler(EventHandler<Point> handler) {
            base.RegisterLocationChangedHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterMaximizedHandler(EventHandler handler) {
            base.RegisterMaximizedHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterMinimizedHandler(EventHandler handler) {
            base.RegisterMinimizedHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterRestoredHandler(EventHandler handler) {
            base.RegisterRestoredHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterSizeChangedHandler(EventHandler<Size> handler) {
            base.RegisterSizeChangedHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterWebMessageReceivedHandler(EventHandler<String> handler) {
            base.RegisterWebMessageReceivedHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterWindowClosingHandler(NetClosingDelegate handler) {
            base.RegisterWindowClosingHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterWindowCreatedHandler(EventHandler handler) {
            base.RegisterWindowCreatedHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow RegisterWindowCreatingHandler(EventHandler handler) {
            base.RegisterWindowCreatingHandler(handler);
            return this;
        }
        public new PhotinoAPIWindow SetChromeless(Boolean chromeless) {
            base.SetChromeless(chromeless);
            return this;
        }
        public new PhotinoAPIWindow SetContextMenuEnabled(Boolean enabled) {
            base.SetContextMenuEnabled(enabled);
            return this;
        }
        public new PhotinoAPIWindow SetDevToolsEnabled(Boolean enabled) {
            base.SetDevToolsEnabled(enabled);
            return this;
        }
        public new PhotinoAPIWindow SetFullScreen(Boolean fullScreen) {
            base.SetFullScreen(fullScreen);
            return this;
        }
        public new PhotinoAPIWindow SetGrantBrowserPermissions(Boolean grant) {
            base.SetGrantBrowserPermissions(grant);
            return this;
        }
        public new PhotinoAPIWindow SetHeight(Int32 height) {
            base.SetHeight(height);
            return this;
        }
        public new PhotinoAPIWindow SetIconFile(String iconFile) {
            base.SetIconFile(iconFile);
            return this;
        }
        public PhotinoAPIWindow SetIconFromResource(String resourceName) {
            var assembly = System.Reflection.Assembly.GetCallingAssembly();
            this.tempIconFile = Path.Join(Path.GetDirectoryName(assembly.Location), resourceName);
            using (var stream = assembly.GetManifestResourceStream(resourceName)) {
                using (var writeStream = File.Create(this.tempIconFile)) {
                    stream.CopyTo(writeStream);
                }
            }

            return this.SetIconFile(this.tempIconFile);
        }
        public new PhotinoAPIWindow SetLeft(Int32 left) {
            base.SetLeft(left);
            return this;
        }
        public new PhotinoAPIWindow SetLocation(Point location) {
            base.SetLocation(location);
            return this;
        }
        public new PhotinoAPIWindow SetLogVerbosity(Int32 verbosity) {
            base.LogVerbosity = verbosity;
            base.SetLogVerbosity(verbosity);
            return this;
        }
        public new PhotinoAPIWindow SetMaximized(Boolean maximized) {
            base.SetMaximized(maximized);
            return this;
        }
        public new PhotinoAPIWindow SetMinimized(Boolean minimized) {
            base.SetMinimized(minimized);
            return this;
        }
        public new PhotinoAPIWindow SetResizable(Boolean resizable) {
            base.SetResizable(resizable);
            return this;
        }
        public new PhotinoAPIWindow SetSize(Size size) {
            base.SetSize(size);
            return this;
        }
        public new PhotinoAPIWindow SetSize(Int32 width, Int32 height) {
            base.SetSize(width, height);
            return this;
        }
        public new PhotinoAPIWindow SetTemporaryFilesPath(String tempFilesPath) {
            base.SetTemporaryFilesPath(tempFilesPath);
            return this;
        }
        public new PhotinoAPIWindow SetTitle(String title) {
            base.SetTitle(title);
            return this;
        }
        public new PhotinoAPIWindow SetTop(Int32 top) {
            base.SetTop(top);
            return this;
        }
        public new PhotinoAPIWindow SetTopMost(Boolean topMost) {
            base.SetTopMost(topMost);
            return this;
        }
        public new PhotinoAPIWindow SetUseOsDefaultLocation(Boolean useOsDefault) {
            base.SetUseOsDefaultLocation(useOsDefault);
            return this;
        }
        public new PhotinoAPIWindow SetUseOsDefaultSize(Boolean useOsDefault) {
            base.SetUseOsDefaultSize(useOsDefault);
            return this;
        }
        public new PhotinoAPIWindow SetWidth(Int32 width) {
            base.SetWidth(width);
            return this;
        }
        public new PhotinoAPIWindow SetZoom(Int32 zoom) {
            base.SetZoom(zoom);
            return this;
        }
        public PhotinoAPIWindow SetRemoveTempFile(Boolean removeTempFile) {
            this.removeTempFile = removeTempFile;
            return this;
        }
        public new PhotinoAPIWindow Win32SetWebView2Path(String data) {
            base.Win32SetWebView2Path(data);
            return this;
        }

        private void Log(String message) {
            if (this.LogVerbosity < 1) return;
            Console.WriteLine($"Photino.NET: \"{this.Title ?? "PhotinoWindow"}\"{message}");
        }
    }
}
