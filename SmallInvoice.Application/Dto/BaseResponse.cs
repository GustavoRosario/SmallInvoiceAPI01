
namespace SmallInvoice.Application.Dto
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string? Date { get; set; } = DateTime.Now.ToShortDateString();
        public string? Time { get; set; } = DateTime.Now.ToShortTimeString();
        public string? Message { get; set; } = "N/A";
    }
}
