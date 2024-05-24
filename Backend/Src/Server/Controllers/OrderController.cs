namespace Server.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(
    IOrderService orderService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPaginatedOrdersAsync(
        int pageNumber, int pageSize, string? searchTerms)
    {
        return Ok(await orderService.GetPaginatedOrdersAsync(
            pageNumber, pageSize, searchTerms));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await orderService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
    {
        return Ok(await orderService.CreateAsync(request));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateOrderRequest request)
    {
        return Ok(await orderService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(await orderService.DeleteAsync(id));
    }
}