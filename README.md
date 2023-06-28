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
- Linux Distos (amd64, arm64)
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
- Sequentials
```c#
using PhotinoNET;


var appExeDir = AppContext.BaseDirectory;
var window = new PhotinoAPIWindow()
    .RegisterAPI(new APIs.Counter())
    .SetTitle("Photino.NET.API.Tests")
    .SetUseOsDefaultSize(false).SetWidth(600).SetHeight(400).Center()
    .Load(Path.Join(appExeDir, "dist", "index.html"));

window.WaitForClose();
```
