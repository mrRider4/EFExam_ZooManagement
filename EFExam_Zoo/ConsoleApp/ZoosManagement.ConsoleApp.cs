using EFExam_Zoo.ConsooleApp.ConsoolTools;
using EFExam_Zoo.EFPersistance;
using EFExam_Zoo.EFPersistance.Animals;
using EFExam_Zoo.EFPersistance.Sections;
using EFExam_Zoo.EFPersistance.Tickets;
using EFExam_Zoo.EFPersistance.Zoos;
using EFExam_Zoo.Models.Animals;
using EFExam_Zoo.Models.Sections;
using EFExam_Zoo.Models.TIckets;
using EFExam_Zoo.Models.Zoos;
using EFExam_Zoo.Models.Zoos.Dtos;

namespace EFExam_Zoo.ConsoleApp;

public class ZoosManagement_ConsoleApp(EFDataContext context, ConsoleTools io)
{
    private EFZooRepository _zooRepository = new EFZooRepository(context);
    private EFSectionRepository _sectionRepository = new EFSectionRepository(context);
    private EFAnimalRepository _animalRepository = new EFAnimalRepository(context);
    private EFTicketRepository _ticketRepository = new EFTicketRepository(context);

    private Dictionary<string, Action> _mainMenu;
    public bool FinishIt { get; private set; }

    private void PrintMenu()
    {
        for (int i = 0; i < _mainMenu.Count; i++)
        {
            io.Write($"[{i + 1}] {_mainMenu.Keys.ElementAt(i)}");
        }
    }

    public void BuildMainMenu()
    {
        FinishIt = false;
        _mainMenu = new Dictionary<string, Action>
        {
            { "Add New Zoo", AddNewZoo },
            { "Add New Section", AddNewSection },
            { "Add New Animal", AddNewAnimal },
            { "Add New Ticket", AddNewTicket },
            { "Update Section", UpdateSection },
            {"Show Zoo And Animals",ShowZooAndAnimals},
            {"Show Zoos With Sections And Tickets",ShowZoosWithSectionsAndTickets},
            {"Show Zoo With Sections And Animals",ShowZooWithSectionsAndAnimals},
            { "Exit", Exit }
        };
    }

    public void Run()
    {
        io.Clear();
        io.Write("<< Zoos Management >>\n");
        PrintMenu();
        int selectedIndex = io.GetIntFromUser(1, _mainMenu.Count) - 1;
        string selectedKey = _mainMenu.Keys.ElementAt(selectedIndex);
        io.Clear();
        _mainMenu[selectedKey].Invoke();
        if (FinishIt) return;
        io.Write("\nPress any key ...");
        io.ReadKey();
    }

    private void Exit()
    {
        FinishIt = true;
        io.Write("Finished...");
        throw new Exception();
    }

    private void AddNewZoo()
    {
        io.Write("Add New Zoo");

        io.Write("Enter name : ");
        string name = io.GetStringFromUser(100);
        if (io.HaveToReturn(name)) return;

        io.Write("Enter Address : ");
        string address = io.GetStringFromUser(500);
        if (io.HaveToReturn(address)) return;

        Zoo newZoo = new Zoo()
        {
            Name = name,
            Address = address
        };
        _zooRepository.Add(newZoo);
        context.SaveChanges();
        io.Clear();
        io.Write($"New zoo registered successfully\nName : {name}\nAddress : {address}");
    }

    private void AddNewSection()
    {
        io.Write("Add New Section");

        var Zoos = _zooRepository.ShowAll().Select(_ => new
        {
            Id = _.Id,
            Detail = _.Name + "\t" + _.Address
        }).OrderByDescending(_ => _.Id).ToList();
        io.Write("Choose Zoo : ");
        io.Write(io.ShowAsNumericList(Zoos.Select(_ => _.Detail).ToList()));

        string exitOpportunity = io.ReadLine();
        if (io.HaveToReturn(exitOpportunity)) return;
        int selectedZoosIndex = io.GetIntFromUser(1, Zoos.Count(), exitOpportunity) - 1;

        io.Write("\nEnter section area as meter : ");
        exitOpportunity = io.ReadLine();
        if (io.HaveToReturn(exitOpportunity)) return;
        int area = io.GetIntFromUser(0, exitOpportunity);

        io.Write("Enter section description : ");
        string description = io.GetStringFromUser(500);
        if (io.HaveToReturn(description)) return;

        Section newSection = new Section()
        {
            Area = area,
            Description = description,
            AnimalsCount = 0,
            ZooId = Zoos[selectedZoosIndex].Id
        };
        _sectionRepository.Add(newSection);
        context.SaveChanges();
        io.Clear();
        io.Write($"New zoo section registered successfully\nArea : {area}\nDescription : {description}" +
                 $"\nZoo : {Zoos[selectedZoosIndex].Detail}");
    }

    private void AddNewAnimal()
    {
        io.Write("Add New Animal");

        io.Write("Enter animal name : ");
        string name = io.GetStringFromUser(100);
        if (io.HaveToReturn(name)) return;

        Animal newAnimal = new Animal()
        {
            Name = name
        };
        _animalRepository.Add(newAnimal);
        context.SaveChanges();
        io.Clear();
        io.Write($"New animal registered successfully\nAnimal name : {name}");
    }

    private void AddNewTicket()
    {
        io.Write("Add New Ticket");


        int SectionId = SelectSectionId();

        io.Write("\nEnter ticket price : ");
        string exitOpportunity = io.ReadLine();
        if (io.HaveToReturn(exitOpportunity)) return;
        int price = io.GetIntFromUser(0, exitOpportunity);

        Ticket newTicket = new Ticket()
        {
            Price = price,
            SectionId = SectionId
        };
        _ticketRepository.Add(newTicket);
        context.SaveChanges();
    }

