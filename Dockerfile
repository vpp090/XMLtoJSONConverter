FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 8080

COPY "XMLtoJSONConverter.sln" "XMLtoJSONConverter.sln"
COPY "Converter.API/Converter.API.csproj" "Converter.API/Converter.API.csproj"
COPY "Converter.Application/Converter.Application.csproj" "Converter.Application/Converter.Application.csproj"
COPY "Converter.Infrastructure/Converter.Infrastructure.csproj" "Converter.Infrastructure/Converter.Infrastructure.csproj"
COPY "Converter.Test/Converter.Test.csproj" "Converter.Test/Converter.Test.csproj"

RUN dotnet restore "XMLtoJSONConverter.sln"

COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Converter.API.dll" ]