namespace Play.Catalog.Service.Entities
{
    public class CatalogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null;

        public string DatabaseName { get; set; } = null;

        public string ItemsCollectionName { get; set; } = null;
    }
}