namespace onlineCinema.Application.DTOs;

public class MerchDto // Було SnackDto
{
    public int MerchId { get; set; } // Було SnackId
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}