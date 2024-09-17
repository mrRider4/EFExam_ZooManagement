using EFExam_Zoo.Models.TIckets;
using EFExam_Zoo.Models.TIckets.Dtos;

namespace EFExam_Zoo.EFPersistance.Tickets;

public class EFTicketRepository(EFDataContext context)
{
    public void Add(Ticket ticket)
    {
        context.Set<Ticket>().Add(ticket);
    }

    public Ticket ShowById(int ticketId)
    {
        return context.Set<Ticket>().First(_ => _.Id == ticketId);
    }

    public Ticket ShowBySectionId(int sectionId)
    {
        return context.Set<Ticket>().First(_ => _.SectionId == sectionId);
    }
}