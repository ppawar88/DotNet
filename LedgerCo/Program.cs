namespace LedgerCo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MarketPlace marketPlace = new MarketPlace();
            marketPlace.StartMarketPlace(args);
        }
    }
}