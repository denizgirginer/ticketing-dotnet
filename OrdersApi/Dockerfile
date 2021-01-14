FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
COPY ["OrdersApi.csproj", ""]
RUN dotnet restore "./OrdersApi.csproj"
COPY . .
WORKDIR "/src/."
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT [ "dotnet", "watch", "run", "--no-restore", "--urls", "http://0.0.0.0:5000"]