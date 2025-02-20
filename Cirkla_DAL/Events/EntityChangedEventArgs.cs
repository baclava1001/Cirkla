using Cirkla_DAL.Models;

namespace Cirkla_DAL.Events;

// Encapsulates the event arguments for when a Contract entity changes
public class EntityChangedEventArgs : EventArgs
{
    public Contract Entity { get; }

    public EntityChangedEventArgs(Contract entity)
    {
        Entity = entity;
    }
}