namespace ApiApp;

public interface IMockDb
{
    public ICollection<Animal?> GetAll();
    public Animal? GetAnimalById(int id);
    public void AddAnimal(Animal student);
    public bool EditAnimalById(int id, Animal animal);
    public bool RemoveAnimalById(int id);


    public ICollection<Visit> GetVisitsByAnimalId(int animalId);
    public void AddVisit(Visit visit);
}