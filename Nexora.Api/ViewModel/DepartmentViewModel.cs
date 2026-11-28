using NEXORA.Domain.Enums;

namespace Nexora.Api.ViewModel;

public class DepartmentViewModel
{
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public DepartmentStatusEnum Status { get; set; }
    
}