using Business.Services;

namespace Presentation.ConsoleApp
{
    public class MenuDialogs
    {
        private readonly CustomerService _customerService;
        private readonly ProjectService _projectService;

        public MenuDialogs(CustomerService customerService, ProjectService projectService)
        {
            _customerService = customerService;
            _projectService = projectService;
        }

        public async Task MenuOptions()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Projekt Hantering ===");
                Console.WriteLine("1. Skapa ny kund");
                Console.WriteLine("2. Skapa nytt projekt");
                Console.WriteLine("3. Lista alla kunder");
                Console.WriteLine("4. Lista alla projekt");
                Console.WriteLine("5. Hämta kund med ID");
                Console.WriteLine("6. Hämta projekt med ID");
                Console.WriteLine("7. Uppdatera kund");
                Console.WriteLine("8. Uppdatera projekt");
                Console.WriteLine("9. Ta bort kund");
                Console.WriteLine("10. Ta bort projekt");
                Console.WriteLine("0. Avsluta");
                Console.Write("Välj ett alternativ: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await CreateNewCustomer();
                        break;
                    case "2":
                        await CreateNewProject();
                        break;
                    case "3":
                        await ListAllCustomers();
                        break;
                    case "4":
                        await ListAllProjects();
                        break;
                    case "5":
                        await GetCustomerByID();
                        break;
                    case "6":
                        await GetProjectById();
                        break;
                    case "7":
                        await UpdateCustomer();
                        break;
                    case "8":
                        await UpdateProject();
                        break;
                    case "9":
                        await DeleteCustomer();
                        break;
                    case "10":
                        await DeleteProject();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Tryck på en tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
        }

