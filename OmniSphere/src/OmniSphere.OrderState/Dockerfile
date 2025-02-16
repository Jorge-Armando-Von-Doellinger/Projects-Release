# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos de projeto para o contêiner
COPY ./OmniSphere.OrderState.sln ./
COPY "OmniSphere.OrderState.Application/*.csproj" ./OmniSphere.OrderState.Application/
COPY "OmniSphere.OrderState.Core/*.csproj" ./OmniSphere.OrderState.Core/
COPY "OmniSphere.OrderState.Grpc/*.csproj" ./OmniSphere.OrderState.Grpc/
COPY "OmniSphere.OrderState.Infrastructure/*.csproj" ./OmniSphere.OrderState.Infrastructure/

# Restaurar dependências
RUN dotnet restore OmniSphere.OrderState.sln

# Copiar o restante do código
COPY . ./

# Verificar a estrutura de diretórios após a cópia
RUN echo "Listando diretórios após copiar os arquivos" && ls -lR /app

# Publicar o projeto principal com verbosidade detalhada
RUN dotnet publish /app/OmniSphere.OrderState.Grpc/OmniSphere.OrderState.Grpc.csproj -c Release -o /out --verbosity detailed

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar os binários compilados da etapa de build
COPY --from=build /out .

# Comando padrão para rodar o projeto
CMD ["dotnet", "OmniSphere.OrderState.Grpc.dll"]
