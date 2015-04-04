param(
	[string] $Configuration = $null
)

Write-Host "** Starting nGratis.Cop.Core PRE-BUILD script..."
Echo "**   [PARAM] Configuration = $($Configuration)"

$CurrentFolderPath = "$([System.IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path))\"

if ($Configuration -eq "Release")
{
	$T4ExeFilePath = [System.Environment]::ExpandEnvironmentVariables("%CommonProgramFiles(x86)%\Microsoft Shared\TextTemplating\12.0\TextTransform.exe")

	if (-not (Test-Path $T4ExeFilePath))
	{
		throw "T4 Transform executable file is not found"
	}

	Write-Host "**   Transforming global assembly info template..."
	$Version = "0.1.0.0"
	& $T4ExeFilePath "$($CurrentFolderPath)..\..\nGratis.Cop.Theia.Common\GlobalAssemblyInfo.tt" -a !!Version!$Version
}

Write-Host "** Finishing nGratis.Cop.Core PRE-BUILD script..."