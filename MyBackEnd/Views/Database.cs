using System;
using System.Collections.Generic;
using System.Data;
using MyBackEnd.Models;
using Npgsql;

namespace MyBackEnd.Views
{
    public class Database : IDatabase
    {
        private string connectionString;
        private NpgsqlConnection connection;

        public Database()
        {
            this.connectToDatabase();
        } // https:localhost:5001/api/cars/1

        private void connectToDatabase()
        {
            connectionString = $"Host=localhost; port=5432; Username=postgres; Password=Admin123; Database=MyDatabase";
            //connectionString = $"Host=my-postgres; port=5432; Username=postgres; Password=Admin123; Database=MyDatabase";
            Console.WriteLine(connectionString);
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            NpgsqlCommand command = new NpgsqlCommand("", connection);
            string sql = "SELECT version()";
            command.CommandText = sql;
            string version = command.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");
        }

        public bool createCarsTable()
        {
            NpgsqlCommand command = new NpgsqlCommand("", connection);

            command.CommandText = "DROP TABLE IF EXISTS cars";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY, 
                    name VARCHAR(255), price INT)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            command.ExecuteNonQuery();

            Console.WriteLine($"New Table created called cars");
            return true;
        }

        public List<Car> getCars()
        {
            NpgsqlCommand command = new NpgsqlCommand("", connection);
            command.CommandText = $"SELECT * FROM cars";
            NpgsqlDataReader reader = command.ExecuteReader();
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
            List<Car> cars = this.RunFunction(1);
            //List<Car> cars = this.getCars();
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
            NpgsqlCommand command = new NpgsqlCommand("", connection);
            Console.WriteLine("Writing to Db");
            string insertCommand = $"INSERT INTO cars(name, price) VALUES('{car.name}', {car.price})";
            Console.WriteLine($"Insert Command: {insertCommand}");
            command.CommandText = insertCommand;
            command.ExecuteNonQuery();
            return true;
        }

        public bool RunProcedure(string name)
        {
            Console.WriteLine("Calling procedure.");
            NpgsqlCommand storeProcCommand = new NpgsqlCommand("", connection);
            string callProcedureCommand = $"CALL public.addcar('${name}')";
            storeProcCommand.CommandText = callProcedureCommand;
            Console.WriteLine($"Calling Proc Command: {callProcedureCommand}");
            storeProcCommand.ExecuteNonQuery();
            return true;
        }

        public List<Car> RunFunction(int greaterThan)
        {
            NpgsqlCommand functionCommand = new NpgsqlCommand("public.count_cars_higher_than", connection);
            functionCommand.CommandType = CommandType.StoredProcedure;
            functionCommand.Parameters.AddWithValue("par_minimum_price", greaterThan);

            NpgsqlDataReader reader = functionCommand.ExecuteReader();
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
