
1. sudo snap install docker
2. docker login
3. docker pull petegoz/locations-api
4. docker run --name locations-api -d -p 80:80 -p 443:443 petegoz/locations-api