# Persistence via Port Monitor 

This PoC creates persistence on a system by creating a new registry key under `HKLM\SYSTEM\CurrentControlSet\Control\Print\Monitors`, and specifies a Dll to be loaded. This will load the specified Dll into the `spoolsvc.exe` process on system boot.

>Note: This method requires Administrative privileges as it modifies the Local Machine registry hive

## How to use
Open the `PortMonitor.sln` project file in Visual Studio. You will need to replace the shellcode in the `rawdata.h` header file. This can be done by opening your raw shellcode file with a program like [HxD](https://mh-nexus.de/en/hxd/) and selecting all the code, Edit -> Copy As -> C. This can simply be pasted into the `rawdata.h` header file overwriting the current value.

Then build the project by selecting Build -> Build Solution (Ensure configuration is set to Release).

The resulting Dll from the `printmon` project will need to be copied to the remote system. It's not required to be placed in `C:\Windows\System32` but will blend in more, just use a name not in use (Such as the default `printmon.dll`).

From basic testing, the registry key name and Dll name seem to be arbitary. Due to this, the C# assembly allows specifying the name of each. 

You can then set the required registry using the resulting .Net assembly from the PortMonitorPersist project. The usage is as follows:

```
Create Port Monitor persistence with default values (Reg:"Microsoft Shared Print Monitor", Driver:"printmon.dll"):
    PortMonitorPersist.exe create
Create Port Monitor persistence with specified registry key name and driver Dll name:
    PortMonitorPersist.exe create "Microsoft Shared Print Monitor" "printmon.dll"
Delete Port Monitor persistence default registry key:
    PortMonitorPersist.exe clear
Delete specified Port Monitor persistence registry key:
    PortMonitorPersist.exe clear "Microsoft Shared Print Monitor"
```

## Resources
- [ESET](https://blog.eset.ie/2019/11/21/registers-as-default-print-monitor-but-is-a-malicious-downloader-meet-deprimon-a/)
- [MITRE](https://attack.mitre.org/techniques/T1013/)
