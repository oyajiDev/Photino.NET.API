## 0.0.1
- Initial release.

<br />

## 0.0.2
- minify "photino.net.api.js" and use as embedded resource.
- add "LoadFile", "LoadUri" methods.
  - unimplement "Load" method.
- add "SetIconFromResource", "SetRemoveTempFile" method.
- "SetLogVerbosity" method change verbosity first.
- update "Test".

<br />

## 0.0.3
- make "ContextMenuEnabled" property to PhotinoAPIWindow.
  - automatically change value to True when tested(Mac).
- change parameter type of "SetLogVerbosity" from Int32 to Boolean.
- change "JS_API" to private property.
- change "RemoveTempFile" to public property.
- use "photino.net.api.js" if DevToolsEnabled else "photino.net.api.min.js".
- register "WindowClosingHandler" to remove temp files.
  - not working on Mac ARM(M1, M2).
- update "Test", "README".

<br />

## 0.0.4
- support multiple api register.
  - call method by "name of API" and "namd of Method".
- add "Responses" namespace.
  - add "JsonResponse" class.
- api does not work when "DevToolsEnabled" is true.
  - fix(temporary).
- update "Test", "README".
