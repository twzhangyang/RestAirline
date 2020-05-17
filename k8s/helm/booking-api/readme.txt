helm install -f inf.yaml --name=booking-api ./booking-api

helm install -f inf.yaml booking-api ./booking-api --dry-run --debug
helm upgrade -f inf.yaml booking-api ./booking-api --dry-run --debug

kubectl get pods -l app.kubernetes.io/name=booking-api
kubectl run -i --tty load-generator --image=busybox /bin/sh


kubectl port-forward svc/booking-api 7000:80