<br />
<h1 align="center">Photino.NET.API</h1>
<h3 align="center">Photino.NET API for ease</h3>
<br />
<br />
<br />
Available for:

- .NET 6.0+

<br />
Supported Platforms:

- Windows 10+ (x86, amd64, arm64)
- Linux(WSL) (amd64)
- OSX 11+ (amd64, arm64)

<br />
<hr>
<br />
<br />

# Install
```bash
dotnet add package Photino.NET.API
# or specific version
dotnet add package Photino.NET.API --version {version}
```

<br />
<br />

# Usage
- use "PhotinoAPIWindow" instead "PhotinoWindow".
- check [Test](https://github.com/oyajiDev/Photino.NET.API/blob/master/Tests/Program.cs) or [PhotinoAPIWindow](https://github.com/oyajiDev/Photino.NET.API/blob/master/Source/PhotinoAPIWindow.cs) class.
```c#
using System;
using PhotinoNET;

namespace Photino.NET.API.Tests {
    internal class Program {
        [STAThread]
        static void Main(String[] args) {
            var appExeDir = AppContext.BaseDirectory;

            var window = new PhotinoAPIWindow()
                .SetLogVerbosity(true)
                .RegisterAPI(new APIs.Counter())
                .SetTitle("{title}")
                .SetUseOsDefaultSize(false)
                .SetWidth(600).SetHeight(400).Center()
                .SetDevToolsEnabled(true)
                .SetContextMenuEnabled(true)
                .SetRemoveTempFile(false)
                .LoadFile("{file}");

            window.WaitForClose();
        }
    }
}
```
