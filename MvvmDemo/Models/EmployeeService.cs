using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MvvmDemo.Models
{
    class EmployeeService
    {
        private static List<Employee> ObjEmployeesList;
        public EmployeeService()
        {
            ObjEmployeesList = new List<Employee>()
            {
                new Employee{Id=101, Name="Syed", Age=28}
            };
        }
        public List<Employee> GetAll()
        {
            return ObjEmployeesList;
        }
        public bool Add(Employee objNewEmployee)
        {
            //age must be between 21 and 58
            if(objNewEmployee.Age<21 || objNewEmployee.Age>58)
            {
                throw new ArgumentException("Invalid age limit for employee");
            }
            ObjEmployeesList.Add(objNewEmployee);
            return true;
        }

        public bool Update(Employee objEmployeeToUpdate)
        {
            bool isUpdated = false;
            for (int i=0; i<ObjEmployeesList.Count;i++)
            {
                if (ObjEmployeesList[i].Id== objEmployeeToUpdate.Id)
                {
                    ObjEmployeesList[i].Name = objEmployeeToUpdate.Name;
                    ObjEmployeesList[i].Age= objEmployeeToUpdate.Age;
                    isUpdated = true;
                    break;

                }
            }
            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            for (int i = 0; i < ObjEmployeesList.Count; i++)
            {
                if (ObjEmployeesList[i].Id == id)
                {
                    ObjEmployeesList.RemoveAt(i);
                    isDeleted = true;
                    break;

                }
            }
            return isDeleted;
        }

        public Employee Search(int id)
        {
            return ObjEmployeesList.FirstOrDefault(e=>e.Id==id);
           
        }
    }
}
