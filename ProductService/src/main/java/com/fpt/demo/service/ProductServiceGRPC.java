package com.fpt.demo.service;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;

import com.devProblems.Empty;
import com.devProblems.Product;
import com.devProblems.ProductCRUDGrpc.ProductCRUDImplBase;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;

@GrpcService
public class ProductServiceGRPC extends ProductCRUDImplBase{
	@Autowired
	ProductService productService;
	
	@Override
	public void selectAll(Empty request, StreamObserver<Product> responseObserver) {
		// TODO Auto-generated method stub
		List<Product> list = productService.getListProducts()
									.stream()
									.map(p -> Product.newBuilder().setProductId(p.getProductId()).setProductName(p.getProductName()).build())
									.collect(Collectors.toList());		
		for (Product product : list) {
			responseObserver.onNext(product);
		}
		responseObserver.onCompleted();
	}
	
}
