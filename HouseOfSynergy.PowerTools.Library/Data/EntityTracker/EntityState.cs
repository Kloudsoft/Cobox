//using System;
//using System.Data;
//using System.Linq;

//namespace HouseOfSynergy.PowerTools.Library.Data.EntityTracker
//{
//    public class EntityTrackerEntityState<TEntity, TEntityPrimaryKey>
//        where TEntity: class
//        where TEntityPrimaryKey: struct, IFormattable, IComparable
//    {
//        public TEntity Entity { get; private set; }
//        public DateTime DateTime { get; private set; }
//        public EntityState State { get; private set; }
//        public TEntityPrimaryKey PrimaryKey { get; private set; }

//        public EntityTrackerEntityState (TEntity entity, TEntityPrimaryKey entityPrimaryKey, EntityState state, DateTime dateTime)
//        {
//            if (entity == null) { throw (new ArgumentNullException("entity")); }

//            switch (state)
//            {
//                case EntityState.Added:
//                case EntityState.Modified:
//                case EntityState.Deleted: { break; }
//                default: { throw (new ArgumentException("The argument [state] cannot have a value of " + state.ToString() + ".", "state")); }
//            }

//            this.Entity = entity;
//            this.PrimaryKey = entityPrimaryKey;
//            this.State = state;
//            this.DateTime = dateTime;
//        }
//    }
//}