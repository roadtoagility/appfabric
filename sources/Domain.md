Executar a verificação de cobertura

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./AppFabric.Tests/TestResults/Coverage/
```

Gerar o relatório de cobertura

```bash
dotnet ~/.nuget/packages/reportgenerator/4.8.5/tools/net5.0/ReportGenerator.dll "-reports:./AppFabric.Tests/TestsResults/coverage.cobertura.xml" "-targetdir:./AppFabric.Tests/TestsResults/"
```

