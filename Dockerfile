FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Asignado en falso para que use la config de mi program.cs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY ["Selection.ProductService.csproj", "Selection.ProductService/"]
RUN dotnet restore "Selection.ProductService/Selection.ProductService.csproj"

COPY . ./Selection.ProductService
WORKDIR "/src/Selection.ProductService"
RUN dotnet build "Selection.ProductService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Selection.ProductService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Selection.ProductService.dll"]