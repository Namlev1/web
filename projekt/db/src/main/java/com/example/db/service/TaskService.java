package com.example.db.service;

import com.example.db.model.Task;
import com.example.db.model.converters.TaskConverter;
import com.example.db.model.dto.TaskDto;
import com.example.db.repository.TaskRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.NoSuchElementException;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class TaskService {
    private final TaskRepository taskRepository;

    public TaskDto save(TaskDto dto) {
        Task task = TaskConverter.toEntity(dto);
        task = taskRepository.save(task);
        return TaskConverter.toDto(task);
    }
    
    public TaskDto update(TaskDto dto) {
        Optional<Task> optionalTask = taskRepository.findById(dto.id());
        if (optionalTask.isEmpty()) {
            throw new NoSuchElementException("Task with id " + dto.id() + " not found");
        }
        
        Task task = TaskConverter.toEntity(dto);
        return TaskConverter.toDto(taskRepository.save(task));
    }

    public List<TaskDto> findAll() {
        return taskRepository.findAll()
                .stream()
                .map(TaskConverter::toDto)
                .collect(Collectors.toList());
    }

    public TaskDto findById(Long id) {
        Optional<Task> category = taskRepository.findById(id);
        return category.map(TaskConverter::toDto).orElseThrow();
    }

    public TaskDto findByName(String name) {
        Optional<Task> category = taskRepository.findByName(name);
        return category.map(TaskConverter::toDto).orElseThrow();
    }

    public void deleteById(Long id) {
        taskRepository.deleteById(id);
    }
}
