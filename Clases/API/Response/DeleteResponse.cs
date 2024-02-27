namespace IntuitChallengeAPI.Clases.API.Response
{
    public class DeleteResponse
    {
        public DeleteResponse()
        {
        }

        public DeleteResponse(string? estado)
        {
            Estado = estado;
        }

        public DeleteResponse(string? estado, string? descError)
        {
            Estado = estado;
            DescError = descError;
        }

        public string? Estado { get; set; }
        public string? DescError { get; set; }
    }
}
