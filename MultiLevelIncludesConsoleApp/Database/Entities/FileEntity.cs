using System;

namespace SpecificationPatternConsoleApp.Database.Entites
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }    
        public string Filename { get; set; }
        public string Description { get; set; }
        public ProjectEntity Project { get; set; }  
    }
}