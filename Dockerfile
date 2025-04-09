# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo do projeto e restaura as dependências
COPY ["curvela_backend.csproj", "./"]
RUN dotnet restore "curvela_backend.csproj"

# Copia o restante dos arquivos e compila a aplicação
COPY . .
RUN dotnet publish "curvela_backend.csproj" -c Release -o /app/publish

# Etapa 2: Imagem de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta que a aplicação vai escutar
EXPOSE 80
EXPOSE 443

# Inicia a aplicação
ENTRYPOINT ["dotnet", "curvela_backend.dll"]
