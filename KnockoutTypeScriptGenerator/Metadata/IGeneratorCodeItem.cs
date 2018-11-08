namespace KnockoutTypeScriptGenerator.Metadata
{
    public interface IGeneratorCodeItem
    {
        string Namespace { get; }

        string Name { get; }

        string FullName { get; }
    }
}
