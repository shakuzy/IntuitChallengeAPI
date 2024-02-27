using IntuitChallengeAPI.Clases.DTO;

namespace IntuitChallengeAPI.Clases.API.Response
{
    public class GetResponse
    {
        public GetResponse()
        {
        }

        public GetResponse(string? estado, string? descError)
        {
            Estado = estado;
            DescError = descError;
        }

        public GetResponse(string? estado, ClienteDTO? cliente)
        {
            Estado = estado;
            Cliente = cliente;
        }

        public string? Estado { get; set; }
        public string? DescError { get; set; }
        public ClienteDTO? Cliente { get; set; }
    }
}
