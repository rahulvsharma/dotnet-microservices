namespace Play.Catalog.Service.Settings
{
    public class CatalogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null;

        public string DatabaseName { get; set; } = null;

        public string ItemsCollectionName { get; set; } = null;
    }
}