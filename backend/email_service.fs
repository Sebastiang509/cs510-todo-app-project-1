module EmailService

open System.Net.Mail

let sendEmail (subject: string) (body: string) =
    let smtpClient = new SmtpClient("smtp.gmail.com", 587)
    smtpClient.Credentials <- new System.Net.NetworkCredential("your-email@gmail.com", "your-password")
    smtpClient.EnableSsl <- true

    let message = new MailMessage()
    message.From <- MailAddress("your-email@gmail.com")
    message.To.Add("recipient@example.com")
    message.Subject <- subject
    message.Body <- body

    smtpClient.Send(message)
