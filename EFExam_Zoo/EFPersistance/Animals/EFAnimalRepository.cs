using EFExam_Zoo.Models.Animals;
using EFExam_Zoo.Models.Animals.Dtos;
using EFExam_Zoo.Models.Sections;
using EFExam_Zoo.Models.Zoos;
using Microsoft.EntityFrameworkCore;

namespace EFExam_Zoo.EFPersistance.Animals;

public class EFAnimalRepository(EFDataContext context)
{
    public void Add(Animal animal)
    {
        context.Set<Animal>().Add(animal);
    }

    public Animal ShowById(int animalId)
    {
        return context.Set<Animal>().First(_ => _.Id == animalId);
    }

    public List<AnimalDto> ShowAll()
    {
        return context.Set<Animal>().Select(_ => new AnimalDto()
        {
            Id = _.Id,
            Name = _.Name
        }).ToList();
    }

    public List<ZooAnimalsDto> ShowZooAnimals()
    {
       return (from animal in context.Set<Animal>()
            join section in context.Set<Section>()
                on animal.Id equals section.AnimalId
            join zoo in context.Set<Zoo>()
                on section.ZooId equals zoo.Id
                into AnimalZoo
            from animalZoo in AnimalZoo
            select new
            {
                ZooName = animalZoo.Name,
                ZooAddress = animalZoo.Address,
                AnimalName = animal.Name
            }).GroupBy(_ => _.ZooName).Select(_ => new ZooAnimalsDto()
        {
            Details = _.Key + "\t" + _.First().ZooAddress + "\n" + string.Join("\n", _.Select(_ => _.ZooName))
        }).ToList();
    }
}