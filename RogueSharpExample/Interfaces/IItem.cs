namespace RogueSharpExample.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        int RemainingUses { get; set; }

        bool Use();
    }
}
