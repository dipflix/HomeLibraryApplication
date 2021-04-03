namespace HomeLibraryData.Models.Base
{
    public interface IEntity
    {
        int Id { get; set; }
        string ToLiteText();
    }
}
