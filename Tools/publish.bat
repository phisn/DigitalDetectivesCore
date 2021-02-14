plink -pw %1 -m "%cd%/remove.sh" pi@%2
docker build -f "../Server/Dockerfile" --force-rm -t %2:5000/server "../"
docker push %2:5000/server
plink -pw %1 -m "%cd%/update.sh" pi@%2
