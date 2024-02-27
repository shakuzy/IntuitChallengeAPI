using IntuitChallengeAPI.Clases.DTO;

namespace IntuitChallengeAPI.Clases.API.Response
{
    public class GetAllResponse
    {
        public GetAllResponse()
        {
        }

        public GetAllResponse(string? estado, string? descError)
        {
            Estado = estado;
            DescError = descError;
        }

        public GetAllResponse(string? estado, List<ClienteDTO>? clientes)
        {
            Estado = estado;
            Clientes = clientes;
        }

        public string? Estado { get; set; }
        public string? DescError { get; set; }
        public List<ClienteDTO>? Clientes { get; set; }
    }
}
