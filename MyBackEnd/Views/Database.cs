using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBackEnd.Models;
using Npgsql;

namespace MyBackEnd.Views
{
    public class Database : IDatabase
    {
        private string username { get; set; }
        private string password { get; set; }
        private string database { get; set; }

        private NpgsqlCommand command { get; set; }

        public Database(string username, string password, string database)
        {
            this.username = username;
            this.password = password;
            this.database = database;

            this.connectToDatabase();
        } // https:localhost:5001/api/cars/1

        private void connectToDatabase()
        {
            // string connectionString = $"Host=my-postgres; port=5432; Username={this.username}; Password={this.password}; Database={this.database}";
            string connectionString = $"Host=localhost; port=5432; Username={this.username}; Password={this.password}; Database={this.database}";
            Console.WriteLine(connectionString);
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            this.command = new NpgsqlCommand
            {
                Connection = connection
            };

            string sql = "SELECT version()";
            this.command.CommandText = sql;
            string version = this.command.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");
        }

        public bool createCarsTable()
        {

            this.command.CommandText = "DROP TABLE IF EXISTS cars";
            this.command.ExecuteNonQuery();

            this.command.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY, 
                    name VARCHAR(255), price INT)";
            this.command.ExecuteNonQuery();

            this.command.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
            this.command.ExecuteNonQuery();

            this.command.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
            this.command.ExecuteNonQuery();

            this.command.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            this.command.ExecuteNonQuery();

            Console.WriteLine($"New Table created called cars");
            return true;
        }

        public List<Car> getCars()
        {
            this.command.CommandText = $"SELECT * FROM cars";
            NpgsqlDataReader reader = this.command.ExecuteReader();
            List<Car> cars = new List<Car>();
            while (reader.Read())
            {
                Car car = new Car
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    price = reader.GetInt32(2)
                };
                cars.Add(car);
            }
            return cars;
        }

        public Car getCarWithID(int id)
        {
            Car result = null;
            // List<Car> cars = this.getCars();
            List<Car> cars = this.RunFunction(1);
            foreach (Car car in cars)
            {
                if (car.id == id)
                {
                    result = car;
                }
            }
            return result;
        }

        public bool addCar(Car car)
        {
            Console.WriteLine("Writing to Db");
            string insertCommand = $"INSERT INTO cars(name, price) VALUES('{car.name}', {car.price})";
            Console.WriteLine($"Insert Command: {insertCommand}");
            this.command.CommandText = insertCommand;
            this.command.ExecuteNonQuery();
            return true;
        }

        public bool RunProcedure(string name)
        {
            Console.WriteLine("Calling procedure.");
            string callProcedureCommand = $"CALL public.addcar('${name}')";
            Console.WriteLine($"Calling Proc Command: {callProcedureCommand}");
            this.command.CommandText = callProcedureCommand;
            this.command.ExecuteNonQuery();
            return true;
        }

        public List<Car> RunFunction(int greaterThan)
        {
            this.command.CommandText = $"SELECT * FROM public.count_cars_higher_than(0)";
            NpgsqlDataReader reader = this.command.ExecuteReader();
            List<Car> cars = new List<Car>();
            while (reader.Read())
            {
                Car car = new Car
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    price = reader.GetInt32(2)
                };
                cars.Add(car);
            }
            return cars;
        }
    }

}
