using LogicLbrary1.Models;
using LogicLbrary1.Models.Smtp;
using LogicLbrary1.SmtpHandler1;

namespace TestProject2.SmtpHandlerFacts1;
public class SmtpGmailFacts1
{
    private readonly SmtpGmail1 _sut1;

    public SmtpGmailFacts1()
    {
        _sut1 = new SmtpGmail1();
    }

    [Fact (DisplayName = "SMTP Gmail Test runner")]
    public async Task Run()
    {
        var payload = new SmtpPayload
        {
            Subject = "Test subject",
        };

        var result = await _sut1.SendAsync(payload);

        Assert.True(result);
    }
}
