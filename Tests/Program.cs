
using System;
using System.IO;
using System.CommandLine;
using System.Runtime.InteropServices;
using PhotinoNET;


namespace Photino.NET.API.Tests {
    internal class Program {
        [STAThread]
        static void Main(params String[] args) {
            var rootCmd = new RootCommand();
            var debugOpt = new Option<Boolean>("--debug");
            rootCmd.AddOption(debugOpt);

            rootCmd.SetHandler(Program.Run, debugOpt);
            rootCmd.Invoke(args);
        }

        static void Run(Boolean isDebug) {
            var appExeDir = AppContext.BaseDirectory;
            // String iconFile;
            // if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            //     iconFile = Path.Join(appExeDir, "dist", "icon.ico");
            // }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
            //     iconFile = Path.Join(appExeDir, "dist", "icon.icns");
            // }
            // else {
            //     iconFile = Path.Join(appExeDir, "dist", "icon.png");
            // }
            String iconName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                iconName = "Photino.NET.API.Tests.assets.icons.icon.ico";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                iconName = "Photino.NET.API.Tests.assets.icons.icon.icns";
            }
            else {
                iconName = "Photino.NET.API.Tests.assets.icons.icon.png";
            }

            var window = new PhotinoAPIWindow()
                .SetLogVerbosity(isDebug ? 1 : 0)
                .RegisterAPI(new APIs.Counter())
                .SetTitle("Photino.NET.API.Tests")
                // .SetIconFile(iconFile)
                .SetIconFromResource(iconName)
                .SetUseOsDefaultSize(false).SetWidth(600).SetHeight(400).Center()
                .SetDevToolsEnabled(isDebug).SetRemoveTempFile(!isDebug)
                .LoadFile(Path.Join(appExeDir, "dist", "index.html"));

            window.WaitForClose();
        }
    }
}
