using System.Runtime.Serialization;

namespace CleanTemplate.Core.Exceptions
{
    [Serializable]
    public class ParametersNotSuppliedException : Exception
    {
        protected ParametersNotSuppliedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ParametersNotSuppliedException(string message) : base(message)
        { 
        }
    }
}
