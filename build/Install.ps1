param($installPath, $toolsPath, $package, $project)

$appPluginsFolder = $project.ProjectItems | Where-Object { $_.Name -eq "App_Plugins" }
$themeEngineFolder = $appPluginsFolder.ProjectItems | Where-Object { $_.Name -eq "ThemeEngine" }

if (!$themeEngineFolder)
{
	$newPackageFiles = "$installPath\Content\App_Plugins\ThemeEngine"

	$projFile = Get-Item ($project.FullName)
	$projDirectory = $projFile.DirectoryName
	$projectPath = Join-Path $projDirectory -ChildPath "App_Plugins"
	$projectPathExists = Test-Path $projectPath

	if ($projectPathExists) {	
		Write-Host "Updating Theme Engine App_Plugin files using PS as they have been excluded from the project"
		Copy-Item $newPackageFiles $projectPath -Recurse -Force
	}
}