# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos de projeto para o contêiner
COPY ./OmniSphere.Authentication.sln ./
COPY "OmniSphere.Authentication.Application/*.csproj" ./OmniSphere.Authentication.Application/
COPY "OmniSphere.Authentication.Core/*.csproj" ./OmniSphere.Authentication.Core/
COPY "OmniSphere.Authentication.Grpc/*.csproj" ./OmniSphere.Authentication.Grpc/
COPY "OmniSphere.Authentication.Infrastructure/*.csproj" ./OmniSphere.Authentication.Infrastructure/

# Restaurar dependências
RUN dotnet restore OmniSphere.Authentication.sln

# Copiar o restante do código
COPY . ./

# Verificar a estrutura de diretórios após a cópia
RUN echo "Listando diretórios após copiar os arquivos" && ls -lR /app

# Publicar o projeto principal com verbosidade detalhada
RUN dotnet publish /app/OmniSphere.Authentication.Grpc/OmniSphere.Authentication.Grpc.csproj -c Release -o /out --verbosity detailed

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar os binários compilados da etapa de build
COPY --from=build /out .

# Comando padrão para rodar o projeto
CMD ["dotnet", "OmniSphere.Authentication.Grpc.dll"]
