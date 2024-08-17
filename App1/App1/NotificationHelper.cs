using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using System;

namespace App1
{
    public static class NotificationHelper
    {
        public static void SendTaskNotification(string taskName, DateTimeOffset deadline)
        {
            try
            {
                // Create the toast content using XML with 12-hour time format and AM/PM
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

                // Create the toast notification
                ToastNotification toastNotification = new ToastNotification(toastXml);

                // Schedule the notification 1 hour prior to the deadline
                DateTimeOffset notificationTime = deadline.AddHours(-1);

                // Ensure the notification time is in the future
                if (notificationTime > DateTimeOffset.Now)
                {
                    // Schedule the notification if it's more than 1 hour away
                    ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, notificationTime);
                    ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);
                    Console.WriteLine($"Notification scheduled for {notificationTime}.");
                }
                else
                {
                    // Show the notification immediately if the deadline is within the next hour
                    ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
                    Console.WriteLine("Notification shown immediately.");
                }
            }
            catch (Exception ex)
            {
                // Catch any errors and log them
                Console.WriteLine($"Error sending notification: {ex.Message}");
            }
        }
    }
}
