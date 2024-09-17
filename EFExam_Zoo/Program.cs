// See https://aka.ms/new-console-template for more information

using EFExam_Zoo.ConsoleApp;
using EFExam_Zoo.ConsooleApp.ConsoolTools;
using EFExam_Zoo.EFPersistance;

EFDataContext context = new EFDataContext();
ZoosManagement_ConsoleApp app = new ZoosManagement_ConsoleApp(new EFDataContext(), new ConsoleTools());
app.BuildMainMenu();
while (!app.FinishIt)
{
    try
    {
        context.Database.BeginTransaction();
        app.Run();
        context.Database.CommitTransaction();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        context.Database.RollbackTransaction();
    }
}