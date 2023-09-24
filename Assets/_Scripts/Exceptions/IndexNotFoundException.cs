using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class IndexNotFoundException : Exception
{
    public IndexNotFoundException() { }

    public IndexNotFoundException(string message)
        : base(message) { }

    public IndexNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
