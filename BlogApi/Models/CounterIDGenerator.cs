using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CounterIDGenerator : IIdGenerator
    {
        private static int _counter = 0;
        public object GenerateId(object container, object document)
        {
            return _counter++;
        }

        public bool IsEmpty(object id)
        {
            return id.Equals(default(int));
        }
    }
}
