
using System;
using System.IO;
using System.Runtime.InteropServices;
using PhotinoNET;


namespace Photino.NET.API.Tests {
    internal class Program {
        static void Main(String[] args) {
            var appExeDir = AppContext.BaseDirectory;
            String iconFile;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                iconFile = Path.Join(appExeDir, "dist", "icon.ico");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                iconFile = Path.Join(appExeDir, "dist", "icon.icns");
            }
            else {
                iconFile = Path.Join(appExeDir, "dist", "icon.png");
            }

            var window = new PhotinoAPIWindow()
                .RegisterAPI(new APIs.Counter())
                .SetTitle("Photino.NET.API.Tests").SetIconFile(iconFile)
                .SetUseOsDefaultSize(false).SetWidth(600).SetHeight(400).Center()
                .Load(Path.Join(appExeDir, "dist", "index.html"));

            window.WaitForClose();
        }
    }
}
