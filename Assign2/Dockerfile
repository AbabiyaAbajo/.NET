#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1809 AS build
WORKDIR /src
COPY ["assignment2Net/assignment2Net.csproj", "assignment2Net/"]
RUN dotnet restore "assignment2Net/assignment2Net.csproj"
COPY . .
WORKDIR "/src/assignment2Net"
RUN dotnet build "assignment2Net.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "assignment2Net.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "assignment2Net.dll"]