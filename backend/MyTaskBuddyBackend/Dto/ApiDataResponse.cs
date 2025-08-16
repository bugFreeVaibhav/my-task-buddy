
    namespace MyTaskBuddyBackend.Dto
    {
        public class ApiDataResponse<T>
        {
            public bool Success { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }

            public ApiDataResponse(bool Success, string message = null, T data = default)
            {
                this.Success = Success;
                TimeStamp = DateTime.Now;
                Message = message;
                Data = data;
            }
        }
    }
