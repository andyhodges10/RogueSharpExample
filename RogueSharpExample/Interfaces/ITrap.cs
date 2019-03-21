namespace RogueSharpExample.Interfaces
{
    public interface ITrap
    {
        string Name { get; set; }

        bool Triggered();
    }
}
