module EmailService

open System.Net.Mail

let sendEmail (subject: string) (body: string) =
    let formattedBody = sprintf "Subject: %s\n\n%s" subject body
    ...
    message.Body <- formattedBody


    let message = new MailMessage()
    message.From <- MailAddress("your-email@gmail.com")
    message.To.Add("recipient@example.com")
    message.Subject <- subject
    message.Body <- body

    smtpClient.Send(message)
