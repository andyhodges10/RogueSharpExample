namespace RogueSharpExample.Interfaces
{
    public interface ITreasure
    {
        string Name { get; set; }
        string Description { get; set; }

        bool PickUp(IActor actor);
    }
}
