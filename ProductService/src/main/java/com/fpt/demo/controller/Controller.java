package com.fpt.demo.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;

import com.fpt.demo.service.ProductService;

@org.springframework.stereotype.Controller
public class Controller {
	@Autowired
	ProductService productService;
	
	
	@GetMapping("/index")
	public String index() {
		return "hello";
	}
	
	@GetMapping("/product")
	public String data(Model model) {
		model.addAttribute("list", productService.getListProducts());
		return "product/data";
	}
	
}
