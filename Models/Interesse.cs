namespace LojaVeiculos.Models
{
    public class Interesse
    {
      
       public string Nome { get; set;}
        public string Telefone { get; set; }
        public string VeiculoInteresse { get; set; }
        public double ValorParcela { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;


    }
}
