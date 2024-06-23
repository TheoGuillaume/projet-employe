using Crud.Models.Domain;

namespace crud.Models.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Motif { get; set; }
        public long NombreJour { get; set; }
        public string Status { get; set; }

        public Guid EmployeeId { get; set; }    
        public Employee Employe { get; set; }
    }
}
