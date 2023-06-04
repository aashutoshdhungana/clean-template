using System.Runtime.Serialization;

namespace CleanTemplate.Core.Exceptions
{
    [Serializable]
    public class InvalidDataException : Exception
    {
        protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidDataException(string message) : base(message)
        {
        }
    }
}
