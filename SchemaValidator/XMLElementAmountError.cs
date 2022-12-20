using System;
using System.Runtime.Serialization;

[Serializable]
internal class XMLElementAmountException : Exception
{
    public XMLElementAmountException()
    {
    }

    public XMLElementAmountException(string message) : base(message)
    {
    }

    public XMLElementAmountException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected XMLElementAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}