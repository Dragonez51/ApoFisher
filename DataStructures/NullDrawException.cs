using System;

public class NullDrawException : Exception
{
    // 1. Default constructor
    public NullDrawException() { }

    // 2. Constructor that accepts a custom message
    public NullDrawException(string message) : base(message) { }

    // 3. Constructor that accepts a custom message and an inner exception
    public NullDrawException(string message, Exception inner) : base(message, inner) { }
}