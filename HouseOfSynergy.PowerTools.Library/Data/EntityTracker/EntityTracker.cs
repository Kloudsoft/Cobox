//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using HouseOfSynergy.PowerTools.Library.Interfaces;

//namespace HouseOfSynergy.PowerTools.Library.Data.EntityTracker
//{
//    public class EntityTracker<TEntityState, TEntity, TEntityPrimaryKey>:
//        Dictionary<TEntityPrimaryKey, TEntityState>,
//        ICloneable<EntityTracker<TEntityState, TEntity, TEntityPrimaryKey>>,
//        IInitializable
//        where TEntity: class
//        where TEntityPrimaryKey: struct, IFormattable, IComparable
//        where TEntityState: EntityTrackerEntityState<TEntity, TEntityPrimaryKey>
//    {
//        public object SyncRoot { get; private set; }

//        public EntityTracker ()
//        {
//            this.SyncRoot = new object();
//        }

//        public void Initialize ()
//        {
//            lock (this.SyncRoot)
//            {
//                var trace = new StackTrace();

//                if (trace.GetFrame(1).GetMethod().DeclaringType != this.GetType())
//                {
//                    throw (new InvalidOperationException("The EntityTracker.Initialize method should never be called publicly."));
//                }

//                this.Clear();
//            }
//        }

//        public new TEntityState this [TEntityPrimaryKey key]
//        {
//            get
//            {
//                lock (this.SyncRoot)
//                {
//                    return (base [key]);
//                }
//            }
//            private set
//            {
//                lock (this.SyncRoot)
//                {
//                    if (value == null)
//                    {
//                        throw (new ArgumentNullException("Indexer value on EntityTracker (" + typeof(TEntityState).FullName + ") is NULL."));
//                    }

//                    base [key] = value;
//                }
//            }
//        }

//        public new bool Add (TEntityPrimaryKey key, TEntityState item)
//        {
//            bool result = false;

//            lock (this.SyncRoot)
//            {
//                if (item == null)
//                {
//                    throw (new ArgumentNullException("Indexer value on EntityTracker (" + typeof(TEntityState).FullName + ") is NULL."));
//                }

//                if (this.ContainsKey(key))
//                {
//                    TEntityState old = this [key];

//                    if (old.State == EntityState.Added)
//                    {
//                        if (item.State == EntityState.Modified)
//                        {
//                            //this [key] = item;

//                            result = true;
//                        }
//                        else if (item.State == EntityState.Deleted)
//                        {
//                            this [key] = item;

//                            result = true;
//                        }
//                    }
//                    else if (old.State == EntityState.Modified)
//                    {
//                        if (item.State == EntityState.Deleted)
//                        {
//                            this [key] = item;

//                            result = true;
//                        }
//                    }
//                }
//                else
//                {
//                    base.Add(key, item);

//                    result = true;
//                }

//                return (result);
//            }
//        }

//        public new bool Remove (TEntityPrimaryKey key)
//        {
//            lock (this.SyncRoot)
//            {
//                return (base.Remove(key));
//            }
//        }

//        public void RemoveState (EntityState state)
//        {
//        }

//        public new void Clear ()
//        {
//            lock (this.SyncRoot)
//            {
//                base.Clear();
//            }
//        }

//        public EntityTracker<TEntityState, TEntity, TEntityPrimaryKey> Clone ()
//        {
//            lock (this.SyncRoot)
//            {
//                return (new EntityTracker<TEntityState, TEntity, TEntityPrimaryKey>().CopyFrom(this));
//            }
//        }

//        public EntityTracker<TEntityState, TEntity, TEntityPrimaryKey> CopyFrom (EntityTracker<TEntityState, TEntity, TEntityPrimaryKey> source)
//        {
//            lock (this.SyncRoot)
//            {
//                source.CopyTo(this);

//                return (this);
//            }
//        }

//        public EntityTracker<TEntityState, TEntity, TEntityPrimaryKey> CopyTo (EntityTracker<TEntityState, TEntity, TEntityPrimaryKey> destination)
//        {
//            lock (this.SyncRoot)
//            {
//                destination.Clear();

//                foreach (var pair in this)
//                {
//                    destination.Add(pair.Key, pair.Value);
//                }

//                return (destination);
//            }
//        }
//    }
//}