using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Email;
using Titan.Core.Util;
using System.Diagnostics;

namespace DigitalCampaignLaunchReport
{
    class Common
    {
        private static string TestEmailAddress = ConfigurationManager.AppSettings["test-email-address"];

        #region ConnectionString property
        /// <summary>Encapsulates the connection strings contained in the web.config</summary>
        /// <value>Returns the connection string for the current environment</value>
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["HeliosConnection"].ConnectionString; }
        }
        #endregion

        #region Connection String Name property

        public static string ConnectionStringName
        {
            get
            {
                return "HeliosConnection";
            }
        }
        #endregion

        #region SmtpAddress property
        /// <summary>Encapsulates the connection strings contained in the web.config</summary>
        /// <value>Returns the connection string for the current environment</value>
        public static string SmtpAddress
        {
            get { return ConfigurationManager.AppSettings["titanSmtpAddress"]; }
        }
        #endregion

        #region IsInTestEnvironment property
        public static bool IsInTestEnvironment()
        {
            return Environment.MachineName.IndexOf("WEBPROD") == -1 && Environment.MachineName.IndexOf("SVMP10ATL") == -1 && Environment.MachineName.IndexOf("IT-TE1") == -1;
            //return true;
        }
        #endregion

        #region GetPodEmailText Method
        public static string GetPodEmailText()
        {
            return @"<span style='font-size:12px;font-family:Arial;'>The following media segments went live today and are expected to serve normally.</span>
                    <br/>
                    <br/>
                    <table border='1' cellpadding='2' cellspacing='0' style='width: 1540px;font-size:12px'>
                    <tr>
	                    <th style='width: 80px; background-color:#1ab1ea; color:#fafafa'>Contract #</th>
	                    <th style='width: 120px; background-color:#1ab1ea; color:#fafafa'>Client</th>
	                    <th style='width: 160px; background-color:#1ab1ea; color:#fafafa'>Program</th>
                        <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Market</th>
                        <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Contracted Start Date</th>
                        <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Actual Start Date</th>
                        <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Segment End Date</th>
                        <th style='width: 60px; background-color:#1ab1ea; color:#fafafa'>Segment Status</th>
                        <th style='width: 120px; background-color:#1ab1ea; color:#fafafa'>Media Product</th>
                        <th style='width: 60px; background-color:#1ab1ea; color:#fafafa'>Status</th>
	                    <th style='width: 340px; background-color:#1ab1ea; color:#fafafa'>Contract URL</th>
                    </tr>
	                    #foreach($segment in $model)
                        #even
                         <tr style='background-color:#dddddd'>
		                    <td>$segment.ContractNumber</td>
		                    <td>$segment.Advertiser</td>
		                    <td>$segment.Program</td>
                            <td>$segment.Market</td>
                            <td>$segment.StartDate.ToShortDateString()</td>
                            <td>$segment.ActualStartDate.ToShortDateString()</td>
                            <td>$segment.EndDate.ToShortDateString()</td>
                            <td>$segment.SegmentStatus</td>
                            <td>$segment.MediaProduct</td>
                            <td>$segment.Status</td>
		                    <td>$segment.ContractURL</td>
	                    </tr>
                        #odd
                         <tr style='background-color:#ffffff'>
		                    <td>$segment.ContractNumber</td>
		                    <td>$segment.Advertiser</td>
		                    <td>$segment.Program</td>
                            <td>$segment.Market</td>
                            <td>$segment.StartDate.ToShortDateString()</td>
                            <td>$segment.ActualStartDate.ToShortDateString()</td>
                            <td>$segment.EndDate.ToShortDateString()</td>
                            <td>$segment.SegmentStatus</td>
                            <td>$segment.MediaProduct</td>
                            <td>$segment.Status</td>
		                    <td>$segment.ContractURL</td>
	                    </tr>
	                    #end
                    </table>
                    <br/>
                    <br/>
                    <span style='font-size:12px;font-family:Arial;'>Day-of or late-launch campaigns are not captured in this email, but their status is exposed and checkable in Salesforce in the Production Details (Digital) view.</span>
                    ";
        }
        #endregion

        #region GetPhotographerEmailText Method
        public static string GetPhotographerEmailText()
        {
            return @"<span style='font-size:12px;font-family:Arial;'>The following media segments went live today and are expected to serve normally.</span>
                    <br/>
                    <br/>
                    <table border='1' cellpadding='2' cellspacing='0' style='width: 1540px;font-size:12px'>
                    <tr>
	                    <th style='width: 80px; background-color:#1ab1ea; color:#fafafa'>Contract #</th>
	                    <th style='width: 120px; background-color:#1ab1ea; color:#fafafa'>Client</th>
	                    <th style='width: 160px; background-color:#1ab1ea; color:#fafafa'>Program</th>
                        <th style='width: 80px; background-color:#1ab1ea; color:#fafafa'>Number of Creatives</th>
	                    <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Contracted Start Date</th>
                        <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Actual Start Date</th>
                        <th style='width: 100px; background-color:#1ab1ea; color:#fafafa'>Segment End Date</th>
                        <th style='width: 60px; background-color:#1ab1ea; color:#fafafa'>Segment Status</th>
	                    <th style='width: 120px; background-color:#1ab1ea; color:#fafafa'>Media Product</th>
                        <th style='width: 60px; background-color:#1ab1ea; color:#fafafa'>Status</th>
	                    <th style='width: 340px; background-color:#1ab1ea; color:#fafafa'>Contract URL</th>
                    </tr>
	                    #foreach($segment in $model)
                        #even
                         <tr style='background-color:#dddddd'>
		                    <td>$segment.ContractNumber</td>
		                    <td>$segment.Advertiser</td>
		                    <td>$segment.Program</td>
                            <td>$segment.NumberOfCreative</td>
                            <td>$segment.StartDate.ToShortDateString()</td>
                            <td>$segment.ActualStartDate.ToShortDateString()</td>
                            <td>$segment.EndDate.ToShortDateString()</td>
                            <td>$segment.SegmentStatus</td>
		                    <td>$segment.MediaProduct</td>
                            <td>$segment.Status</td>
		                    <td>$segment.ContractURL</td>
	                    </tr>
                        #odd
                         <tr style='background-color:#ffffff'>
		                    <td>$segment.ContractNumber</td>
		                    <td>$segment.Advertiser</td>
		                    <td>$segment.Program</td>
                            <td>$segment.NumberOfCreative</td>
                            <td>$segment.StartDate.ToShortDateString()</td>
                            <td>$segment.ActualStartDate.ToShortDateString()</td>
                            <td>$segment.EndDate.ToShortDateString()</td>
                            <td>$segment.SegmentStatus</td>
                            <td>$segment.MediaProduct</td>
                            <td>$segment.Status</td>
		                    <td>$segment.ContractURL</td>
	                    </tr>
	                    #end
                    </table>";
        }
        #endregion

        #region SendNotificationEmail method

        public static void SendNotificationEmail(List<DigitalCampaignSegmentItem> segmentItems, bool isPodEmail)
        {

            string subject = string.Format("{0}Digital Campaign Launch Report",(IsInTestEnvironment() ? string.Format("DISREGARD - EMAIL ORIGINATED FROM TEST SYSTEM FOR {0} - ",(isPodEmail ? "SALES POD":"PHOTOGRAPHERS")) : ""));
            var emailTemplate = isPodEmail ? GetPodEmailText() : GetPhotographerEmailText();
            var emailBody = new NVelocityHelper()
                .Add("model", segmentItems)
                .Merge(emailTemplate);

            Message notificationEmail = new Message(SmtpAddress);
            if (IsInTestEnvironment())
            {
                notificationEmail.AddRecipients(TestEmailAddress.Split(','));
            }
            else
            {                
                if (isPodEmail)
                {
                    notificationEmail.AddRecipient(segmentItems[0].AccountExecutiveEmail);
                    if(!string.IsNullOrEmpty(segmentItems[0].PodEmails))
                    {
                        notificationEmail.AddCCs(segmentItems[0].PodEmails.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    
                }
                else
                {
                    notificationEmail.AddRecipients(segmentItems[0].PhotographerEmails.Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries));
                }
                notificationEmail.AddBCCs(new string[] { "apps@intersection.com", "digitaladops@intersection.com" });
            }
            notificationEmail.From = "Apollo@intersection.com";
            notificationEmail.Subject = subject;
            notificationEmail.Body = emailBody;
            notificationEmail.SendEmail();
        }
        #endregion

        #region SendEmailToSupport method
        /// <summary>TBD</summary>
        /// <param name="subject">TBD</param>
        /// <param name="body">TBD</param>
        public static void SendEmailToSupport(string subject, string body)
        {
            Message mail = new Message(ConfigurationManager.AppSettings["titanSmtpAddress"]);
            mail.Subject = subject;
            mail.From = "Apollo@intersection.com";
            mail.Body = body;
            if (IsInTestEnvironment())
            {
                mail.AddRecipients(TestEmailAddress.Split(','));
            }
            else
            {
                mail.AddRecipient("apps@intersection.com");
            }

            mail.SendEmail();
        }
        #endregion

        #region SendErrorEmail method
        /// <summary>TBD</summary>
        /// <param name="errorCode">TBD</param>
        /// <param name="messageBody">TBD</param>
        public static void SendErrorEmail(string messageBody)
        {

            string messageSubject =
                string.Format("{0}An Exception has occurred during the Digital Campaign Launch Report...", (IsInTestEnvironment() ? "DISREGARD - EMAIL ORIGINATED FROM TEST SYSTEM - " : ""));
            SendEmailToSupport(messageSubject, messageBody);
        }
        #endregion

        #region WriteToEventLog method
        /// <summary>TBD</summary>
        /// <param name="message">TBD</param>
        /// <param name="type">TBD</param>
        public static void WriteToEventLog(string message, EventLogEntryType type)
        {
            string source = "DigitalCampaignLaunchReportTask";
            string log = "DigitalCampaignLaunchReportGeneration";
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }
            EventLog.WriteEntry(source, message, type);
        }
        #endregion

    }
}
