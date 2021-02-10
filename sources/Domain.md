

Executar a verificação de cobertura

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./AppFabric.Tests/TestResults/Coverage/
```

Gerar o relatório de cobertura

```bash
dotnet ~/.nuget/packages/reportgenerator/4.8.5/tools/net5.0/ReportGenerator.dll "-reports:./AppFabric.Tests/TestsResults/coverage.cobertura.xml" "-targetdir:./AppFabric.Tests/TestsResults/"
```

###########################################################

Migrate

Criando uma migração após a modificação no contexto

```bash
dotnet ef migrations add InitialCreate  --project ../AppFabric.Persistence/AppFabric.Persistence.csproj --context AppFabricDbContext --output-dir ../AppFabric.Persistence/Migrations
```

Listando as migrações

```bash
dotnet ef migrations list  --project ../AppFabric.Persistence/AppFabric.Persistence.csproj --context AppFabricDbContext
```

Atualiza o banco de dados executando a última migração

```bash
dotnet ef database update  --project ../AppFabric.Persistence/AppFabric.Persistence.csproj --context AppFabricDbContext
```

Rollback última migração

```bash
dotnet ef migrations remove  --project ../AppFabric.Persistence/AppFabric.Persistence.csproj --context AppFabricDbContext
```

Executando uma migração em especial pelo nome

```bash
dotnet ef database update {nome-migração}
```

