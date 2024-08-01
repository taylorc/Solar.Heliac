﻿using MediatR;

namespace Solar.Heliac.Domain.Common.Base;

/// <summary>
/// Can be raised by an AggregateRoot to notify subscribers of a domain event.
/// </summary>
public record DomainEvent : INotification;