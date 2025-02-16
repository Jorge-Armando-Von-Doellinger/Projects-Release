# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos de projeto para o contêiner
COPY ./OmniSphere.SaleProduct.sln ./
COPY "OmniSphere.SaleProduct.Application/*.csproj" ./OmniSphere.SaleProduct.Application/
COPY "OmniSphere.SaleProduct.Core/*.csproj" ./OmniSphere.SaleProduct.Core/
COPY "OmniSphere.SaleProduct.gRpc/*.csproj" ./OmniSphere.SaleProduct.gRpc/
COPY "OmniSphere.SaleProduct.Infrastructure/*.csproj" ./OmniSphere.SaleProduct.Infrastructure/

# Restaurar dependências
RUN dotnet restore OmniSphere.SaleProduct.sln

# Copiar o restante do código
COPY . ./

# Verificar a estrutura de diretórios após a cópia
RUN echo "Listando diretórios após copiar os arquivos" && ls -lR /app

# Publicar o projeto principal com verbosidade detalhada
RUN dotnet publish /app/OmniSphere.SaleProduct.gRpc/OmniSphere.SaleProduct.gRpc.csproj -c Release -o /out --verbosity detailed

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar os binários compilados da etapa de build
COPY --from=build /out .

# Comando padrão para rodar o projeto
CMD ["dotnet", "OmniSphere.SaleProduct.gRpc.dll"]
