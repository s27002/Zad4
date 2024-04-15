using Microsoft.AspNetCore.Mvc;
using Zad4.Models;

namespace Zad4.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{

    public static readonly List<Animal> _animals = new List<Animal>()
    {
        new Animal { id = 1, name = "Bill", category = "Dog", weight = 12.5, color = "white" },
        new Animal { id = 2, name = "Mike", category = "Cat", weight = 6.7, color = "black" },
        new Animal { id = 3, name = "Reksio", category = "Dog", weight = 10, color = "brown" },
        new Animal { id = 4, name = "Golden", category = "Rabbit", weight = 2.4, color = "white" }
    };

    public static readonly List<Visit> _visits = new List<Visit>()
    {
        new Visit() { date = "2020-03-01", animal = "Reksio", desciption = "examination", cost = 200 },
        new Visit() { date = "2021-10-22", animal = "Mike", desciption = "examination", cost = 40 }
    };

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} not found");
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult EditAnimal(int id, Animal animal)
    {
        var animalEdited = _animals.FirstOrDefault(s => s.id == id);
        if (animalEdited == null)
        {
            return NotFound($"Student with id {id} not found");
        }

        _animals.Remove(animalEdited);
        _animals.Add(animal);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalDeleted = _animals.FirstOrDefault(s => s.id == id);
        if (animalDeleted == null)
        {
            return NoContent();
        }

        _animals.Remove(animalDeleted);
        return NoContent();
    }

    [HttpGet("{name}")]
    public IActionResult GetAnimalVisits(string name)
    {
        List<Visit> foundAnimals = _visits.Where(e => e.animal.Equals(name)).ToList();
        if (foundAnimals.Count == 0)
            return NotFound($"There is no visits for {name}");
        return Ok(foundAnimals);
    }
    
    [HttpPost]
    public IActionResult AddVisit(Visit visit)
    {
        _visits.Add(visit);
        return StatusCode(StatusCodes.Status201Created);
    }
}