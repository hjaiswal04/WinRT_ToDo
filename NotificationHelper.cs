using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace App1
{
    public static class NotificationHelper
    {
        public static void SendTaskNotification(string taskName, DateTimeOffset deadline)
        {
            // Ensure hh for 12-hour format and tt for AM/PM in the toast notification
            string toastXmlString = $@"
        <toast>
            <visual>
                <binding template='ToastGeneric'>
                    <text>Task Deadline Reminder</text>
                    <text>{taskName} is due at {deadline:hh:mm tt} on {deadline:MMM dd, yyyy}</text>
                </binding>
            </visual>
            <actions>
                <action content='Dismiss' arguments='dismiss' />
            </actions>
        </toast>";

            XmlDocument toastXml = new XmlDocument();
            toastXml.LoadXml(toastXmlString);

            ToastNotification toastNotification = new ToastNotification(toastXml);

            // Schedule the notification 1 hour prior to the deadline
            DateTimeOffset notificationTime = deadline.AddHours(-1);
            if (notificationTime > DateTimeOffset.Now)
            {
                ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, notificationTime);
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);
            }
        }
    }
}
