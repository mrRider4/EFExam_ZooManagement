using EFExam_Zoo.Models.Sections;

namespace EFExam_Zoo.Models.TIckets;

public class Ticket
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int SectionId { get; set; }
    public Section Section { get; set; }
}