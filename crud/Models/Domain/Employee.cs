namespace Crud.Models.Domain
{
    public class Employee 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

         // Ajouter la propriété pour la clé étrangère
        public Guid PosteId { get; set; }
        public Poste Poste { get; set; } // Propriété de navigation

          public List<Paye> Payes { get; set; }

        public string Department { get; set; }
    }
}