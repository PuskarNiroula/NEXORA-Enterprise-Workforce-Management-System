using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Api.ViewModel;
using NEXORA.Application.Dto.Department;
using NEXORA.Application.Interfaces.ServiceInterfaces;

namespace Nexora.Api.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class DepartmentController(IDepartmentService service):ControllerBase
{
    public async Task<IActionResult> Index()
    {
        try
        {
         var departments=await service.GetAllDepartmentsAsync();
         var vms = departments.Select(d => new DepartmentViewModel
         {
             DepartmentId = d.DepartmentId,
             Name = d.Name,
             Status = d.Status,
         }).ToList();

         return Ok(vms);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{departmentId}")]
    public async Task<IActionResult> Activate(Guid departmentId)
    {
        try
        {
            await service.ActivateDepartment(departmentId);
            return Ok("Department activated successfully");
            

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPatch("{departmentId}")]
    public async Task<IActionResult> Deactivate(Guid departmentId)
    {
        try
        {
            await service.DeactivateDepartment(departmentId);
            return Ok("Department deactivated successfully");
            

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Store(DepartmentCreateDto dto)
    {
        try
        {
            await service.CreateDepartmentAsync(dto);
            return Ok("Department created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }  
    [HttpPut("{departmentId}")]
    public async Task<IActionResult> Update(Guid id,string name)
    {
        try
        {
            var dto = new DepartmentUpdateDto
            {
                Name = name,
                DepartmentId = id
            };
            await service.UpdateDepartmentAsync(dto);
            return Ok("Department created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
    
    
    
}