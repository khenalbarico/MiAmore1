using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLbrary1.Models.Contracts;
public interface ISmtpContract
{
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
    public List<string> RecepientEmails { get; set; }
}
