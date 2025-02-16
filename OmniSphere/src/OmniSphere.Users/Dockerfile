# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos de projeto para o contêiner
COPY ./OmniSphere.Users.sln ./
COPY "OmniSphere.Users.Application/*.csproj" ./OmniSphere.Users.Application/
COPY "OmniSphere.Users.Core/*.csproj" ./OmniSphere.Users.Core/
COPY "OmniSphere.Users.Grpc/*.csproj" ./OmniSphere.Users.Grpc/
COPY "OmniSphere.Users.Infrastructure/*.csproj" ./OmniSphere.Users.Infrastructure/

# Restaurar dependências
RUN dotnet restore OmniSphere.Users.sln

# Copiar o restante do código
COPY . ./

# Verificar a estrutura de diretórios após a cópia
RUN echo "Listando diretórios após copiar os arquivos" && ls -lR /app

# Publicar o projeto principal com verbosidade detalhada
RUN dotnet publish /app/OmniSphere.Users.Grpc/OmniSphere.Users.Grpc.csproj -c Release -o /out --verbosity detailed

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

EXPOSE 50054
ENV ASPNETCORE_URLS=http://+:50054

# Copiar os binários compilados da etapa de build
COPY --from=build /out .

# Comando padrão para rodar o projeto
CMD ["dotnet", "OmniSphere.Users.Grpc.dll"]
