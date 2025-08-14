namespace MyTaskBuddyBackend.Dto
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public DateTime TimeStamp { get; set; }
        public ApiResponse(bool v1) {
            this.IsSuccess = v1;
            this.TimeStamp = DateTime.Now;
        }
    }
}
