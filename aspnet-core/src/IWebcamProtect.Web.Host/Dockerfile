FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/IWebcamProtect.Web.Host/IWebcamProtect.Web.Host.csproj", "src/IWebcamProtect.Web.Host/"]
COPY ["src/IWebcamProtect.Web.Core/IWebcamProtect.Web.Core.csproj", "src/IWebcamProtect.Web.Core/"]
COPY ["src/IWebcamProtect.Application/IWebcamProtect.Application.csproj", "src/IWebcamProtect.Application/"]
COPY ["src/IWebcamProtect.Core/IWebcamProtect.Core.csproj", "src/IWebcamProtect.Core/"]
COPY ["src/IWebcamProtect.EntityFrameworkCore/IWebcamProtect.EntityFrameworkCore.csproj", "src/IWebcamProtect.EntityFrameworkCore/"]
WORKDIR "/src/src/IWebcamProtect.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/IWebcamProtect.Web.Host", "src/IWebcamProtect.Web.Host"]
COPY ["src/IWebcamProtect.Web.Core", "src/IWebcamProtect.Web.Core"]
COPY ["src/IWebcamProtect.Application", "src/IWebcamProtect.Application"]
COPY ["src/IWebcamProtect.Core", "src/IWebcamProtect.Core"]
COPY ["src/IWebcamProtect.EntityFrameworkCore", "src/IWebcamProtect.EntityFrameworkCore"]
WORKDIR "/src/src/IWebcamProtect.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "IWebcamProtect.Web.Host.dll"]
