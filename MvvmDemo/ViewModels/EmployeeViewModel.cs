using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using MvvmDemo.Models;
using MvvmDemo.Commands;
using System.Collections.ObjectModel;

namespace MvvmDemo.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChangedImplementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<Employee> employeesList;
        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; 
                onPropertyChanged("EmployeesList"); }

        }
        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>( ObjEmployeeService.GetAll());
        }
        #endregion

        #region AddOperation

        private Employee currentEmployee;
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; onPropertyChanged("Message"); }
        }

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value;
                onPropertyChanged("CurrentEmployee");
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get {return saveCommand; }
           
        }

        public void Save()
        {
            try { var IsSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                {
                    Message = "IsSaved";
                }
                else
                {
                    Message = "Save Operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);

                if (ObjEmployee != null)
                {
                    CurrentEmployee.Name = ObjEmployee.Name;
                    CurrentEmployee.Age = ObjEmployee.Age;
                    Message = "Employee found";
                }
                else
                {
                    Message = "Employee not found";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion

        #region UpdateOperation

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand 
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            {
                try
                {
                    var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
            
                    if (IsUpdated)
                    {
                        Message = "Employee Updated";
                        LoadData();
                    }
                    else
                    {
                        Message = "Update Operation failed";
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }

        #endregion

        #region DeleteOperation
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
        }

        public void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeeService.Delete(CurrentEmployee.Id);
                if (IsDeleted)
                {
                    Message = "Employee Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete Operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion


    }
}
