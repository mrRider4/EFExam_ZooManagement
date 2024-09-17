namespace EFExam_Zoo.Models.Zoos.Dtos;

public class ZooWithSectionsAndAnimalsDto
{
    public string ZooName { get; set; }
    public string ZooAddress { get; set; }
    public List<string> AnimalNames { get; set; }
    public List<int> AnimalsCounts { get; set; }
    public List<string> Descriptions { get; set; }
}