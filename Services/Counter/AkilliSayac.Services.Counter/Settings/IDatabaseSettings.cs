namespace AkilliSayac.Services.Counter.Settings
{
    public interface IDatabaseSettings
    {
        public string CounterCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
