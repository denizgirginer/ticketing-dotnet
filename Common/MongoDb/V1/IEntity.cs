using System;

namespace Ticket.Common.MongoDb.V1 {
    public interface IEntity
    {
    }

    public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
    {
        public TKey id { get; }
        DateTime createdAt { get; set; }
        int version { get; set; }
    }
}