using EFExam_Zoo.Models.TIckets;

namespace EFExam_Zoo.Models.Zoos.Dtos;

public class ZoosWithSectionsAndTicketsDto
{
    public string ZooName { get; set; }
    public string Address { get; set; }
    public int SectionsCount { get; set; }
    public List<decimal> Prices { get; set; }
    public List<decimal> Areas { get; set; }
    public List<string> Descriptions { get; set; }
}