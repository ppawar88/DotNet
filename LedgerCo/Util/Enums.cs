namespace LedgerCo.Util
{
    public enum RequestType
    {
        Loan,
        Payment,
        Balance
    }

    public enum DataSource
    {
        InMemory
        // OutOfMemmory // can be use for any other relational / non relational data source 
    }

    public enum OutputType
    {
        Console
        //Other output options like Write to file.
    }

}
