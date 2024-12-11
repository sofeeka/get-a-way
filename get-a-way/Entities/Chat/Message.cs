﻿using System.Xml.Serialization;
using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Chat;

[Serializable]
public class Message : IExtent<Message>
{
    private static List<Message> _extent = new List<Message>();

    private static long IdCounter = 0;

    private long _id;
    private string _text;
    private DateTime _timestamp;
    
    [XmlIgnore]
    public Account Sender { get; private set; }

    public long ID
    {
        get => _id;
        set => _id = value;
    }

    public string Text
    {
        get => _text;
        set => _text = ValidateText(value);
    }

    public DateTime Timestamp
    {
        get => _timestamp;
        set
        {
            if (_timestamp != DateTime.MinValue && value != _timestamp)
                throw new InvalidOperationException("Message timestamp can not be changed.");
        }
    }

    public Message()
    {
    }

    public Message(string text)
    {
        ID = ++IdCounter;
        Text = text;
        _timestamp = DateTime.Now;

        AddInstanceToExtent(this);
    }
    
    public Message(string text, Account sender) : this(text)
    {
        Sender = sender ?? throw new ArgumentNullException(nameof(sender), "Sender cannot be null");
        sender.AddMessage(this); //reverse connection
    }

    private string ValidateText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidAttributeException("Text of a message cannot be empty.");

        if (value.Length > 10000)
            throw new InvalidAttributeException("Message text can not exceed 10000 characters");

        return value;
    }

    private DateTime ValidateTimestamp(DateTime value)
    {
        if (value > DateTime.Now)
            throw new InvalidAttributeException("Message Timestamp cannot be in the future.");
        return value;
    }

    public static List<Message> GetExtentCopy()
    {
        return new List<Message>(_extent);
    }

    public static void AddInstanceToExtent(Message instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        _extent.Add(instance);
    }

    public static void RemoveInstanceFromExtent(Message instance)
    {
        _extent.Remove(instance);
    }

    public static List<Message> GetExtent()
    {
        return _extent;
    }

    public static void ResetExtent()
    {
        _extent.Clear();
        IdCounter = 0;
    }

    public override string ToString()
    {
        return $"Message ID: {ID}\n" +
               $"Text: {Text}\n" +
               $"Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}\n";
    }
}
