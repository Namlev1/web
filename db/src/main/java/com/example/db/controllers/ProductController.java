package com.example.db.controllers;

import com.example.db.model.Product;
import com.example.db.service.ProductService;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/api/product")
@RequiredArgsConstructor
public class ProductController {
    private final ProductService productService;
    
    @GetMapping("/all")
    public List<Product> all() {
        return productService.getAllProducts();
    }
    
    @PostMapping("/sample")
    void save(){
        productService.addSampleProducts();
    }
}
