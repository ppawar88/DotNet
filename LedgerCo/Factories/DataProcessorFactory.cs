using LedgerCo.Repository;
using LedgerCo.Util;

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
                    dataRepository = InMemoryDataSource.Instance;
                    break;
                //Can add more data repository such as SQL or Oracle
            }

            return dataRepository;
        }
    }
}