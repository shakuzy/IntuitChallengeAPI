using IntuitChallengeAPI.Clases.DTO;

namespace IntuitChallengeAPI.Models
{
    public partial class Cliente
    {
        public ClienteDTO Convertir()
        {
            return new ClienteDTO()
            {
                Apellidos = this.Apellidos,
                Cuit = this.Cuit,
                Domicilio = this.Domicilio,
                Email = this.Email,
                FechaNacimiento = this.FechaNacimiento,
                Id = this.Id,
                Nombres = this.Nombres,
                TelefonoCelular = this.TelefonoCelular
            };
        }
    }
}
