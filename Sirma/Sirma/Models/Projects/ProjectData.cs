namespace Sirma.Models.Projects;

public class ProjectData
{
    public ProjectData(
        int employeeId, 
        int projectId, 
        DateTime dateFrom, 
        DateTime dateTo)
    {
        EmployeeId = employeeId;
        ProjectId = projectId;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
    
    public int EmployeeId { get; }
    public int ProjectId { get; }
    public DateTime DateFrom { get; }
    public DateTime DateTo { get; }
    
    public bool IsOverlap(ProjectData other)
    {
        return ProjectId == other.ProjectId &&
               (DateFrom <= other.DateTo && other.DateFrom <= DateTo);
    }

    public int CalculateOverlapDays(ProjectData other)
    {
        DateTime overlapStart = DateFrom > other.DateFrom ? DateFrom : other.DateFrom;
        DateTime overlapEnd = DateTo < other.DateTo ? DateTo : other.DateTo;

        return (int)(overlapEnd - overlapStart).TotalDays + 1;
    }
}