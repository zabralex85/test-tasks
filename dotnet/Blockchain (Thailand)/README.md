# dotnet-bitcoin-api
Sample bitcoin API wrapper over bitcoind, realized on dotnet

Installation

1. Install MiniKube: https://kubernetes.io/docs/tasks/tools/install-minikube/
2. Install Docker: https://docs.docker.com/install/linux/docker-ce/ubuntu/
3. sudo usermod -a -G docker $USER
4. minikube start
5. kubectl create -f bitcoind-server.yml
6. kubectl create -f db/sqlserver.yml
7. 
8. 
9. 
10. minikube dashboard&


Using now: ```https://hub.docker.com/r/freewil/bitcoin-testnet-box/```

ToDo: ```https://hub.docker.com/r/ruimarinho/bitcoin-core/ ```

Info: 
```https://en.bitcoin.it/wiki/API_reference_(JSON-RPC)#.NET_.28C.23.29```
```https://en.bitcoin.it/wiki/Original_Bitcoin_client/API_calls_list```
```https://github.com/ladimolnar/BitcoinDatabaseGenerator/blob/master/Sources/BitcoinDataLayerAdoNet/Schema/Tables.sql```
```https://github.com/cryptean/bitcoinlib```
```https://medium.com/@benjaminsky/blockchain-by-example-in-sql-server-8376b410128```
```https://github.com/MetacoSA/NBitcoin```


Useful commands:
```
kubectl exec -it bitcoin-testnet-box -- /bin/bash
git clone --shallow-since 2014-10-18 https://github.com/freewil/bitcoin-testnet-box 
eval $(minikube docker-env)
docker build -t bitcoin-testnet-box .
kubectl run bitcoin-testnet-box --port=19001 --image=bitcoin-testnet-box:latest --generator=run-pod/v1 --image-pull-policy=Never --command /usr/bin/tail -- -f /dev/null

kubectl get pods
kubectl port-forward sqlserver-d9dcc7b4b-jq76f 1433:1433
kubectl port-forward bitcoin-testnet-box-758b67f4-5sptg 19011:19011
```
