﻿
using ArtMuseum.Data;
using ArtMuseum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtMuseum.Services
{
    public class EmployeeService
    {
        //POST
        public bool NewEmployee(EmployeeCreate model)
        {
            Employee newEmployee = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Location = model.Location,
                Position = model.Position,
                Id = model.Id
            };

            using (var db = new ApplicationDbContext())
            {
                db.Employees.Add(newEmployee);
                return db.SaveChanges() == 1;
            }
        }

        //GET--ALL
        public IEnumerable<EmployeeListItem> GetEmployeesAtMuseum(int museumId)
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                db.Employees.Where(e => e.Location == museumId).Select(e => new EmployeeListItem
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Position = e.Position,
                });
                return query.ToArray();
            }
        }

        //GET--SPECIFIC
        public EmployeeDetails GetEmployeeById(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var employee =
                db.Employees.Single(e => e.Id == id);
                return new EmployeeDetails
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Location = employee.Location,
                    Position = employee.Position,
                };
            }
        }

        //PUT
        public bool UpdateEmployee(EmployeeEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var employee =
                db.Employees.Single(e => e.DbId == model.DbId);
                employee.Id = model.Id;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Location = model.Location;
                employee.Position = model.Position;

                return db.SaveChanges() == 1;
            }
        }

        //DELETE
        public bool DeleteEmployee(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var employee =
                db.Employees.Single(e => e.Id == id);

                db.Employees.Remove(employee);

                return db.SaveChanges() == 1;
            }
        }

    }
}