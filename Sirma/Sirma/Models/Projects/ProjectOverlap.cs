namespace Sirma.Models.Projects;

public class ProjectOverlap
{
    public int ProjectId { get; }
    public int DaysWorked { get; }

    public ProjectOverlap(int projectId, int daysWorked)
    {
        ProjectId = projectId;
        DaysWorked = daysWorked;
    }
}