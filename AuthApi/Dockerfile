FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
COPY ["AuthApi.csproj", ""]
RUN dotnet restore "./AuthApi.csproj"
COPY . .
WORKDIR "/src/."
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT [ "dotnet", "watch", "run", "--no-restore", "--urls", "http://0.0.0.0:5000"]
#RUN dotnet build "AuthApi.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "AuthApi.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AuthApi.dll"]