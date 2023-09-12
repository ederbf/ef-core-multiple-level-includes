using System;

namespace SpecificationPatternConsoleApp.Database.Entites
{
    public class HashtagEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ImageEntity> Images { get; set; }
    }
}