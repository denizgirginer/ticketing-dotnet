using System;

namespace Ticket.Common.MongoDb.V1 {
    public interface IEntity
    {
    }

    public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        DateTime CreatedAt { get; set; }
        int Version { get; set; }
    }
}