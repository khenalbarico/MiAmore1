using LogicLbrary1.Models.Contracts;
using LogicLbrary1.SmtpHandler1;

namespace LogicLbrary1.Models.Smtp;
public class SmtpPayload : ISmtpContract
{
    public string SenderEmail { get; set; } = "khenalbarico05@gmail.com";
    public string SenderPassword { get; set; } = "vjmy akcv bzhm vmuc";
    public List<string> RecepientEmails { get; set; } = [
        "khenalbarico05@gmail.com","altheajoyaustria31@gmail.com", "gwpcsales03@gmail.com"
        ];
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
