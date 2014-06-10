﻿param (
	[string] $newSiteFolder = (Read-Host -Prompt "Enter path for new site"),
	[string] $branch = (Read-Host -Prompt "Enter branch name"),
	
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $dbFolder = (Read-Host -Prompt "Db storage path"),

	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),
	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version")
)

clear

Write-Host "Cloning site..."
git clone http://git.alicargo.ru/Deploy.git $newSiteFolder -b $branch

Write-Host "Backuping old db..."
.\backup-db.ps1 $server $backupLocation "$mainDbPrefix`_$oldVersion"
.\backup-db.ps1 $server $backupLocation "$filesDbPrefix`_$oldVersion"

Write-Host "Creating new db..."
$mainDbBackup = Get-ChildItem "$mainDbPrefix`_$oldVersion*.bak" -Path $backupLocation | Sort-Object LastAccessTime -Descending | Select-Object -First 1
$filesDbBackup = Get-ChildItem "$filesDbPrefix`_$oldVersion*.bak" -Path $backupLocation | Sort-Object LastAccessTime -Descending | Select-Object -First 1
.\restore-previous.ps1 $server $dbFolder $backupLocation$mainDbBackup $mainDbPrefix $oldVersion $newVersion
.\restore-previous.ps1 $server $dbFolder $backupLocation$filesDbBackup $filesDbPrefix $oldVersion $newVersion

Write-Host "Updating db..."
Sqlcmd -S $server -i ".\update-main-db.sql" -v MainDbName = "$mainDbPrefix`_$newVersion"
Sqlcmd -S $server -i ".\update-files-db.sql" -v FilesDbName = "$filesDbPrefix`_$newVersion"
