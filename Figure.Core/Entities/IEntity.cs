﻿namespace Figure.Core.Entities;
public interface IEntity {
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
