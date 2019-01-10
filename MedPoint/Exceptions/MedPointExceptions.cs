using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MedPoint.Exceptions
{
    [Serializable]
    public class MedPointException : Exception
    {
        public MedPointException() { }
        public MedPointException(string message) : base(message) { }
        public MedPointException(string message, Exception inner) : base(message, inner) { }
        protected MedPointException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class EntityNotFoundException : MedPointException
    {
        public EntityNotFoundException() { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    
}
