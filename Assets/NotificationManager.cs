using UnityEngine;
using System;
using Unity.Notifications.Android;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Manager { get; private set; }
    string channel_id = "channel_id";
    List<int> notifs = new List<int>();
    //AndroidNotificationChannel notif_channel;

    // Start is called before the first frame update
    void Awake()
    {
        Manager = this;
        var notif_channel= new AndroidNotificationChannel()
        {
            Id = channel_id,
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notif_channel);
    }

    public void SendDelayedNotif(string title, string text, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = fireTime;

        int id = AndroidNotificationCenter.SendNotification(notification, channel_id);
        notifs.Add(id);
    }


    public void SendSimpleNotif()
    {
        var notification = new AndroidNotification();
        notification.Title = "SomeTitle";
        notification.Text = "SomeText";
        notification.FireTime = DateTime.Now.AddSeconds(10);

        int id = AndroidNotificationCenter.SendNotification(notification, channel_id);
        notifs.Add(id);
    }

    public void CancelAllNotifications()
    {
        foreach (int i in notifs)
        {
            NotificationStatus status = AndroidNotificationCenter.CheckScheduledNotificationStatus(i);
            switch (status)
            {
                case NotificationStatus.Scheduled:
                    AndroidNotificationCenter.CancelNotification(i);
                    break;
                case NotificationStatus.Delivered:
                    AndroidNotificationCenter.CancelNotification(i);
                    break;
                case NotificationStatus.Unknown:
                    AndroidNotificationCenter.CancelNotification(i);
                    break;
                default:
                    break;
            }
        }
    }
}
