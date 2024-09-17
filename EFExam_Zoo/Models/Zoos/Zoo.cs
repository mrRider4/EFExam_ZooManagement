using EFExam_Zoo.Models.Sections;

namespace EFExam_Zoo.Models.Zoos;

public class Zoo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Section> Sections { get; set; }
}