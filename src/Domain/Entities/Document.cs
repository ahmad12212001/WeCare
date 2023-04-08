namespace WeCare.Domain.Entities;
public class Document : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public DocumentType DocumentType { get; set; }

    public string Path { get; set; } = null!;

    public int Materiald { get; set; }
    public Material Material { get; set; } = null!;

}
