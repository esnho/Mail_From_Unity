using ESN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;

public class MailSingleton : MonoBehaviour {

    public string SMTPAddress = "";
    public int SMTPPort = 25;
    public string From = "";
    public string Password = "";
    public bool EnableSSL = false;
    public bool UseDefaultCredentials = true;

    public static MailSingleton Instance;

    MailSender mailSender;
    
    void OnEnable () {
        if (MailSingleton.Instance != null && MailSingleton.Instance != gameObject.GetComponent<MailSingleton>())
        {
            DestroyImmediate(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
            MailSingleton.Instance = gameObject.GetComponent<MailSingleton>();
        }

        mailSender = new MailSender(From, Password, SMTPAddress, SMTPPort, EnableSSL, UseDefaultCredentials);
    }

    public void SendMailWithAttachment(string from, string fromName, string to, string subject, string body, string attachment)
    {
        mailSender.SendMail(from, fromName, to, subject, body, onAsyncComplete, attachment);
    }

    public void SendPlainMail(string from, string fromName, string to, string subject, string body)
    {
        mailSender.SendMail(from, fromName, to, subject, body, onAsyncComplete);
    }

    void onAsyncComplete(object sender, AsyncCompletedEventArgs completedEventArgs)
    {

        if (completedEventArgs.Error != null)
        {
            Debug.LogError(completedEventArgs.Error.Message);
            Debug.LogWarning("If you are using gmail please setup an application password to use whit this application");
            return;
        }

        if (completedEventArgs.Cancelled)
        {
            Debug.LogWarning("Sending cancelled");
            return;
        }

        Debug.Log("Message sent successfully");

        SmtpClient sndr = (SmtpClient)sender;
        sndr.SendCompleted -= onAsyncComplete;

        // TODO use UnityThread to invoke actions on the main thread
    }
}
