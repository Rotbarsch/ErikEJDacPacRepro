$msBuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild"

# Build the dacpac
$dacpacProjectFile = "$PSScriptRoot/ReproDB/ReproDB.sqlproj"
& $msBuildPath $dacpacProjectFile -p:Configuration=Release
$dacpacFile = Resolve-Path "$PSScriptRoot/ReproDB/bin/Release/ReproDB.dacpac"

cd .\ReproDB.Entities

dotnet ef dbcontext scaffold `
"$dacpacFile" ErikEJ.EntityFrameworkCore.SqlServer.Dacpac `
--context MATDBContext `
-o Entities `
--schema mat `
--context-dir . `
--data-annotations `
--schema mat `
--force



