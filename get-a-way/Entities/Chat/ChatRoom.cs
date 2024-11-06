﻿using get_a_way.Entities.Accounts;
using get_a_way.Exceptions;
using get_a_way.Services;

namespace get_a_way.Entities.Chat;

[Serializable]
public class ChatRoom : IExtent<ChatRoom>
{
    public static List<ChatRoom> Extent = new List<ChatRoom>();
    
    private static long IdCounter = 0;
    public long ID { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public List<Account> Accounts { get; set; }

    public ChatRoom()
    {
    }

    public ChatRoom(string name, string photoUrl)
    {
        ID = ++IdCounter;
        Name = name;
        PhotoUrl = photoUrl;
        Accounts = new List<Account>();
    }

    public List<ChatRoom> GetExtentCopy()
    {
        return new List<ChatRoom>(Extent);
    }

    public void AddInstanceToExtent(ChatRoom instance)
    {
        if (instance == null)
            throw new AddingNullInstanceException();
        Extent.Add((instance));
    }

    public void RemoveInstanceFromExtent(ChatRoom instance)
    {
        Extent.Remove(instance);
    }
}