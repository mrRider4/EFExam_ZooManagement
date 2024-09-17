using EFExam_Zoo.Models.Sections;
using EFExam_Zoo.Models.Sections.Dtos;

namespace EFExam_Zoo.EFPersistance.Sections;

public class EFSectionRepository(EFDataContext context)
{
    public void Add(Section section)
    {
        context.Set<Section>().Add(section);
    }

    public List<SectionDto> ShowAllFullSectionByZooId(int zooId)
    {
        return context.Set<Section>().Where(_ => _.ZooId == zooId && _.AnimalId != null)
            .Select(_ => new SectionDto()
            {
                Id = _.Id,
                Area = _.Area,
                AminalName = _.Animal.Name,
                AnimalsCount = _.AnimalsCount,
                Description = _.Description,
                ZooName = _.Zoo.Name
            }).OrderByDescending(_=>_.Id).ToList();
    }

    public Section ShowById(int sectionId)
    {
        return context.Set<Section>().First(_ => _.Id == sectionId);
    }
    
}