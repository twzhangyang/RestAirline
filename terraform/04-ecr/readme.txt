docker build --build-arg Version=1 -f ./src/Booking/RestAirline.Booking.Api/Dockerfile -t booking:v1 ./src

aws ecr get-login-password --region ap-east-1 | docker login --username AWS --password-stdin 332679337602.dkr.ecr.ap-east-1.amazonaws.com

docker tag booking:v1 332679337602.dkr.ecr.ap-east-1.amazonaws.com/booking:v1

docker push 332679337602.dkr.ecr.ap-east-1.amazonaws.com/booking:v1
