sudo docker pull localhost:5000/server
sudo docker run --name piserver -d --privileged -v /dev:/dev localhost:5000/server
exit 0