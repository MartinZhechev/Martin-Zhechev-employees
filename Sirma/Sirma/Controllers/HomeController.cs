using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Sirma.Models;
using Sirma.Models.Projects;

namespace Sirma.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public string FindCommonProjects([FromForm] IFormFile input)
    {
        var projectData = ReadInputData(input);
        return FindLongestWorkingPairs(projectData);
    }

    private static List<ProjectData> ReadInputData(IFormFile file)
    {
        var projectDataList = new List<ProjectData>();

        using (var fileStream = file.OpenReadStream())
        {
            using (var reader = new StreamReader(fileStream))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    if (parts.Length != 4)
                        continue;

                    var empId = int.Parse(parts[0]);
                    var projectId = int.Parse(parts[1]);
                    ParseDateTime(parts[2], out var dateFrom);
                    ParseDateTime(parts[3], out var dateTo);

                    projectDataList.Add(new ProjectData(empId, projectId, dateFrom, dateTo));
                }
            }
        }

        return projectDataList;
    }

    private static void ParseDateTime(string input, out DateTime result)
    {
        if (input == "NULL")
        {
            result = DateTime.Today;
            return;
        }
            
        if (DateTime.TryParse(input, out result))
                return;

        result = DateTime.MinValue;
    }

    private static string FindLongestWorkingPairs(List<ProjectData> projectDataList)
    {
        var uniquePairs = new HashSet<string>();
        var sb = new StringBuilder();

        for (var i = 0; i < projectDataList.Count - 1; i++)
        {
            for (var j = i + 1; j < projectDataList.Count; j++)
            {
                var project1 = projectDataList[i];
                var project2 = projectDataList[j];

                if (!project1.IsOverlap(project2)) 
                    continue;
                
                var pairKey = CommonProjects.GetPairKey(project1.EmployeeId, project2.EmployeeId);

                if (uniquePairs.Contains(pairKey)) 
                    continue;
                    
                uniquePairs.Add(pairKey);
                sb.AppendLine($"{project1.EmployeeId},{project2.EmployeeId},{project1.CalculateOverlapDays(project2)}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}