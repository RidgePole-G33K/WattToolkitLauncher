# WattToolkitLauncher
Used to start "Steam++.exe" and perform auxiliary operations

# Effect
1. Leave Contents of "UISettings.json" unchanged
2. When closing "Steam++.exe", refresh the System Tray Area

# How to use
1. Copy and overwrite "UISettings.json.save.bak" with the adjusted UI state "UISettings.json"
2. Put "WattToolkitLauncher.exe" into the directory where "Steam++.exe" exists
3. Have "WattToolkitLauncher.exe" start instead of "Steam++.exe"

# Tip
You may need to find the difference in position before and after start, and manually save the form information after the deviation

# Example
When Resolution is 1920x1080 and DPI is 1.0x, the Bound of 60% Full-Screen is (377, 185 647x1150). If DPI is 1.25x, the Bound is (376, 178 538.4x920)

# Note
For users in different operating systems and language environments, it may be necessary to replace the last executed "FindWindowEx()" parameter in "GetNotifyAreaHandle()" and "GetNotifyOverHandle()"

# Disclaimer
WARNING: WattToolkitLauncher may cause file loss and CPU resource waste, but I currently feel good
