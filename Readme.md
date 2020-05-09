## Movie Version 4 for DotnetCore ef

### 迁移
```
# migration database
Add-Migration m_init_2020 -OutputDir DbMigration\Movie -Project M.Repository -StartupProject M.ServiceAPI -Context MovieDBContext

# update database
Update-Database m_init_2020 -StartupProject M.ServiceAPI -Project M.ServiceAPI -Context MovieDBContext
```

### 反向工程
```
Usage: dotnet ef dbcontext scaffold [arguments] [options]
Options:
  -d|--data-annotations                  Use attributes to configure the model (where possible). If omitted, only the fluent API is used.
  -c|--context <NAME>                    The name of the DbContext.
  --context-dir <PATH>                   The directory to put DbContext file in. Paths are relative to the project directory.
  -f|--force                             Overwrite existing files.
  -o|--output-dir <PATH>                 The directory to put files in. Paths are relative to the project directory.
  --schema <SCHEMA_NAME>...              The schemas of tables to generate entity types for.
  -t|--table <TABLE_NAME>...             The tables to generate entity types for.
  --use-database-names                   Use table and column names directly from the database.
  --json                                 Show JSON output.
  -p|--project <PROJECT>                 The project to use.
  -s|--startup-project <PROJECT>         The startup project to use.
  --framework <FRAMEWORK>                The target framework.
  --configuration <CONFIGURATION>        The configuration to use.
  --runtime <RUNTIME_IDENTIFIER>         The runtime to use.
  --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".
  --no-build                             Don't build the project. Only use this when the build is up-to-date.
  -h|--help                              Show help information
  -v|--verbose                           Show verbose output.
  --no-color                             Don't colorize output.
  --prefix-output                        Prefix output with level.
```

### DOME
```
dotnet ef dbcontext scaffold 'Data Source=.;Initial Catalog=Movie;Integrated Security=True' Microsoft.EntityFrameworkCore.SqlServer --context MovieDBContext --context-dir Context --output-dir Entity --startup-project M.ServiceAPI

dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Movie;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -c MovieDBContext --context-dir Context -o Entity -s M.ServiceAPI -p M.Repository
```