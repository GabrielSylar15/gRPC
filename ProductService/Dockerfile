FROM adoptopenjdk/openjdk8
WORKDIR /app
COPY target/*.jar /app
ENV DB_HOST=jdbc:mysql://172.18.0.2:3306/Product
EXPOSE 8081
CMD ["java","-jar", "websocker-0.0.1-SNAPSHOT.jar"]
