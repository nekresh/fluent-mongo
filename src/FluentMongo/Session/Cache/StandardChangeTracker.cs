using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.DefaultSerializer;
using MongoDB.Bson;

namespace FluentMongo.Session.Cache
{
    internal class StandardChangeTracker : IChangeTracker
    {
        private Dictionary<object, StandardTrackedObject> _items;

        public StandardChangeTracker()
        {
            _items = new Dictionary<object, StandardTrackedObject>();
        }

        public void AcceptChanges()
        {
            var list = _items.Values.ToList();
            foreach (var obj in list)
                obj.AcceptChanges();
        }

        public ITrackedObject GetTrackedObject(object obj)
        {
            StandardTrackedObject trackedObject;
            if (!_items.TryGetValue(obj, out trackedObject))
                return null;

            return trackedObject;
        }

        public bool IsTracked(object obj)
        {
            return _items.ContainsKey(obj);
        }

        public void StopTracking(object obj)
        {
            _items.Remove(obj);
        }

        public ITrackedObject Track(BsonClassMap classMap, object obj)
        {
            var trackedObject = (StandardTrackedObject)GetTrackedObject(obj);
            if (trackedObject == null)
            {
                trackedObject = new StandardTrackedObject(classMap, obj, obj.ToBsonDocument());
                _items.Add(obj, trackedObject);
            }

            return trackedObject;
        }

        private class StandardTrackedObject : ITrackedObject
        {
            private State _state;

            public BsonClassMap ClassMap { get; private set; }

            public BsonDocument Original { get; private set; }

            public object Current { get; private set; }

            public bool IsDead
            {
                get { return _state == State.Dead; }
            }

            public bool IsDeleted 
            { 
                get { return _state == State.Deleted; }
            }

            public bool IsNew
            {
                get { return _state == State.New; }
            }

            public bool IsModified
            {
                get { return _state == State.Modified; }
            }

            public bool IsPossiblyModified
            {
                get { return _state == State.PossiblyModified; }
            }

            public StandardTrackedObject(BsonClassMap classMap, object current, BsonDocument original)
            {
                ClassMap = classMap;
                Current = current;
                Original = original;
                _state = State.PossiblyModified;
            }

            public void AcceptChanges()
            {
                if (IsDeleted)
                {
                    ConvertToDead();
                }
                else if (IsNew || IsPossiblyModified)
                {
                    ConvertToUnmodified();
                }
            }

            public void ConvertToDead()
            {
                _state = State.Dead;
            }

            public void ConvertToDeleted()
            {
                _state = State.Deleted;
            }

            public void ConvertToModified()
            {
                _state = State.Modified;
            }

            public void ConvertToNew()
            {
                this.Original = null;
                _state = State.New;
            }

            public void ConvertToPossiblyModified()
            {
                _state = State.PossiblyModified;
            }

            public void ConvertToUnmodified()
            {
                _state = State.PossiblyModified;
                this.Original = Current.ToBsonDocument();
            }

            private enum State
            {
                New,
                Deleted,
                Modified,
                PossiblyModified,
                Dead
            }
        }

    }
}
