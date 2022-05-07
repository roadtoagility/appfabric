docker build . -t appfabric -f Dockerfile.appfabric.api

docker build . -t appfabric-ui -f Dockerfile.appfabric.ui

k3d cluster --registry-create mycluster-registry 5001

k3d cluster create mycluster --registry-use k3d-myregister.localhost:5001 --port "8081:80@loadbalancer" --agents 2

k3d cluster stop mycluster

k3d cluster start mycluster

docker tag appfabric:latest myregistry.localhost:5001/appfabric:v0.1

docker push myregistry.localhost:5001/appfabric:v0.1

docker tag appfabric-ui:latest myregistry.localhost:5001/appfabric-ui:v0.1

docker push myregistry.localhost:5001/appfabric-ui:v0.1

kubectl apply -f project/manifest.yaml

kubectl apply -f traefik-ingress.yaml

