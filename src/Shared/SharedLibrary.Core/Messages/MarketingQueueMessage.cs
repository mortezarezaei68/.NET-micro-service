namespace SharedLibrary.Core.Messages;

public class MarketingQueueMessage
{
    public string PhoneNumber { get; set; }

    public string SmsText { get; set; }

    public MessagePriorityEnum MessagePriority { get; set; }
}