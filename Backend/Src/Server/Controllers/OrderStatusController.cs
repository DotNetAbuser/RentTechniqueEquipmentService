namespace Server.Controllers;

[ApiController]
[Route("api/order/status")]
public class OrderStatusController(
    IOrderStatusService orderStatusService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await orderStatusService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await orderStatusService.GetByIdAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderStatusRequest request)
    {
        return Ok(await orderStatusService.CreateAsync(request));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateOrderStatusRequest request)
    {
        return Ok(await orderStatusService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await orderStatusService.DeleteAsync(id));
    }
    
}