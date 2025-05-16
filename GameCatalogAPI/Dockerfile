#build Docker slike na osnovu instrukcija u vašem Dockerfile-u.
#-t Dodaje tag vašoj izgrađenoj slici. gamecatalogapi je ime slike, a dev je tag 
#(kao što su verzije ili labels). Ovo vam pomaže da lako identifikujete i referencirate sliku.
#. znači "koristi trenutni direktorijum kao izvor fajlova za build"

#docker build -t gamecatalogapi:dev .

#kreiranje i pokretanje novog Docker kontejnera iz navedene Docker slike.
#--rm: Ovaj flag osigurava da se kontejner automatski ukloni kada se zaustavi ili izađe
#-d (ili --detach): Pokreće kontejner u "detached" modu, što znači da radi u pozadini 
#i ne blokira vaš terminal. Možete nastaviti da koristite terminal za druge komande.
#-p 5000:8080 (ili --publish 5000:8080): Ovo je mapiranje portova. 
#Mapira port 5000 na vašem host računaru (gde vi pristupate aplikaciji, npr. http://localhost:5000) 
#na port 8080 unutar Docker kontejnera (gde vaša ASP.NET Core aplikacija zapravo sluša).
#--name gcapi: Daje ime kontejneru (gcapi). 
#To olakšava zaustavljanje, uklanjanje ili referenciranje kontejnera kasnije.
#gamecatalogapi:dev: Ovo je ime i tag Docker slike iz koje želite da pokrenete kontejner.

#docker run --rm -d -p 5000:8080 --name gcapi gamecatalogapi:dev

# 1) Base runtime image - PRVI STAGE: BAZA ZA POKRETANJE (Runtime)

#osnovna slika (base image) za POKRETANJE KOMPAJLIRANE vaše aplikacije. Microsoftova Docker slika 
#koja sadrži samo .NET 8.0 runtime (okruženje za izvršavanje), bez SDK-a
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#Postavlja radni direktorijum unutar Docker slike na '/app'.
#Sve naredne instrukcije (kao što je COPY ili ENTRYPOINT) će se izvršavati u njemu
WORKDIR /app
#kontejner GOVORI DA sluša na portu 8080
EXPOSE 8080

# 2) Build + restore

#druga osnovna slika - zvanična Microsoftova Docker slika koja sadrži ceo 
#NET 8.0 SDK, uključujući kompajlere i alate. 
#Ova slika je mnogo veća od aspnet slike i koristi se za IZGRADNJU (Build),kompajliranje, restore paketa
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#Ova instrukcija postavlja radni direktorijum unutar ovog build stage-a na /src
WORKDIR /src
#Ova instrukcija kopira samo fajl projekta (.csproj) iz vašeg lokalnog direktorijuma
#u trenutni radni direktorijum unutar kontejnera (/src). 
#Ovo je trik za optimizaciju Docker keširanja: ako se samo .csproj fajl ne menja, 
#Docker može da kešira rezultat dotnet restore komande i time ubrza ponovne build-ove.
COPY ["GameCatalogAPI.csproj", "./"]
#Ova instrukcija izvršava komandu dotnet restore unutar kontejnera. 
#Ova komanda preuzima sve NuGet pakete koji su potrebni vašem projektu 
#(definisani u .csproj fajlu).
RUN dotnet restore "GameCatalogAPI.csproj"
#kopira sav preostali kod, sve ostale fajlove i foldere iz vašeg lokalnog direktorijuma
#osim onih isključenih u .dockerignore fajlu 
#u radni direktorijum unutar kontejnera (/src).
COPY . .

# 3) Publish

#Ovaj stage koristi prethodni 'build' stage kao svoju osnovu. 
#To znači da ima pristup svim fajlovima i rezultatima koji su kreirani u build stageu
FROM build AS publish 
#ova instrukcija ne kreira novi layer
#kompajlira vašu aplikaciju za produkciju
#-c Release: Označava da se build radi u 'Release' konfiguraciji (optimizovanoj za performanse).
#-o /app/publish: Specifikuje izlazni direktorijum unutar kontejnera gde će se smestiti 
#objavljeni fajlovi (izvršni fajl, DLL-ovi, itd.). 
#Ovaj korak stvara kompaktni set fajlova koji su potrebni za pokretanje aplikacije.
RUN dotnet publish "GameCatalogAPI.csproj" -c Release -o /app/publish

# 4) Final image

#Konačna slika koristi base sliku (onu koja sadrži samo .NET runtime, bez SDK-a). 
#Na taj način, konačna Docker slika je mnogo manja i sigurnija jer ne sadrži razvojne alate.
FROM base AS final 
#ova instrukcija ne kreira novi layer
#Ova instrukcija postavlja radni direktorijum unutar konačne Docker slike na /app.
WORKDIR /app

# copy published app
#Ova instrukcija kopira SAMO objavljene fajlove aplikacije 
#(koji su kreirani u 'publish' stage-u i nalaze se u '/app/publish') 
#u radni direktorijum (/app) konačne slike. Ovo je razlog zašto je konačna slika mala: 
#sadrži samo objavljeni kod aplikacije i .NET runtime.
COPY --from=publish /app/publish .

# tell ASP.NET to bind to container port 80
#ENV ASPNETCORE_URLS=http://+:5000

#Ova instrukcija definiše komandu koja će se izvršiti kada se kontejner pokrene. 
#U ovom slučaju, ona pokreće vašu kompajliranu ASP.NET Core aplikaciju.
ENTRYPOINT ["dotnet", "GameCatalogAPI.dll"]
