namespace Crud.Models.Domain
{
    public class Paye{
        public Guid Id {get; set;}
        public DateTime dateOfPayment { get; set; }
        public int jourPayement {get; set;}

        public int payeParJour {get; set;}

        public ICollection<Paye> Payes { get; set; }
    }
}