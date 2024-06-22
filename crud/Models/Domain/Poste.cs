namespace Crud.Models.Domain
{
    public class Poste{
        public Guid Id {get; set;}
        public string NamePoste { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}