namespace NEXORA.Application.Exception.Department;

public class DepartmentNotFoundException(string? id = "") : System.Exception($"Department with id {id} not found");
