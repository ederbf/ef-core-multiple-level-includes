using System;

namespace SpecificationPatternConsoleApp.Database.Entites
{
    public class ImageEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }    
        public string Filename { get; set; }
        public ProjectEntity Project { get; set; }   
        public List<HashtagEntity> Hashtags { get; set; }
    }
}