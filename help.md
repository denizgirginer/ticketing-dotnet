-- mongo console bağlanma
kubectl exec -it [mongo-pod-name] mongo

-- create secret
kubectl create secret generic dev-db-secret \
  --from-literal=username=test \
  --from-literal=password=blabla

-- expose node port
kubectl expose deployment hello-world --type=NodePort --name=example-service

pv
pvc-d33c8d4b-3786-4a15-b594-c553ee4cca0b

-
0553205e-298c-4ff2-bdc2-f6d0996543da