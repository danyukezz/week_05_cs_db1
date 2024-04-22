public interface IPersonService
{
    Person AddPerson(Person person);
    void DeletePerson(int PersonID);
    List<Person> GetAllPerson();
    Person GetPersonById(int PersonID);
    void UpdatePerson(Person person);
}
public class PersonService : IPersonService
{
    private IPersonRepository _personRepository;
    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    public Person AddPerson(Person person)
    {
        return _personRepository.AddPerson(person);
    }
    public void DeletePerson(int PersonID)
    {
        _personRepository.DeletePerson(PersonID);
    }
    public List<Person> GetAllPerson()
    {
        return _personRepository.GetAllPerson();
    }
    public Person GetPersonById(int PersonID)
    {
        return _personRepository.GetPersonById(PersonID);
    }
    public void UpdatePerson(Person person)
    {
        _personRepository.UpdatePerson(person);
    }
}