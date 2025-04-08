# WattToolkitLauncher
Used to start "Steam++.exe" and perform auxiliary operations

# Effect
1. 

# How to use
1. Copy and overwrite "UISettings.json.save.bak" with the adjusted UI state "UISettings.json"
Note: You may need to find the difference in position before and after the startup form,
and manually save the form information after the deviation

2. Put "WattToolkitLauncher.exe" into the directory where "Steam++.exe" exists
3. Have "WattToolkitLauncher.exe" start instead of "Steam++.exe"

# Notice
For users in different operating systems and language environments,
it may be necessary to replace the last executed "FindWindowEx()" parameter
in "GetNotifyAreaHandle()" and "GetNotifyOverHandle()"

# Disclaimer
WARNING: WattToolkitLauncher may cause file loss and CPU resource waste, but I currently feel good
