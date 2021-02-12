FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

RUN mkdir -p /usr/src/app
COPY app/ /usr/src/app

WORKDIR /usr/src/app

EXPOSE 8080

ENTRYPOINT [ "dotnet", "MyBackEnd.dll" ]