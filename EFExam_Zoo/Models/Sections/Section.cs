using EFExam_Zoo.Models.Animals;
using EFExam_Zoo.Models.TIckets;
using EFExam_Zoo.Models.Zoos;

namespace EFExam_Zoo.Models.Sections;

public class Section
{
    public int Id { get; set; }
    public decimal Area { get; set; }
    public int AnimalsCount { get; set; }
    public string Description { get; set; }
    public int? AnimalId { get; set; }
    public Animal? Animal { get; set; }
    public int? TicketId { get; set; }
    public Ticket? Ticket { get; set; }
    public int ZooId { get; set; }
    public Zoo Zoo { get; set; }
}