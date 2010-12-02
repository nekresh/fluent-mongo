using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMongo.Linq;
using MongoDB.Bson.DefaultSerializer;
using System.Collections;
using FluentMongo.Session.Tracking;

namespace FluentMongo.Session
{
    internal class CacheableQuery<T> : IQueryable<T>
    {
        private readonly IQueryable<T> _query;
        private readonly IChangeTracker _changeTracker;

        public Type ElementType
        {
            get { return _query.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _query.Provider; }
        }

        public CacheableQuery(IQueryable<T> query, IChangeTracker changeTracker)
        {
            _query = query;
            _changeTracker = changeTracker;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CacheableQueryEnumerator(_query.GetEnumerator(), _changeTracker);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class CacheableQueryEnumerator : IEnumerator<T>
        {
            private readonly IChangeTracker _changeTracker;
            private readonly IEnumerator<T> _wrapped;

            object System.Collections.IEnumerator.Current
            {
                get { return ((IEnumerator)_wrapped).Current; }
            }

            public T Current
            {
                get { return _wrapped.Current; }
            }

            public CacheableQueryEnumerator(IEnumerator<T> wrapped, IChangeTracker changeTracker)
            {
                _wrapped = wrapped;
                _changeTracker = changeTracker;
            }

            public void Dispose()
            {
                _wrapped.Dispose();
            }

            public bool MoveNext()
            {
                var result = _wrapped.MoveNext();
                if (result)
                {

                    _changeTracker.Track(Current);
                }

                return result;
            }

            public void Reset()
            {
                _wrapped.Reset();
            }
        }
    }
}