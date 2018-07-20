using System;

namespace Elysium.Data.Entities
{
    public interface IEntity<TKey> : IEntity where TKey : IEquatable<TKey>
    {
        new TKey Id { get; }
    }

    public interface IEntity
    {
        object Id { get; set; }

        DateTimeOffset? CreatedAt { get; set; }

        DateTimeOffset? UpdatedAt { get; set; }

        string Version { get; set; }

        bool Deleted { get; set; }
    }
}