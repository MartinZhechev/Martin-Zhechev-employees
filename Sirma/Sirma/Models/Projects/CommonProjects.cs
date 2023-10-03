namespace Sirma.Models.Projects;

public class CommonProjects
{
    public CommonProjects(int employeeOneId, int employeeTwoId)
    {
        this.EmployeeOneId = employeeOneId;
        this.EmployeeTwoId = employeeTwoId;
    }
    
    public int EmployeeOneId { get; }
    public int EmployeeTwoId { get; }
    
    public static string GetPairKey(int employeeOneId, int employeeTwoId)
    {
        return employeeOneId < employeeTwoId ? $"{employeeOneId}-{employeeTwoId}" : $"{employeeTwoId}-{employeeOneId}";
    }
}