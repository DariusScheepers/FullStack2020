docker rm -f $(docker ps -a -q)
docker build -t my-front-end-image .
winpty docker run --name my-front-end-container -it -p 8080:80 my-front-end-image