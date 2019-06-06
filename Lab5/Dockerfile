#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1809 AS build
WORKDIR /src
COPY ["Lab5a/Lab5a.csproj", "Lab5a/"]
RUN dotnet restore "Lab5a/Lab5a.csproj"
COPY . .
WORKDIR "/src/Lab5a"
RUN dotnet build "Lab5a.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Lab5a.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lab5a.dll"]