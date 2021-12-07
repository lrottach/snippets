# Get all users using the Microsoft Graph PowerShell SDK
# Creating a new Hashtable linking each DisplayName with its Id
# Creator: Jeffrey Snover (YouTube: https://www.youtube.com/watch?v=uuiTR8r27Os)

Get-MgUser | % {$id = @{}} {$id.$($_.DisplayName)=$_.id; $_} -ov a | ft DisplayName,Id,*Phon*

# This makes it a lot esier to work with Microsoft Graph in PowerShell
Get-MgUser -UserId $id.'Adele Vance'
