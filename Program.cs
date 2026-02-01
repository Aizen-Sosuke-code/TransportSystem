using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportSystem
{
    // -----------------------------
    // 1. Базовый класс
    // -----------------------------
    public abstract class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        protected Vehicle(string brand, string model, int year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }

        public virtual void StartEngine()
        {
            Console.WriteLine($"{Brand} {Model}: Двигатель запущен.");
        }

        public virtual void StopEngine()
        {
            Console.WriteLine($"{Brand} {Model}: Двигатель остановлен.");
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Марка: {Brand}, Модель: {Model}, Год: {Year}");
        }
    }

    // -----------------------------
    // 2. Производный класс Car
    // -----------------------------
    public class Car : Vehicle
    {
        public int Doors { get; set; }
        public string Transmission { get; set; }

        public Car(string brand, string model, int year, int doors, string transmission)
            : base(brand, model, year)
        {
            Doors = doors;
            Transmission = transmission;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Тип: Автомобиль, Дверей: {Doors}, Трансмиссия: {Transmission}");
        }
    }

    // -----------------------------
    // 2. Производный класс Motorcycle
    // -----------------------------
    public class Motorcycle : Vehicle
    {
        public string BodyType { get; set; }
        public bool HasBox { get; set; }

        public Motorcycle(string brand, string model, int year, string bodyType, bool hasBox)
            : base(brand, model, year)
        {
            BodyType = bodyType;
            HasBox = hasBox;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Тип: Мотоцикл, Кузов: {BodyType}, Бокс: {(HasBox ? "Есть" : "Нет")}");
        }
    }

    // -----------------------------
    // 3. Класс Garage (Композиция)
    // -----------------------------
    public class Garage
    {
        public string Name { get; set; }
        private List<Vehicle> vehicles;

        public Garage(string name)
        {
            Name = name;
            vehicles = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            Console.WriteLine($"Транспорт добавлен в гараж '{Name}'");
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            Console.WriteLine($"Транспорт удален из гаража '{Name}'");
        }

        public List<Vehicle> GetVehicles()
        {
            return vehicles;
        }

        public void ShowAllVehicles()
        {
            Console.WriteLine($"\nГараж: {Name}");
            foreach (var v in vehicles)
            {
                v.ShowInfo();
                Console.WriteLine("---------------------");
            }
        }
    }

    // -----------------------------
    // 3. Класс Fleet (Композиция)
    // -----------------------------
    public class Fleet
    {
        private List<Garage> garages;

        public Fleet()
        {
            garages = new List<Garage>();
        }

        public void AddGarage(Garage garage)
        {
            garages.Add(garage);
            Console.WriteLine("Гараж добавлен в автопарк");
        }

        public void RemoveGarage(Garage garage)
        {
            garages.Remove(garage);
            Console.WriteLine("Гараж удален из автопарка");
        }

        public void FindVehicle(string brand)
        {
            Console.WriteLine($"\nПоиск транспорта марки: {brand}");
            foreach (var garage in garages)
            {
                foreach (var vehicle in garage.GetVehicles())
                {
                    if (vehicle.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Найден в гараже: {garage.Name}");
                        vehicle.ShowInfo();
                        return;
                    }
                }
            }
            Console.WriteLine("Транспорт не найден");
        }

        public void ShowAll()
        {
            foreach (var garage in garages)
            {
                garage.ShowAllVehicles();
            }
        }
    }

    // -----------------------------
    // 4. Тестирование программы
    // -----------------------------
    class Program
    {
        static void Main(string[] args)
        {

            Fleet fleet = new Fleet();

            Garage garage1 = new Garage("Центральный гараж");
            Garage garage2 = new Garage("Южный гараж");

            fleet.AddGarage(garage1);
            fleet.AddGarage(garage2);

            Car car1 = new Car("Toyota", "Camry", 2022, 4, "Автомат");
            Car car2 = new Car("BMW", "X5", 2023, 4, "Автомат");

            Motorcycle moto1 = new Motorcycle("Yamaha", "R1", 2021, "Спортивный", false);
            Motorcycle moto2 = new Motorcycle("Honda", "Africa Twin", 2020, "Туристический", true);

            garage1.AddVehicle(car1);
            garage1.AddVehicle(moto1);

            garage2.AddVehicle(car2);
            garage2.AddVehicle(moto2);

            fleet.ShowAll();

            Console.WriteLine("\nТест двигателя:");
            car1.StartEngine();
            moto1.StartEngine();
            car1.StopEngine();

            fleet.FindVehicle("BMW");

            garage1.RemoveVehicle(moto1);

            Console.WriteLine("\nПосле удаления:");
            fleet.ShowAll();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
