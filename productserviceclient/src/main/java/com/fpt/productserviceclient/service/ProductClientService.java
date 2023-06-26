package com.fpt.productserviceclient.service;

import org.springframework.stereotype.Service;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import net.devh.boot.grpc.client.inject.GrpcClient;
import net.devh.boot.grpc.common.codec.GrpcCodec;

@Service
public class ProductClientService {
	public void getResponse() {
		 ManagedChannel channel = ManagedChannelBuilder.forAddress("localhost", 8080)
		          .usePlaintext()
		          .build();

//		        HelloServiceGrpc.HelloServiceBlockingStub stub 
//		          = HelloServiceGrpc.newBlockingStub(channel);
//
//		        HelloResponse helloResponse = stub.hello(HelloRequest.newBuilder()
//		          .setFirstName("Baeldung")
//		          .setLastName("gRPC")
//		          .build());

		        channel.shutdown();
	}
}
