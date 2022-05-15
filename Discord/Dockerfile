FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY Discord.csproj Discord.csproj
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /output
ENTRYPOINT ["dotnet", "/output/Discord.dll"]