    private void UpdateSection()
    {
        Section sectionToEdit = _sectionRepository.ShowById(SelectSectionId());
        while (true)
        {
            string exitOpportunity = "";
            io.Write("Update Section");
            io.Write("Choose to edit");
            io.Write("[1] Area\n[2] AnimalCount\n[3] Aminmal\n[4] Description\n[5] Ticket\n[6] Return main menu");
            switch (io.GetStringFromUser())
            {
                case "1":
                {
                    io.Write("New area : ");
                    exitOpportunity = io.ReadLine();
                    if (io.HaveToReturn(exitOpportunity)) return;
                    sectionToEdit.Area = io.GetIntFromUser(0, exitOpportunity);
                }
                    break;
                case "2":
                {
                    io.Write("New animal count : ");
                    exitOpportunity = io.ReadLine();
                    if (io.HaveToReturn(exitOpportunity)) return;
                    sectionToEdit.AnimalsCount = io.GetIntFromUser(0, exitOpportunity);
                }
                    break;
                case "3":
                {
                    io.Write("Choose new animal : ");
                    var animals = _animalRepository.ShowAll();
                    io.Write(io.ShowAsNumericList(animals.Select(_=>_.Name).ToList()));
                    sectionToEdit.AnimalId = animals[io.GetIntFromUser(1, animals.Count() - 1)].Id;
                }
                    break;
                case "4":
                {
                    io.Write("New section description : ");
                    string description = io.GetStringFromUser(500);
                    if (io.HaveToReturn(description)) return;
                    sectionToEdit.Description = description;
                }
                    break;
                case "5":
                {
                    if (sectionToEdit.TicketId == null)
                    {
                        throw new Exception("No ticket set\n go to Add New Ticket");
                    }

                    Ticket ticketToEdit = _ticketRepository.ShowById(sectionToEdit.TicketId ?? 0);
                    io.Write("\nEnter ticket price : ");
                    exitOpportunity = io.ReadLine();
                    if (io.HaveToReturn(exitOpportunity)) return;
                    ticketToEdit.Price = io.GetIntFromUser(0, exitOpportunity);
                }
                    break;
                case "6": return;
                default:
                    io.Write("Invalid entry");
                    break;
            }

            context.SaveChanges();
            io.Write("press Any key");
            io.ReadKey();
        }
    }

    private int SelectSectionId()
    {
        var Zoos = _zooRepository.ShowAll().Select(_ => new
        {
            Id = _.Id,
            Detail = _.Name + "\t" + _.Address
        }).OrderByDescending(_ => _.Id).ToList();
        if (!Zoos.Any())
        {
            io.Write("no Zoo found!");
            throw new Exception();
        }

        io.Write("Choose Zoo : ");
        io.Write(io.ShowAsNumericList(Zoos.Select(_ => _.Detail).ToList()));
        string exitOpportunity = io.ReadLine();
        if (io.HaveToReturn(exitOpportunity)) throw new Exception();
        int selectedZoosIndex = io.GetIntFromUser(1, Zoos.Count(), exitOpportunity) - 1;
        int zooId = Zoos[selectedZoosIndex].Id;

        var sections = _sectionRepository.ShowAllFullSectionByZooId(zooId)
            .Select(_ => new
            {
                Id = _.Id,
                Detail = _.AminalName + "\n" + _.AnimalsCount + "\n" + _.Description
            }).ToList();
        if (!sections.Any())
        {
            io.Write("no Section found that have animal\nTo Add set animal for section go to Update Section ...!");
            throw new Exception();
        }

        io.Write("Choose Section (only sections that have animal are in this list) : ");
        io.Write(io.ShowAsNumericList(sections.Select(_ => _.Detail).ToList()));
        exitOpportunity = io.ReadLine();
        if (io.HaveToReturn(exitOpportunity)) throw new Exception();
        int selectedSectionIndex = io.GetIntFromUser(1, Zoos.Count(), exitOpportunity) - 1;
        int SectionId = sections[selectedSectionIndex].Id;

        return SectionId;
    }

    private void ShowZooAndAnimals()
    {
        var ZooAndAnimals = _animalRepository.ShowZooAnimals().Select(_=>_.Details).ToList();
        io.Write(io.ShowAsNumericList(ZooAndAnimals));
    }

    private void ShowZoosWithSectionsAndTickets()
    {
        var ZoosWithSectionsAndTickets = _zooRepository.ShowZoosWithSectionsAndTickets();
        ZoosWithSectionsAndTickets.ForEach(item =>
        {
            io.Write($"{item.ZooName}\t{item.Address}\t Section Count : {item.SectionsCount}");
            for (int i = 0; i < item.SectionsCount; i++)
            {
                io.Write($"Description : {item.Descriptions[i]}" +
                         $"\nPrice : {item.Prices[i]}   Area : {item.Areas[i]}");
            }
        });
    }
    
    private void ShowZooWithSectionsAndAnimals()
    {
        var ZooWithSectionsAndAnimals = _zooRepository.ShowZooWithSectionsAndAnimals();
        ZooWithSectionsAndAnimals.ForEach(item =>
        {
            io.Write($"{item.ZooName}\t{item.ZooAddress}\t");
            for (int i = 0; i < item.Descriptions.Count(); i++)
            {
                io.Write($"Description : {item.Descriptions[i]}" +
                         $"\nPrice : {item.AnimalNames[i]}   Area : {item.AnimalsCounts[i]}");
            }
        });
    }

    private void BuyTicket()
    {
        int sectionId=SelectSectionId();
        Ticket ticket = _ticketRepository.ShowBySectionId(sectionId);
        io.Write();
    }
}