        private async Task CreateNewCustomer()
        {
            Console.Clear();
            Console.WriteLine("--- Skapa en ny kund ---");
            Console.Write("Ange kundnamn: ");
            string customerName = Console.ReadLine() ?? string.Empty;

            Console.Write("Ange kundemail: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Ange kund-telefonnummer: ");
            string phoneNumber = Console.ReadLine() ?? string.Empty;

            var form = new Business.Models.CustomerRegistrationForm
            {
                CustomerName = customerName,
                CustomerEmail = email,
                CustomerPhoneNumber = phoneNumber,
            };
            await _customerService.CreateCustomerAsync(form);

            Console.WriteLine("Kunden har skapats! Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task CreateNewProject()
        {
            Console.Clear();
            Console.WriteLine("--- Skapa ett nytt projekt ---");

            Console.Write("Ange projektnamn: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Projektnamn är obligatoriskt. Försök igen.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ange startdatum (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Ange slutdatum (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Ange projektansvarig: ");
            string projectManager = Console.ReadLine() ?? "";

            Console.Write("Ange Befintlig kund-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Ogiltigt kund-ID.");
                Console.ReadKey();
                return;
            }

            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine("Kunden med det angivna ID:t finns inte. Försök igen.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ange tjänstebeskrivning (ex. konsulttid 1000kr/tim): ");
            string serviceDescription = Console.ReadLine() ?? "";

            Console.Write("Ange totalpris: ");
            decimal totalPrice = decimal.Parse(Console.ReadLine() ?? "0");

            var model = new Business.Models.ProjectModel
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                ProjectManager = projectManager,
                CustomerId = customerId,
                ServiceDescription = serviceDescription,
                TotalPrice = totalPrice,
               
            };

            await _projectService.CreateProjectAsync(model);

            Console.WriteLine("Projektet har skapats! Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }


        private async Task ListAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("--- Lista på alla kunder ---");

            try
            {
                var customers = await _customerService.GetCustomerAsync();
                Console.WriteLine($"Antal kunder: {customers.Count()}");  // Debug-utskrift

                if (!customers.Any())
                {
                    Console.WriteLine("Inga kunder hittades.");
                }
                else
                {
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"ID: {customer?.Id}, Namn: {customer?.CustomerName}, Email: {customer?.CustomerEmail}, PhoneNumber: {customer?.CustomerPhoneNumber}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid hämtning av kunder: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }

            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task ListAllProjects()
        {
            Console.Clear();
            Console.WriteLine("--- Lista alla projekt ---");

            var projects = await _projectService.GetAllProjectsAsync();
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Projektnummer: {project.ProjectNumber}, Namn: {project.Name}, " +
                                  $"Period: {project.StartDate.ToShortDateString()} - {project.EndDate.ToShortDateString()}, " +
                                  $"Status: {project.Status}");
            }

            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task GetCustomerByID()
        {
            Console.Clear();
            Console.Write("Ange kund-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID-format.");
                Console.ReadKey();
                return;
            }

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                Console.WriteLine($"ID: {customer?.Id}, Namn: {customer?.CustomerName}, Email: {customer?.CustomerEmail}, PhoneNumber: {customer?.CustomerPhoneNumber}");
            }
            else
            {
                Console.WriteLine("Kunden hittades inte.");
            }

            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task GetProjectById()
        {
            Console.Clear();
            Console.Write("Ange projekt-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID-format.");
                Console.ReadKey();
                return;
            }

            var project = await _projectService.GetProjectByIdAsync(id);
            if (project != null)
            {
                Console.WriteLine($"Projekt: ID: {project.Id}, Projektnummer: {project.ProjectNumber}, Namn: {project.Name}");
            }
            else
            {
                Console.WriteLine("Projektet hittades inte.");
            }
            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task UpdateCustomer()
        {
            Console.Clear();
            Console.Write("Ange kund-ID som du vill uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID-format.");
                Console.ReadKey();
                return;
            }

            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                Console.WriteLine("Kunden hittades inte.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Redigerar kund: ID {customer.Id}, Namn: {customer.CustomerName}");
            Console.WriteLine("Tryck Enter för att behålla det nuvarande värdet.");

            Console.Write($"Nytt kundnamn ({customer.CustomerName}): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                customer.CustomerName = input;
            }

            bool success = await _customerService.UpdateCustomerAsync(customer);
            if (success)
                Console.WriteLine("Kunden har uppdaterats!");
            else
                Console.WriteLine("Uppdatering misslyckades.");

            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task UpdateProject()
        {
            Console.Clear();
            Console.Write("Ange ID för projektet du vill uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID-format.");
                Console.ReadKey();
                return;
            }

            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                Console.WriteLine("Projektet hittades inte.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Uppdaterar projekt: {project.ProjectNumber} - {project.Name}");
            Console.WriteLine("Tryck Enter för att behålla nuvarande värde.");

            Console.Write($"Nytt namn ({project.Name}): ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                project.Name = input;

            Console.Write($"Nytt startdatum ({project.StartDate.ToShortDateString()}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && DateTime.TryParse(input, out DateTime newStart))
                project.StartDate = newStart;

            Console.Write($"Nytt slutdatum ({project.EndDate.ToShortDateString()}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && DateTime.TryParse(input, out DateTime newEnd))
                project.EndDate = newEnd;

            Console.Write($"Ny projektansvarig ({project.ProjectManager}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                project.ProjectManager = input;

            Console.Write($"Ny tjänstebeskrivning ({project.ServiceDescription}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                project.ServiceDescription = input;

            Console.Write($"Nytt totalpris ({project.TotalPrice}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && decimal.TryParse(input, out decimal newPrice))
                project.TotalPrice = newPrice;

            Console.Write($"Ny status ({project.Status}) (EjPaborjat, PaaGorande, Avslutat): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                project.Status = input;

            await _projectService.UpdateProjectAsync(project);
            Console.WriteLine("Projektet har uppdaterats! Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task DeleteCustomer()
        {
            Console.Clear();
            Console.Write("Ange kund-ID att ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID-format.");
                Console.ReadKey();
                return;
            }

            bool success = await _customerService.DeleteCustomerAsync(id);
            if (success)
                Console.WriteLine("Kunden har tagits bort.");
            else
                Console.WriteLine("Kunden kunde inte tas bort eller hittades inte.");

            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }

        private async Task DeleteProject()
        {
            Console.Clear();
            Console.Write("Ange projekt-ID att ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID-format.");
                Console.ReadKey();
                return;
            }

            bool success = await _projectService.DeleteProjectAsync(id);
            if (success)
                Console.WriteLine("Projektet har tagits bort.");
            else
                Console.WriteLine("Projektet kunde inte tas bort eller hittades inte.");

            Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
            Console.ReadKey();
        }
    }
}
