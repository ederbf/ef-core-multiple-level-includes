using SpecificationPatternConsoleApp.Database.Entites;
using System;
using System.Linq;

namespace SpecificationPatternConsoleApp.Database
{
    public static class SampleData
    {
        static object synchlock = new object();
        static volatile bool seeded = false;

        public static void EnsureSeedData(this Context context)
        {
            if (!seeded && context.Projects.Count() == 0)
            {
                lock (synchlock)
                {
                    if (!seeded)
                    {
                        var cars = GenerateCars();

                        context.Projects.AddRange(cars);

                        context.SaveChanges();
                        seeded = true;
                    }
                }
            }
        }

        #region Data
        public static ProjectEntity[] GenerateCars()
        {
            return new ProjectEntity[] {
                new ProjectEntity
                    {
                        Headline = "Project_1",
                        Files = new List<FileEntity>
                        {
                            new FileEntity
                            {   
                                Filename = "project_description.pdf",
                                Description = "The project description"
                            },  
                            new FileEntity
                            {   
                                Filename = "disclaimer.pdf",
                                Description = "The project disclaimer"
                            }
                        },
                        Images = new List<ImageEntity>
                        {
                            new ImageEntity
                            {
                                Filename = "image1.png",
                                Hashtags = new List<HashtagEntity>
                                {
                                    new HashtagEntity { Name = "building" },
                                    new HashtagEntity { Name = "city" },
                                    new HashtagEntity { Name = "skyscraper" },
                                }
                            }
                        }
                    },
                    new ProjectEntity
                    {
                        Headline = "Project_2",
                        Files = new List<FileEntity>
                        {
                            new FileEntity
                            {
                                Filename = "project_description.pdf",
                                Description = "The project description"
                            },
                            new FileEntity
                            {
                                Filename = "nda.pdf",
                                Description = "The project nda"
                            }
                        },
                        Images = new List<ImageEntity>
                        {
                            new ImageEntity
                            {
                                Filename = "image1.png",
                                Hashtags = new List<HashtagEntity>
                                {
                                    new HashtagEntity { Name = "landscape" },
                                    new HashtagEntity { Name = "rural" }
                                }
                            },
                            new ImageEntity
                            {
                                Filename = "image2.png"
                            },
                            new ImageEntity
                            {
                                Filename = "image2.png"
                            }
                        }
                    },
                };
            }
          #endregion
      }
}
