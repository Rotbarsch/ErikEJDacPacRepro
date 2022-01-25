

dotnet ef dbcontext scaffold `
"ReproDb\ReproDB.sqlproj" ErikEJ.EntityFrameworkCore.SqlServer.Dacpac `
--context MATDBContext `
-o Entities `
--schema mat `
--context-dir . `
--data-annotations `
--force



