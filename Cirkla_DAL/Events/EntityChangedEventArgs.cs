using Cirkla_DAL.Models;

namespace Cirkla_DAL.Events;

public class EntityChangedEventArgs : EventArgs
{
    public Contract Entity { get; }

    public EntityChangedEventArgs(Contract entity)
    {
        Entity = entity;
    }
}