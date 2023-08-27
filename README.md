# CSMemory
Lite & easy-to-use C# memory library

# Adding to project
You can add it by building to solution by yourself or just by taking the CSMemory.dll located in the build folder and [add it to your project][1]

# How to use
You can check by yourself, but there is the main methods:

- [🏷] Get process handle
```csharp
using CSMemory;
IntPtr handle = CSMemory.GetProcessHandle(string processName);
```

- [📖] Read memory
```csharp
using CSMemory;
value = CSMemory.Read<T>(IntPtr process, IntPtr address);
```

- [✍️] Write memory
```csharp
using CSMemory;
bool success = CSMemory.Write<T>(IntPtr process, IntPtr address, T value);
```

[1]: https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022 "Project References"

