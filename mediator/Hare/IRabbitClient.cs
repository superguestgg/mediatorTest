namespace mediator.Hare;

public interface IRabbitClient
{
    void Send(string message);

    string Receive(string queueName);
}