using System;
using System.Collections.Generic;
using System.Text;

public class Car
{
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public string VIN { get; set; }
    public string Owner { get; set; }

    public override string ToString()
    {
        return $"Model: {Model}, Year: {Year}, Color: {Color}, VIN: {VIN}, Owner: {Owner}";
    }
}

public interface ICarRecordSystem
{
    void AddCar(Car car);
    Car FindCar(string vin);
    string ListAllCars();
    void PrintAllCars();
}

public class CarRecordSystem : ICarRecordSystem
{
    private Dictionary<string, Car> Cars = new Dictionary<string, Car>();

    public void AddCar(Car car)
    {
        if (!Cars.ContainsKey(car.VIN))
        {
            Cars.Add(car.VIN, car);
        }
        else
        {
            throw new ArgumentException("A car with this VIN already exists.");
        }
    }

    public Car FindCar(string vin)
    {
        if (Cars.TryGetValue(vin, out Car car))
        {
            return car;
        }
        else
        {
            throw new ArgumentException("Car not found.");
        }
    }

    public string ListAllCars()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var car in Cars.Values)
        {
            sb.AppendLine(car.ToString());
        }
        return sb.ToString();
    }

    public void PrintAllCars()
    {
        Console.WriteLine(ListAllCars());
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        CarRecordSystem system = new CarRecordSystem();

        // Adding cars
        system.AddCar(new Car { Model = "Tesla Model S", Year = 2022, Color = "Red", VIN = "1HGCM82633A004352", Owner = "John Doe" });
        system.AddCar(new Car { Model = "Toyota Corolla", Year = 2021, Color = "Blue", VIN = "2T1BURHE0EC171892", Owner = "Jane Smith" });

        // Printing all cars
        Console.WriteLine("All cars in the system:");
        system.PrintAllCars();

        // Finding a car
        try
        {
            Car foundCar = system.FindCar("1HGCM82633A004352");
            Console.WriteLine("\nFound car:");
            Console.WriteLine(foundCar);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        // Trying to find a non-existent car
        try
        {
            Car notFoundCar = system.FindCar("NONEXISTENT");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("\n" + e.Message);
        }
    }
}
