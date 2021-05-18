#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExamCorrectionBackend/ExamCorrectionBackend.csproj", "ExamCorrectionBackend/"]
RUN dotnet restore "ExamCorrectionBackend/ExamCorrectionBackend.csproj"
COPY . .
WORKDIR "/src/ExamCorrectionBackend"
RUN dotnet build "ExamCorrectionBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExamCorrectionBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExamCorrectionBackend.dll"]