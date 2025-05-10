# Use the official image as a base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MedicalCheckUpASP", "MedicalCheckUpASP/"]
RUN dotnet restore "MedicalCheckUpASP"
COPY . .
WORKDIR "/src/MedicalCheckUpASP"
RUN dotnet build "MedicalCheckUpASP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedicalCheckUpASP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalCheckUpASP.dll"]
