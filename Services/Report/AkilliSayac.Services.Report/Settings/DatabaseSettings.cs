﻿namespace AkilliSayac.Services.Report.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CounterCollectionName { get; set ; }
        public string ReportCollectionName { get ; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
