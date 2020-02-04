using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Titan.Core.Data;
using Titan.DataIO;

namespace DigitalCampaignLaunchReport
{

    class DigitalCampaignDataRepository : DataRepository
    {

        public DigitalCampaignDataRepository(DbMapper dbMapper) : base(dbMapper) { }

        public List<DigitalCampaignSegmentItem> GetCampaignSegments(DateTime startDate)
        {
            List<DigitalCampaignSegmentItem> segmentItems;
            using (IO io = new IO(Common.ConnectionString))
            {
                segmentItems = io.RetrieveEntitiesFromCommand<DigitalCampaignSegmentItem>(IO.CreateCommandFromStoredProc("dbo.Reports_DigitalCampaignLaunch", Param.CreateParam("startDate", SqlDbType.DateTime, startDate)));
            }
            return segmentItems;
        }
    }

    public abstract class DataRepository
    {
        protected readonly DbMapper _dbMapper;

        protected DataRepository(DbMapper dbMapper)
        {
            _dbMapper = dbMapper;
        }

        protected void Execute(bool runInTransaction, Action databaseOperation)
        {
            if (runInTransaction)
            {
                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    databaseOperation();
                    transactionScope.Complete();
                }
            }
            else databaseOperation();
        }
    }
}
