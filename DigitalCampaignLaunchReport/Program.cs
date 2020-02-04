using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Titan.Core.Data;

namespace DigitalCampaignLaunchReport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var repository = new DigitalCampaignDataRepository(new DbMapper(Common.ConnectionStringName));
                DateTime startDate;
                DateTime configDate;
                //see if we will use a config date or todays date by default.
                if (ConfigurationManager.AppSettings.Get("startDate") != null && DateTime.TryParse(ConfigurationManager.AppSettings.Get("startDate"), out configDate))
                {
                    startDate = new DateTime(configDate.Year, configDate.Month, configDate.Day, 0, 0, 0);
                }
                else
                {
                    startDate = DateTime.Now.Date;
                }
                //get the segments
                var segments = repository.GetCampaignSegments(startDate);
                //send emails to the sales pod
                segments.GroupBy(s => s.AccountExecutiveId).ToList().ForEach(sg =>
                 {
                     Common.SendNotificationEmail(sg.ToList(), true);
                 });

                //send emails to the photographers
                segments.GroupBy(s => s.Market).ToList().ForEach(sg =>
                {
                    Common.SendNotificationEmail(sg.ToList(), false);
                });
            }
            catch(Exception ex)
            {
                Common.WriteToEventLog(string.Format("There was an error while running the Digital Campaign Launch Report. Details: {0}", ex.ToString()), EventLogEntryType.Error);
                Common.SendErrorEmail(string.Format("There was an error while running the Digital Campaign Launch Report. Details: {0}", ex.ToString()));
            }
        }
    }
}
