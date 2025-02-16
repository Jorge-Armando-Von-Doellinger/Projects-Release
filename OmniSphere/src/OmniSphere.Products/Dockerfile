# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos de projeto para o contêiner
COPY ./OmniSphere.Products.sln ./
COPY "OmniSphere.Products.Application/*.csproj" ./OmniSphere.Products.Application/
COPY "OmniSphere.Products.Core/*.csproj" ./OmniSphere.Products.Core/
COPY "OmniSphere.Products.Grpc/*.csproj" ./OmniSphere.Products.Grpc/
COPY "OmniSphere.Products.Infrastructure/*.csproj" ./OmniSphere.Products.Infrastructure/

# Restaurar dependências
RUN dotnet restore OmniSphere.Products.sln

# Copiar o restante do código
COPY . ./

# Verificar a estrutura de diretórios após a cópia
RUN echo "Listando diretórios após copiar os arquivos" && ls -lR /app

# Publicar o projeto principal com verbosidade detalhada
RUN dotnet publish /app/OmniSphere.Products.Grpc/OmniSphere.Products.Grpc.csproj -c Release -o /out --verbosity detailed

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar os binários compilados da etapa de build
COPY --from=build /out .

# Comando padrão para rodar o projeto
CMD ["dotnet", "OmniSphere.Products.Grpc.dll"]
