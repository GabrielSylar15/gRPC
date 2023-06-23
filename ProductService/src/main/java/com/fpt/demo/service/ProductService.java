package com.fpt.demo.service;
import com.fpt.demo.entity.Product;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.fpt.demo.repository.ProductRepository;

@Service
public class ProductService {
	@Autowired
	ProductRepository productRepository;
	
	public List<Product> getListProducts(){
		return productRepository.findAll();
	}

}
