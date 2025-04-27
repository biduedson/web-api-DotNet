
namespace Application.Http
{
    public class RespostasDaApi<TData> : IRespostasDaApi<TData>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public TData? Data { get; set; }


        public RespostasDaApi<TData> Ok(TData Data, string Message)
        {
            return new RespostasDaApi<TData>
            {
                Success = true,
                Data = Data,
                Message = Message
            };

        }

        public RespostasDaApi<TData> Error(string? Message, List<string>? Errors)
        {
            return new RespostasDaApi<TData>
            {
                Success = false,
                Message = Message,
                Errors = Errors
            };
        }

        public RespostasDaApi<string> Error(string? Message)
        {
            return new RespostasDaApi<string>
            {
                Success = false,
                Message = Message
            };
        }
        public RespostasDaApi<string> AutenthicationError(string? Message)
        {
            return new RespostasDaApi<string>
            {
                Success = false,
                Message = Message
            };
        }
    }
}
