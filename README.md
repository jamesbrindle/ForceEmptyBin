# ForceEmptyBin

A simple app for emptying the recycling, specifically for 'sticky' deleted objects.

You tend tend to get 'sticky' deleted objects (those that don't delete when emptying the bin) on SharePoint syncrhonised folders in OneDrive. When a user deletes a file or folder in SharePoint, and you sync your folder to the same SharePoint location, that file or folder ends up in the recyling bin but it seems you can't delete it.

The solution is to simply open a command prompt as adminstrator and run: "rd /s /q {driveLetter}:\\$Recycle.bin"

This app does just this, by iterating over your drives and running this delete command.

It also uses NirCmd.exe to empty the bin, just to finish it off. This should also 'refresh' the Recycle Bin icon.