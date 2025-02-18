namespace WTE.TintTrack.Business.DataImporter;

[AttributeUsage(AttributeTargets.Property)]
public class CsvColumnAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}