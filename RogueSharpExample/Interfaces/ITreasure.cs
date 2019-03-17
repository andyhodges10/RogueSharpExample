namespace RogueSharpExample.Interfaces
{
    public interface ITreasure
    {
        string Name { get; set; }
        string Name2 { get; set; }

        bool PickUp(IActor actor);
    }
}
