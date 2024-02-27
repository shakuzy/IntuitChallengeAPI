using IntuitChallengeAPI.Clases.DTO;

namespace IntuitChallengeAPI.Clases.API.Response
{
    public class InsertResponse
    {
        public InsertResponse()
        {
        }

        public InsertResponse(string? estado, int? idCliente)
        {
            Estado = estado;
            IdCliente = idCliente;
        }

        public InsertResponse(string? estado, string? descError)
        {
            Estado = estado;
            DescError = descError;
        }

        public string? Estado { get; set; }
        public string? DescError { get; set; }
        public int? IdCliente { get; set; }
    }
}
