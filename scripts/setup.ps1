$root = Split-Path $PSScriptRoot -Parent

Set-Location $root

dotnet new tool-manifest --force
dotnet tool install Husky

git clone https://gist.github.com/henry-js/3692fafba401f846db9e39f025506227

$proj = Get-ChildItem src | Select-Object -First 1

if ($null -eq $proj) { exit; }

dotnet husky attach $proj
