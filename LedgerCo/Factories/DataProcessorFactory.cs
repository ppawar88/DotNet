using LedgerCo.Repository;
using LedgerCo.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.Factories
{
    public class DataProcessorFactory
    {
        public IDataRepository GetDataProcessorInstance(DataSource dataSource)
        {
            IDataRepository dataRepository = null;

            switch (dataSource)
            {
                case DataSource.InMemory:
                    dataRepository = new InMemoryDataSource();
                    break;
                //Can add more data repository such as SQL or Oracle
            }

            return dataRepository;
        }
    }
}