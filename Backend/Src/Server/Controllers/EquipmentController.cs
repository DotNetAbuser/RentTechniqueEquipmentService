namespace Server.Controllers;

[ApiController]
[Route("api/order/equipment")]
public class EquipmentController(
    IEquipmentService equipmentService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await equipmentService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await equipmentService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateEquipmentRequest request)
    {
        return Ok(await equipmentService.CreateAsync(request));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateEquipmentRequest request)
    {
        return Ok(await equipmentService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await equipmentService.DeleteAsync(id));
    }
}