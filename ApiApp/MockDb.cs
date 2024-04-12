namespace ApiApp;

public class MockDb : IMockDb
{
    private static ICollection<Animal?> _animals;
    private static ICollection<Visit> _visits;

    public MockDb()
    {
        _animals = new List<Animal?>
        {
            new Animal { Id = 1, Name = "Fido", Category = "Dog", Weight = 10.5, furColor = "Brown" },
            new Animal { Id = 2, Name = "Whiskers", Category = "Cat", Weight = 5.2, furColor = "White" },
            new Animal { Id = 3, Name = "Fluffy", Category = "Rabbit", Weight = 2.3, furColor = "Gray" },
            new Animal { Id = 4, Name = "Tweety", Category = "Bird", Weight = 0.5, furColor = "Yellow" }
        };
        _visits = new List<Visit>()
        {
            new Visit { Id = 1, AnimalId = 1, Date = DateTime.Now.AddDays(-10), Description = "Routine checkup", Price = 50.0 },
            new Visit { Id = 2, AnimalId = 2, Date = DateTime.Now.AddDays(-5), Description = "Vaccination", Price = 30.0 },
            new Visit { Id = 3, AnimalId = 2, Date = DateTime.Now.AddDays(-16), Description = "Blood test", Price = 100.0 },
            new Visit { Id = 4, AnimalId = 3, Date = DateTime.Now.AddDays(-3), Description = "Nail trimming", Price = 20.0 }
        };
    }

    public ICollection<Animal?> GetAll()
    {
        return _animals;
    }

    public Animal? GetAnimalById(int id)
    {
        return _animals.FirstOrDefault(animal => animal?.Id == id); // Sprawdzanie nulli w kolekcji
    }

    public void AddAnimal(Animal student)
    {
        _animals.Add(student);
    }

    public bool EditAnimalById(int id, Animal? animal)
    {
        var tmpAnimal = GetAnimalById(id);
        if (tmpAnimal is null)
        {
            return false;
        }

        _animals.Remove(tmpAnimal);
        _animals.Add(animal);
        return true;
    }

    public bool RemoveAnimalById(int id)
    {
        var tmpAnimal = GetAnimalById(id);
        if (tmpAnimal is null)
        {
            return false;
        }
        return _animals.Remove(tmpAnimal);
    }

    public ICollection<Visit> GetVisitsByAnimalId(int animalId)
    {
        return _visits.Where(visit => visit.AnimalId == animalId).ToList();
    }

    public void AddVisit(Visit visit)
    {
        _visits.Add(visit);
    }
}