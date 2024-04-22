using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Math.EC.Endo;

public interface IPersonRepository
{
    Person AddPerson(Person person);
    void DeletePerson(int PersonID);
    List<Person> GetAllPerson();
    Person GetPersonById(int PersonID);
    void UpdatePerson(Person person);
}
public class PersonRepository : IPersonRepository
{
    private MySqlConnection CreateConnection()
    {
        string connectionString = "server=localhost;database=Demo01;user=root;pwd=root1234";
        MySqlConnection connection = new MySqlConnection(connectionString);
        return connection;
    }
    
    public Person AddPerson(Person person)
    {
        try
        {
            MySqlConnection mysqlConnection = CreateConnection();
            mysqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO Person(Name, Age, Email) VALUES(@Name, @Age, @Email)", mysqlConnection);
            mySqlCommand.Parameters.AddWithValue("@Name", person.Name);
            mySqlCommand.Parameters.AddWithValue("@Age", person.Age);
            mySqlCommand.Parameters.AddWithValue("@Email", person.Email);
            // mySqlCommand.ExecuteNonQuery();
            
            int id = Convert.ToInt32(mySqlCommand.ExecuteScalar());
            person.Id = id;
            return person;
        }
        catch(Exception ex)
        {
            Console.Write(ex);
            return null;
        }

    }
    public void DeletePerson(int PersonID)
    {
        MySqlConnection mysqlConnection = CreateConnection();
        mysqlConnection.Open();
        MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM Person WHERE Id = @PersonId", mysqlConnection);
        mySqlCommand.Parameters.AddWithValue("@PersonId", PersonID);
        mySqlCommand.ExecuteNonQuery();
    }
    public List<Person> GetAllPerson()
    {
        List<Person> persons = new List<Person>();
        try
        {
            MySqlConnection mySqlConnection = CreateConnection();
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Person", mySqlConnection);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Person newPerson = new Person();
                newPerson.Id = reader.GetInt32("Id");
                newPerson.Name = reader.GetString("Name");
                newPerson.Age = reader.GetInt32("Age");
                newPerson.Email = reader.GetString("Email");
                persons.Add(newPerson);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
        return persons;
    }
    
    public Person GetPersonById(int PersonID)
    {
        Person lookingPerson = new Person();
        try
        {
            MySqlConnection mySqlConnection = CreateConnection();
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Person WHERE Id = @PersonId", mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("@PersonId", PersonID);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while(reader.Read())
            {
                lookingPerson.Id = reader.GetInt32("Id");
                lookingPerson.Name = reader.GetString("Name");
                lookingPerson.Age = reader.GetInt32("Age");
                lookingPerson.Email = reader.GetString("Email");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
        return lookingPerson;
    }
    public void UpdatePerson(Person person)
    {
        try
        {
            MySqlConnection mySqlConnection = CreateConnection();
            mySqlConnection.Open();
            MySqlCommand mySqlCommand= new MySqlCommand("UPDATE Person SET Name = @Name, Age = @Age, Email = @Email WHERE Id = @PersonID", mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("@PersonId", person.Id);
            mySqlCommand.Parameters.AddWithValue("@Name", person.Name);
            mySqlCommand.Parameters.AddWithValue("@Age", person.Age);
            mySqlCommand.Parameters.AddWithValue("@Email", person.Email);
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while(reader.Read());
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}