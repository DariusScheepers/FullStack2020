using MyBackEnd.Models;
using System.Collections.Generic;

namespace MyBackEnd.Views
{
    public interface IDatabase
    {
        bool addCar(Car car);
        bool createCarsTable();
        List<Car> getCars();
        Car getCarWithID(int id);
        List<Car> RunFunction(int greaterThan);
        bool RunProcedure(string name);
    }
}