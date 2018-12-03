FROM microsoft/dotnet:sdk AS build-env
COPY . /app
WORKDIR /app

RUN dotnet restore 
RUN dotnet publish -c Release -o out 


FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PublicApi.dll"]
EXPOSE 80/tcp