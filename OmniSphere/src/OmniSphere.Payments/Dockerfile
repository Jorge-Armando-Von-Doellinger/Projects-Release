# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos de projeto para o contêiner
COPY ./OmniSphere.Payments.sln ./
COPY "OmniSphere.Payments.Application/*.csproj" ./OmniSphere.Payments.Application/
COPY "OmniSphere.Payments.Core/*.csproj" ./OmniSphere.Payments.Core/
COPY "OmniSphere.Payments.gRpc/*.csproj" ./OmniSphere.Payments.gRpc/
COPY "OmniSphere.Payments.Infrastructure/*.csproj" ./OmniSphere.Payments.Infrastructure/

# Restaurar dependências
RUN dotnet restore OmniSphere.Payments.sln

# Copiar o restante do código
COPY . ./

# Verificar a estrutura de diretórios após a cópia
RUN echo "Listando diretórios após copiar os arquivos" && ls -lR /app

# Publicar o projeto principal com verbosidade detalhada
RUN dotnet publish /app/OmniSphere.Payments.gRpc/OmniSphere.Payments.gRpc.csproj -c Release -o /out --verbosity detailed

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar os binários compilados da etapa de build
COPY --from=build /out .

# Comando padrão para rodar o projeto
CMD ["dotnet", "OmniSphere.Payments.gRpc.dll"]
