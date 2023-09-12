namespace SpecificationPatternConsoleApp.Database.Entites
{
    public class ProjectEntity
    {
        public Guid Id { get; set; }
        public string Headline { get; set; }
        public List<FileEntity> Files { get; set; }
        public List<ImageEntity> Images { get; set; }
    }
}