FROM ubuntu
FROM microsoft/dotnet:2.1-sdk


COPY . ./
WORKDIR src
RUN dotnet restore

RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/AppsflyerTwitter.dll"]
