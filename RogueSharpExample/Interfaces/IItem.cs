namespace RogueSharpExample.Interfaces
{
    public interface IItem
    {
        string Name { get; set; }
        int RemainingUses { get; set; }
        int Value { get; set; }

        bool Use();
    }
}
