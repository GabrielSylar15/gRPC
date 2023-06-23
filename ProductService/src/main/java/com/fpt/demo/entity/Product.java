package com.fpt.demo.entity;

import java.io.Serializable;

import javax.persistence.Entity;
import javax.persistence.Table;
import javax.persistence.Column;
import javax.persistence.Id;

@Entity
@Table(name="Product")
public class Product implements Serializable{
	@Id
	@Column(name="ProductId")
	private Integer productId;
	@Column(name="ProductName")
	private String productName;
	
	public Product() {
	}
	public Integer getProductId() {
		return productId;
	}
	public void setProductId(Integer productId) {
		this.productId = productId;
	}
	public String getProductName() {
		return productName;
	}
	public void setProductName(String productName) {
		this.productName = productName;
	}
}
