dotnet test Tooark.Tests/Tooark.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/coverage.xml /p:Include="[Tooark.Securities]*" --filter "FullyQualifiedName~Securities"

reportgenerator -reports:Tooark.Tests/TestResults/coverage.xml -targetdir:Tooark.Tests/TestResults/coveragereport -reporttypes:Html
