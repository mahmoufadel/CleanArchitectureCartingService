using MediatR;

namespace CleanArchitecture.Domain.Common;

public abstract class BaseEvent : INotification
{
    public bool IsPublished { get; set; }
}
