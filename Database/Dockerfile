FROM microsoft/dotnet:sdk AS build-env
COPY . /app
WORKDIR /app
RUN dotnet restore 
RUN dotnet publish -c Release -o out 
EXPOSE 80/tcp
RUN chmod +x ./start.sh
CMD /bin/bash ./start.sh
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Database.dll"]