FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BillingAPI.csproj", "./"]
RUN dotnet restore "BillingAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BillingAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BillingAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "BillingAPI.dll"]
