namespace Application.Http
{
    public interface IRespostasDaApi<TData>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public TData? Data { get; set; }

        public RespostasDaApi<TData> Ok(TData Data, string Message);
        public RespostasDaApi<TData> Error(string? Message, List<string>? Errors);
        public RespostasDaApi<string> Error(string? Message);
        public RespostasDaApi<string> AutenthicationError(string? Message);
    }
}

