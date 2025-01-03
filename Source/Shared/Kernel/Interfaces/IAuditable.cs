﻿namespace Shared.Kernel.Interfaces
{
    public interface IAuditable
    {
        public Guid UserId { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset LastUpdatedAt { get; set; }
    }
}
