namespace IntuitChallengeAPI.Clases.API.Response
{
    public class UpdateResponse
    {
        public UpdateResponse()
        {
        }

        public UpdateResponse(string? estado)
        {
            Estado = estado;
        }

        public UpdateResponse(string? estado, string? descError)
        {
            Estado = estado;
            DescError = descError;
        }

        public string? Estado { get; set; }
        public string? DescError { get; set; }
    }
}
