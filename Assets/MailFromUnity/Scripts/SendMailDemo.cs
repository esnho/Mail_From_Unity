using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class SendMailDemo : MonoBehaviour {

    public InputField From;
    public InputField To;
    public InputField Name;
    public InputField Subject;
    public InputField Message;
    
    public string AttachmentFilename = "unity-logo.png";
    
    public void SendMailWithAttachment()
    {
        MailSingleton.Instance.SendMailWithAttachment(
            From.text, 
            Name.text, 
            To.text, 
            Subject.text, 
            Message.text, 
            Application.streamingAssetsPath + "/" + AttachmentFilename
        );
    }

    public void SendPlainMail()
    {
        MailSingleton.Instance.SendPlainMail(
            From.text, 
            Name.text, 
            To.text, 
            Subject.text, 
            Message.text
        );
    }

}
