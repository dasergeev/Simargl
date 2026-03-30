rem »мпорт данных с включением режима --multu-geometry (-G)
.\osm2pgsql.exe -d Osm2Russia --create --number-processes 4 -H 10.69.16.239 -P 5432 -U postgres -W --log-progress=true -G .\russia-latest.osm.pbf

rem »порт данных через osmosis (в данный момент данный вариант не используетс€)
.\osmosis.bat --read-pbf file=russia-latest.osm.pbf --log-progress interval=30 --write-pgsql host="10.69.16.239" database="osm" user="postgres" password="!TTCRTdbsa2"

rem под линуксом в Sh
osm2pgsql -d OsmRussiaOthers --create --number-processes 8 -H 10.69.16.239 -P 5432 -U postgres -W --log-progress=true -G -C 73728 merged_osm.pbf