using EFExam_Zoo.Models.Animals;
using EFExam_Zoo.Models.Animals.Dtos;
using EFExam_Zoo.Models.Sections;
using EFExam_Zoo.Models.TIckets;
using EFExam_Zoo.Models.Zoos;
using EFExam_Zoo.Models.Zoos.Dtos;

namespace EFExam_Zoo.EFPersistance.Zoos;

public class EFZooRepository(EFDataContext context)
{
    public void Add(Zoo zoo)
    {
        context.Set<Zoo>().Add(zoo);
    }

    public List<ZooDto> ShowAll()
    {
        return context.Set<Zoo>().Select(_ => new ZooDto()
        {
            Id = _.Id,
            Name = _.Name,
            Address = _.Address
        }).OrderByDescending(_ => _.Id).ToList();
    }

    public List<ZoosWithSectionsAndTicketsDto> ShowZoosWithSectionsAndTickets()
    {
        return (from zoo in context.Set<Zoo>()
            join section in context.Set<Section>()
                on zoo.Id equals section.ZooId
            join ticket in context.Set<Ticket>()
                on section.TicketId equals ticket.Id
                into SectionTicket
            from sectionTicket in SectionTicket.DefaultIfEmpty()
            select new
            {
                Name = zoo.Name,
                Address = zoo.Address,
                Price = section.TicketId == null ? 0 : sectionTicket.Price,
                Area = section.Area,
                Description = section.Description
            }).GroupBy(_ => _.Name).Select(_ => new ZoosWithSectionsAndTicketsDto()
        {
            ZooName = _.Key,
            Address = _.First().Address,
            Areas = _.Select(_ => _.Area).ToList(),
            Descriptions = _.Select(_ => _.Description).ToList(),
            Prices = _.Select(_ => _.Price).ToList(),
            SectionsCount = _.Select(_ => _.Area).ToList().Count()
        }).ToList();
    }

    public List<ZooWithSectionsAndAnimalsDto> ShowZooWithSectionsAndAnimals()
    {
        return (from zoo in context.Set<Zoo>()
            join section in context.Set<Section>()
                on zoo.Id equals section.ZooId
            join animal in context.Set<Animal>()
                on section.AnimalId equals animal.Id
                into SectionAnimals
            from sectionAnimals in SectionAnimals.DefaultIfEmpty()
            select new
            {
                ZooName = zoo.Name,
                ZooAddress = zoo.Address,
                Description = section.Description,
                AnimalName = section.AnimalId == null ? "NO Animal" : sectionAnimals.Name,
                AnimalCount = section.AnimalsCount,
            }).GroupBy(_ => _.ZooName).Select(_=>new ZooWithSectionsAndAnimalsDto()
        {
            ZooName = _.Key,
            ZooAddress = _.First().ZooAddress,
            AnimalNames = _.Select(_=>_.AnimalName).ToList(),
            AnimalsCounts = _.Select(_=>_.AnimalCount).ToList(),
            Descriptions = _.Select(_=>_.Description).ToList()
        }).ToList();
    }
}