namespace MazeGame.Networking.Dto;

public class BackpackDto
{
    public int InventoryCapacity { get; set; }
    public int Gold { get; set; }
    public int Money { get; set; }
    public List<ItemDto> Inventory { get; set; } = new();
}
