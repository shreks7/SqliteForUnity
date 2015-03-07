namespace SqliteForUnity3D
{
    public class ConnectionFactory : ISQLiteConnectionFactory
    {

        public ISQLiteConnection Create(string address)
        {
            return new SQLiteConnection(address, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        }

    }
}
