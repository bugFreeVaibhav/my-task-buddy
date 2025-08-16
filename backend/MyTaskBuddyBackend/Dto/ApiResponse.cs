namespace MyTaskBuddyBackend.Dto
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }
        
        public ApiResponse(bool v1, string message = null)
        {
            this.Success = v1;
            this.TimeStamp = DateTime.Now;
            this.Message = message;
        }

        public ApiResponse() { 
            
            TimeStamp = DateTime.Now;
        }
    }
}
