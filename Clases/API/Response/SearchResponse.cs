using IntuitChallengeAPI.Clases.DTO;

namespace IntuitChallengeAPI.Clases.API.Response
{
    public class SearchResponse
    {
        public SearchResponse()
        {
        }

        public SearchResponse(string? estado, string? descError)
        {
            Estado = estado;
            DescError = descError;
        }

        public SearchResponse(string? estado, ClienteDTO? cliente)
        {
            Estado = estado;
            Cliente = cliente;
        }

        public string? Estado { get; set; }
        public string? DescError { get; set; }
        public ClienteDTO? Cliente { get; set; }
    }
}
