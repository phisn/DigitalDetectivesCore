sudo docker pull localhost:5000/server
sudo docker run -p 80:80 --name piserver -d --privileged -v /sys/class/thermal/thermal_zone0:/sys/class/thermal/thermal_zone0 -v /dev:/dev localhost:5000/server
exit 0