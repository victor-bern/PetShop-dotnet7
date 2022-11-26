
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-image

WORKDIR /home/app/PetShop

COPY /*.sln ./
COPY /*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

COPY . .


RUN dotnet publish ./PetShop/PetShop.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:7.0	

COPY --from=build-image /publish .


COPY ./wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh
