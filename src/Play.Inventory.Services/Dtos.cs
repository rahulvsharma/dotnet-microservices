namespace Play.Inventory.Service.Dtos
{
    public record GrantItemsDto(Guid UserId, Guid CalalogItemId, int Quantity);

    public record InventoryItemDto(Guid CatalogItemId, string name, string description, int Quantity, DateTimeOffset AcquireDate);
    public record CatalogItemDto(Guid Id, string name, string description);
    
}