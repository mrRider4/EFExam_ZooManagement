using EFExam_Zoo.Models.Animals;
using EFExam_Zoo.Models.TIckets;
using EFExam_Zoo.Models.Zoos;

namespace EFExam_Zoo.Models.Sections.Dtos;

public class SectionDto
{
    public int Id { get; set; }
    public decimal Area { get; set; }
    public int AnimalsCount { get; set; }
    public string AminalName { get; set; }
    public string Description { get; set; }
    public string ZooName { get; set; }
}