using EFExam_Zoo.Models.Sections;

namespace EFExam_Zoo.Models.Animals;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Section> Sections { get; set; }
}