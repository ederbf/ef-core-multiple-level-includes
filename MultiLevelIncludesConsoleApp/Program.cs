using SpecificationPatternConsoleApp.Database.Repositories;
using SpecificationPatternConsoleApp.Database.Entites;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddTransient<IProjectsRepository, ProjectsRepository>()
    .BuildServiceProvider();

var projectsRepository = serviceProvider.GetService<IProjectsRepository>();


//Without includes we get projects, but no Files nor Images
var projectsNoIncludes = await projectsRepository.GetAllAsync();

Console.WriteLine($"There are {projectsNoIncludes.Count} projects");
foreach(var project in projectsNoIncludes)
{
    Console.WriteLine($"Project {project.Headline} has {(project.Files != null ? project.Files.Count : 0)} files and {(project.Images != null ? project.Images.Count : 0)} images");
}
Console.WriteLine("-----------------------------------------------");

//With direct includes we get Images and Files, but not their hashtags
var includes = new Expression<Func<ProjectEntity, object>>[2];
//simpleIncludes[0] = p => p.Images.Hashtags; --> This does not compile work
//simpleIncludes[0] = p => p.Images.Select(x => x.Hashtags); --> This works in old EF, in EF Core it compiles but crashes on runtime
includes[0] = p => p.Images.Select(x => x.Hashtags);
includes[1] = p => p.Files;

var projectsWithIncludes = await projectsRepository.GetAllAsync(includes);
Console.WriteLine($"There are {projectsWithIncludes.Count} projects");
foreach(var project in projectsWithIncludes)
{
    Console.WriteLine($"Project {project.Headline} has {project.Files.Count} files and {project.Images.Count} images.");
    if (project.Images != null && project.Images.Any() && project.Images.First().Hashtags != null && project.Images.First().Hashtags.Any())
    {
        Console.WriteLine($"The first image has hashtags: {(string.Join(", ", project.Images.First().Hashtags.Select(x => x.Name)))}");
    }
}