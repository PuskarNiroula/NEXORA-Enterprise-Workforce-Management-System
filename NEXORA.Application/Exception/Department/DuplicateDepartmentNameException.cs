namespace NEXORA.Application.Exception.Department;

public class DuplicateDepartmentNameException(string name)
    : System.Exception($"Department with name {name} already exists!");
