FROM adoptopenjdk/openjdk8
WORKDIR /app
COPY target/*.jar /app
EXPOSE 8080
CMD ["java","-jar", "websocker-0.0.1-SNAPSHOT.jar"]
