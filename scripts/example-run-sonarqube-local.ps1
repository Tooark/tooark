# Defina as variáveis do projeto e token do SonarQube
$sonarProjectKey = "Tooark"
$sonarHost = "<HOST-SONARQUBE>"
$sonarToken = "<TOKEN-SONARQUBE>"

# Inicie a análise do SonarQube
dotnet sonarscanner begin /k:$sonarProjectKey /d:sonar.host.url=$sonarHost /d:sonar.token=$sonarToken /d:sonar.scanner.scanAll=false /d:sonar.cs.vscoveragexml.reportsPaths="coverage.xml"

# Construa o projeto sem incremental
dotnet build --no-incremental

# Colete a cobertura de código
dotnet-coverage collect "dotnet test --configuration Debug /p:CoverletOutputFormat=opencover /p:CoverletOutput='coverage.xml'" -f xml -o "coverage.xml"

# Finalize a análise do SonarQube
dotnet sonarscanner end /d:sonar.token=$sonarToken
