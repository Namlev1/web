package com.example.db.service;

import com.example.db.model.Category;
import com.example.db.model.Product;
import com.example.db.model.ProductDetails;
import com.example.db.model.Tag;
import com.example.db.repository.CategoryRepository;
import com.example.db.repository.ProductDetailsRepository;
import com.example.db.repository.ProductRepository;
import com.example.db.repository.TagRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.Arrays;
import java.util.List;

@Service
@RequiredArgsConstructor
public class ProductService {
    private final ProductRepository productRepository;
    private final CategoryRepository categoryRepository;
    private final TagRepository tagRepository;
    private final ProductDetailsRepository productDetailsRepository;

    public List<Product> getAllProducts() {
        return productRepository.findAll();
    }

    public void addSampleProducts() {
        Category category1 = Category.builder().name("p1 - category").build();
        Category category2 = Category.builder().name("p2 - category").build();

        ProductDetails details1 = ProductDetails.builder()
                .manufacturer("p1 - manufacturer")
                .warranty("p1 - warranty")
                .build();

        ProductDetails details2 = ProductDetails.builder()
                .manufacturer("p2 - manufacturer")
                .warranty("p2 - warranty")
                .build();

        Tag tag1 = Tag.builder().name("tag1").build();
        Tag tag2 = Tag.builder().name("tag2").build();

        // Save dependent objects first if they are not saved via cascade
        category1 = categoryRepository.save(category1);
        category2 = categoryRepository.save(category2);

        details1 = productDetailsRepository.save(details1);
        details2 = productDetailsRepository.save(details2);

        tag1 = tagRepository.save(tag1);
        tag2 = tagRepository.save(tag2);

        Product product1 = new Product();
        product1.setName("p1");
        product1.setDescription("p1 Description");
        product1.setPrice(15.0);
        product1.setCategory(category1);
        product1.setDetails(details1);
        product1.setTags(Arrays.asList(tag1, tag2));

        Product product2 = new Product();
        product2.setName("p2");
        product2.setDescription("p2 Description");
        product2.setPrice(35.0);
        product2.setCategory(category2);
        product2.setDetails(details2);
        product2.setTags(Arrays.asList(tag1, tag2));

        productRepository.save(product1);
        productRepository.save(product2);
    }
}
