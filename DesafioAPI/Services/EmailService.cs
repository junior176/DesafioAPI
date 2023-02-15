using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System;
using System.ComponentModel;

namespace DesafioAPI.Services
{
    public static class EmailService
    {
        public static async Task EnviarEmailAsync()
        {
            string emailEnvio = "desafioluizalabsjuvenal@gmail.com";
            string senhaEmail = "gglopxmickkocjmz";
            string smtpEmail = "smtp.gmail.com";
            int porta = 587;



            MailMessage mail = new()
            {
                From = new MailAddress(emailEnvio, "Desafio LuizaLabs")
            };
            mail.To.Add(new MailAddress("junior.176@outlook.com"));

            string textoEmail = "<html><body style='background-color: #fdf9f9; text-align: center;'>" +
            "<img src='https://juvenal.com.br/luizalabs/logoLuizaLabs.png' height='100'><br>" +
            " Olá <strong>Juvenal Viana</strong><br>" +
            " Precisamos da sua confirmação para liberar a página que você está tentando acessar.<br>" +
            " Retorne à página e digite o código:<br><br>" +
            " <strong>123456</strong><br><br>" +
            " Atenciosamente,<br>" +
            " LuizaLabs" +
            "</body></html>";

            mail.Subject =  "Seu código de confirmação";
            mail.Body = textoEmail;
            mail.IsBodyHtml = true;


            using (SmtpClient smtp = new(smtpEmail, porta))
            {
                smtp.Credentials = new NetworkCredential(emailEnvio, senhaEmail);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
                mail.Dispose();
            }
        }
    }
}
