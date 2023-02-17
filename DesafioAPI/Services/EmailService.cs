using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System;
using System.ComponentModel;
using System.Text;

namespace DesafioAPI.Services
{
    public static class EmailService
    {
        static string emailEnvio = "desafioluizalabsjuvenal@gmail.com";
        static string senhaEmail = "gglopxmickkocjmz";
        static string smtpEmail = "smtp.gmail.com";
        static int porta = 587;

        public static async Task EnviarEmailCadastro(string nome, string email)
        {
            MailMessage mail = new()
            {
                From = new MailAddress(emailEnvio, "Desafio LuizaLabs")
            };
            mail.To.Add(new MailAddress(email));

            string textoEmail = "<html><body style='background-color: #fdf9f9; text-align: center;'>" +
            "<img src='https://juvenal.com.br/luizalabs/logoLuizaLabs.png' height='100'><br>" +
            " Olá <strong>"+ nome + "</strong><br>" +
            " Sua conta está quase pronta. Para ativá-la, por favor confirme o seu endereço de email clicando no link abaixo.<br><br>"+
            " <a href='https://localhost/luizalabs/ativarconta-"+Convert.ToBase64String(Encoding.UTF8.GetBytes(email)) +"' target='new'>Confirmar meu email</a><br><br>"+
            " Sua conta não será ativada até que seu email seja confirmado.<br>" +
            " Se você não se cadastrou recentemente, por favor ignore este email.<br><br>" +
            " Atenciosamente,<br>" +
            " LuizaLabs" +
            "</body></html>";

            mail.Subject = "DesafioLuizaLabs - Confirme seu email";
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
        public static async Task EnviarEmailLogin(string codigo)
        {

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
            " <strong>"+ codigo + "</strong><br><br>" +
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
        public static async Task EnviarEmailRecuperarSenha(string email, string codigoRecuperacao)
        {
            MailMessage mail = new()
            {
                From = new MailAddress(emailEnvio, "Desafio LuizaLabs")
            };
            mail.To.Add(new MailAddress(email));

            string textoEmail = "<html><body style='background-color: #fdf9f9; text-align: center;'>" +
            "<img src='https://juvenal.com.br/luizalabs/logoLuizaLabs.png' height='100'><br>" +
            " Olá<br>" +
            " Para alterar sua senha, acesso o link abaixo.<br><br>" +
            " <a href='https://localhost/luizalabs/recuperarSenha-" + email + "' target='new'>Recuperar Senha</a><br><br>" +
            " Se você não solicitou, por favor ignore este email.<br><br>" +
            " Atenciosamente,<br>" +
            " LuizaLabs" +
            "</body></html>";

            mail.Subject = "DesafioLuizaLabs - Recuperar Senha";
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
