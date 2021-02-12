echo "Starting"
appName="my-back-end"
location=.
dotnet restore
rm -rf $location/app
dotnet publish -c release -o $location/app --no-restore --no-self-contained -r linux-x64

echo "Creating container"

docker rm -f $appName-container
docker build . -t $appName-image
docker run --name $appName-container -i -p 8000:8080 -p 5432:5432 $appName-image 