namespace Inbx;

public interface IMessageMail
{
    int Id { get; set; }
    string Email { get; set; }
    string Host { get; set; }
    int Port { get; set; }
}