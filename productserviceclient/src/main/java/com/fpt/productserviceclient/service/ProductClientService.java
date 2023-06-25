package com.fpt.productserviceclient.service;

import org.springframework.stereotype.Service;

import net.devh.boot.grpc.client.inject.GrpcClient;
import net.devh.boot.grpc.common.codec.GrpcCodec;

@Service
public class ProductClientService {
	@GrpcClient("grpc-devprolems-service")s
	
}
