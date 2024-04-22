IPersonRepository personRepository = new PersonRepository();
IPersonService personService = new PersonService(personRepository);

bool isSelected = false;
while (!isSelected)
{
    Console.Clear();
    Console.WriteLine("\u001B[1mPerson App\u001b[0m");
    Console.WriteLine("1 - Get all persons from DB");
    Console.WriteLine("2 - Get person by ID");
    Console.WriteLine("3 - Add a new person");
    Console.WriteLine("4 - Delete an existing person");
    Console.WriteLine("5 - Update an existing person");
    Console.WriteLine("6 - Exit");
    int option = int.Parse(Console.ReadLine());
    ConsoleKeyInfo key;
    switch (option)
    {
        case 1:
            Console.Clear();
            List<Person> listPersons = personService.GetAllPerson();

            if (listPersons.Count == 0)
            {
                Console.WriteLine("No persons available");
            }
            else
            {
                foreach (Person person in listPersons)
                {
                    Console.WriteLine($"{person.Id}, {person.Name}, {person.Age}, {person.Email}");
                }
            }
            Console.WriteLine("Press Enter to go to the main menu");
            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    break;
            }
        break;
        case 2:
            Console.Clear();
            Console.WriteLine("Enter ID:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Person searchedPerson = personService.GetPersonById(id);
                if (searchedPerson.Id != id)
                {
                    Console.WriteLine("No matching person by id found :(");
                }
                else
                {
                    Console.WriteLine($"{searchedPerson.Name}, {searchedPerson.Age}, {searchedPerson.Email}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer ID.");
            }
            Console.WriteLine("Press Enter to go to the main menu");
            Console.ReadLine();
        break;
        case 3:
            Console.Clear();
            Person myNewPerson = new Person();
            Console.WriteLine("What is the name?");
            myNewPerson.Name = Console.ReadLine();
            Console.WriteLine("What is the Age?");
            myNewPerson.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the Email?");
            myNewPerson.Email = Console.ReadLine(); 

            Person isSuccessful = personService.AddPerson(myNewPerson);

            Task.Delay(200).Wait();
            if (isSuccessful != null)
            {
                Console.WriteLine("Person Added!");
            }
            else
            {
                Console.WriteLine("Error, Person NOT Added!");
            }
            Task.Delay(600).Wait();
        break;
        case 4:
            Console.Clear();
            Console.WriteLine("What is the id of person you want to delete?");
            int deletionId = int.Parse(Console.ReadLine());
            Person searchedPersonToDelete = personService.GetPersonById(deletionId);
            personService.DeletePerson(deletionId);
            Task.Delay(200).Wait();
            if (deletionId == searchedPersonToDelete.Id)
            {
                Console.WriteLine("Deletion Succesfull!");
            }
            else
            {
                Console.WriteLine("Deletion NOT Succesfull!");
            }
            Task.Delay(600).Wait();
        break;
        case 5:
            Console.Clear();
            Console.WriteLine("What is the id of person you want to update");
            int updatedId = int.Parse(Console.ReadLine());
            Person searchedPersonToUpdate = personService.GetPersonById(updatedId);
            if (updatedId == searchedPersonToUpdate.Id)
            {
                Console.WriteLine("Enter new name");
                searchedPersonToUpdate.Name = Console.ReadLine();
                Console.WriteLine("Enter new age");
                searchedPersonToUpdate.Age = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter new email");
                searchedPersonToUpdate.Email = Console.ReadLine();
                personService.UpdatePerson(searchedPersonToUpdate);
                Console.WriteLine("Updating Succesfull!");
            }
            else
            {
                Console.WriteLine("Updating NOT Succesfull or person not found!");
            }
            Task.Delay(600).Wait();
        break;
        case 6:
            isSelected = true;
            Console.Clear();
            Environment.Exit(0);
            break;
        default:
            break;
    }
}