## App Fabric 

## Preparação Ambiente

### Plataformas usadas

- nodejs LTS
  - Angular 10
  - Typescript
  - NGX Admin Template (veja https://github.com/akveo/ngx-admin)
- dotnet core LTS

## Estrutura

![image-20210216161852386](/home/adriano/Projects/roadtoagility/workshop/todoagilityapi/docs/images/project-structure.png)

## Inicialização

### Frontend

1. cd {PROJETO}/sources/AppFabric.UI/src

2. npm install

3. ng serve

### Backend

1. cd {PROJETO}/sources/AppFabric.API
2. dotnet restore
3. dotnet build

### Migrações (opcional - o projeto já vem com a base de dados atualizada)

Criando uma migração após a modificação no contexto

##### Instalar dotnet tools para entity framework

```bash
dotnet tool install --global -ef
```

##### Algumas opções relevantes

Toda migração tem um nome, no caso abaixo foi usado InitialCreate

**--project**:  é o projeto onde os códigos das migrações serão criados

**context**: define qual mapeamento será usado, caso exista apenas um na aplicação, ele não é necessário.

**output-dir**: diretório para os códigos referentes aos scripts de migração gerados.

```bash
dotnet ef migrations add InitialCreate  --project ../AppFabric.Persistence/AppFabric.Persistence.csproj --context AppFabricDbContext --output-dir ../AppFabric.Persistence/Migrations
```

##### Gestão de banco via migrações

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

Gerar o script de migração para um banco vazio

```bash
dotnet ef migrations script {nome-da-migração}
```

Gerar script para migrações específicas

```bash
dotnet ef migrations script {nome-da-migração-1  nome-da-migração-X }
```

## Qualidade

### Rodando Testes

dotnet test

### Métricas

Executar a verificação de cobertura

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./AppFabric.Tests/TestResults/Coverage/
```

Gerar o relatório de cobertura

```bash
dotnet ~/.nuget/packages/reportgenerator/4.8.5/tools/net5.0/ReportGenerator.dll "-reports:./AppFabric.Tests/TestsResults/coverage.cobertura.xml" "-targetdir:./AppFabric.Tests/TestsResults/"
```

## Materiais Recomendados

- nossos vídeos

Referencias:

https://docs.microsoft.com/en-us/ef/core/cli/dotnet

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli