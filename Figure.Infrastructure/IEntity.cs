﻿namespace Figure.Infrastructure;
public interface IEntity {
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
