using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalCampaignLaunchReport
{
    class DigitalCampaignSegmentItem
    {
        public string ContractNumber { get; set; }

        public string Advertiser { get; set; }

        public string Program { get; set; }

        public string AccountExecutive { get; set; }

        public string AccountExecutiveId { get; set; }

        public string AccountExecutiveEmail { get; set; }

        public string PodEmails { get; set; }

        public string PhotographerEmails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ActualStartDate { get; set; }

        public string MediaProduct { get; set; }

        public string Market { get; set; }

        public int NumberOfCreative { get; set; }

        public string SegmentStatus { get; set; }

        public string Status { get; set; }

        public string ContractURL { get; set; }

    }
}
