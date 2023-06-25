FROM adoptopenjdk/openjdk8
WORKDIR /app
ARG JAR_FILE=target/*.jar
COPY ${JAR_FILE} /app
ENV DB_HOST=jdbc:mysql://172.18.0.2:3306/Product
EXPOSE 8080
CMD ["java","-jar", "websocker-0.0.1-SNAPSHOT.jar"]
