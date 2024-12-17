package com.example.db.model.converters;

import com.example.db.model.Task;
import com.example.db.model.dto.TaskDto;

public class TaskConverter {
    public static Task toEntity(TaskDto taskDto) {
        return Task.builder()
                .name(taskDto.name())
                .id(taskDto.id())
                .done(taskDto.done())
                .build();
    }

    public static TaskDto toDto(Task task) {
        return new TaskDto(task.getId(), task.getName(), task.getDone());
    }
}
