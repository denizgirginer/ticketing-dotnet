#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["dotnet-demo-api.csproj", ""]
RUN dotnet restore "./dotnet-demo-api.csproj"
COPY . .
WORKDIR "/src/."
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
#RUN dotnet build "dotnet-demo-api.csproj" -c Release -o /app/build
#CMD ["dotnet", "watch", "run", "dotnet-demo-api.csproj"]
ENTRYPOINT [ "dotnet", "watch", "run", "--no-restore", "--urls", "http://0.0.0.0:5000"]

#FROM build AS publish
#RUN dotnet publish "dotnet-demo-api.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "dotnet-demo-api.dll"]