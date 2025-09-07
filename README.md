
Create docker hub repository -publish

```
docker build -t angular_p22 . 
docker ps -a
docker images

docker run -d --restart=always --name angular-p22-container -p 4098:8080 angular_p22
docker stop angular-p22-container
docker rm angular-p22-container

docker login

docker tag angular_p22:latest 0taras0/angular-p22-api:0.1
docker push 0taras0/angular-p22-api:0.1

docker pull 0taras0/angular-p22-api:0.1
docker ps -a
docker run -d --restart=always --name angular-p22-container -p 4098:8080 0taras0/angular-p22-api:0.1
```