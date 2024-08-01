﻿namespace Solar.Heliac.Domain.Common.Interfaces;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; }
    public string? CreatedBy { get; } // TODO: String as userId? (https://github.com/SSWConsulting/Solar.Heliac/issues/76)
    public DateTimeOffset? UpdatedAt { get; }
    public string? UpdatedBy { get; } // TODO: String as userId? (https://github.com/SSWConsulting/Solar.Heliac/issues/76)

    public void SetCreated(DateTimeOffset createdAt, string? createdBy);

    public void SetUpdated(DateTimeOffset updatedAt, string? updatedBy);
}