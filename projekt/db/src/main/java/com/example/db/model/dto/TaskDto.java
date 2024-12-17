package com.example.db.model.dto;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;

public record TaskDto(
        Long id,
        @NotBlank(message = "Name cannot be blank") String name,
        @NotNull(message = "Done cannot be null") Boolean done) {
}
