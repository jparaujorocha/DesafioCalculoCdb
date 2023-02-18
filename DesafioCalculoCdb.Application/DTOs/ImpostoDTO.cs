namespace DesafioCalculoCdb.Application.DTOs
{
    public class ImpostoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdTipoImposto { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
