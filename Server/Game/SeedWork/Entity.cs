using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Game.SeedWork
{
    public abstract class Entity
    {
        public abstract long Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return obj is Entity entity &&
                   Id == entity.Id;
        }

        public void ClearEvents()
            => gameEvents.Clear();

        public IReadOnlyCollection<INotification> GameEvents 
            => gameEvents;

        public static bool operator ==(Entity left, Entity right)
            => left.Equals(right);
        public static bool operator !=(Entity left, Entity right)
            => !(left == right);

        protected void AddGameEvent(INotification domainEvent)
        {
            if (gameEvents == null)
                gameEvents = new List<INotification>();

            gameEvents.Add(domainEvent);
        }
        
        private List<INotification> gameEvents;
    }
}
