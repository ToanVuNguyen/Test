using System;
using System.Data;
using System.Configuration;



/// <summary>
/// Summary description for CCRCException
/// </summary>
public class CCRCException : System.Exception
{
    public CCRCException()
    {
    }
    //Write a message for Each error in Error.InnerMessage Logit.
    public CCRCException(string Message)
    {
        this._message = Message;
    }

    private string _message;
    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            value = _message;
        }
    }


}