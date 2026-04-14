FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/fgv.ordenacao.livros.api/fgv.ordenacao.livros.api.csproj", "src/fgv.ordenacao.livros.api/"]
COPY ["src/fgv.ordenacao.livros.application/fgv.ordenacao.livros.application.csproj", "src/fgv.ordenacao.livros.application/"]
COPY ["src/fgv.ordenacao.livros.domain/fgv.ordenacao.livros.domain.csproj", "src/fgv.ordenacao.livros.domain/"]
COPY ["src/fgv.ordenacao.livros.infrastructure/fgv.ordenacao.livros.infrastructure.csproj", "src/fgv.ordenacao.livros.infrastructure/"]
RUN dotnet restore "src/fgv.ordenacao.livros.api/fgv.ordenacao.livros.api.csproj"
COPY . .
WORKDIR "/src/src/fgv.ordenacao.livros.api"
RUN dotnet publish "fgv.ordenacao.livros.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "fgv.ordenacao.livros.api.dll"]
