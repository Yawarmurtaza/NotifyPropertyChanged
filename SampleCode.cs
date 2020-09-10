public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public DateTime DoB { get; set; }
    public decimal AnnualSalary { get; set; }
}


public class EmployeeModel : INotifyPropertyChanged
{
    private string firstName;
    public int Id { get; set; }
    public string FirstName
    {
        get => firstName;
        set => SetField(ref firstName, value, nameof(FirstName));
    }
    public DateTime DoB { get; set; }
    public long AnnualSalary { get; set; }

    public bool EmployeeFirstNameChanged { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;


    protected void SetField<T>(ref T field, T value, string propertyName)
    {
        if (field != null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                EmployeeFirstNameChanged = true;
                //OnPropertyChanged(propertyName);
            }
        }

        field = value;
    }

    
    //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
}



// client code:
static void Main(string[] args)
{
    var mapper = new Mapper(ConfigureAutoMapper());
    
    Employee emp = new Employee();
    emp.FirstName = "Yawar"; // event fires...
    emp.Id = 33;
    emp.AnnualSalary = 45000.00m;
    emp.DoB = new DateTime(1900, 01, 10);

    EmployeeModel model = new EmployeeModel();
    mapper.Map<Employee, EmployeeModel>(emp, model);

    // Note: model.EmployeeFirstNameChanged should be false.

    emp.FirstName = "Yawar2";
    mapper.Map<Employee, EmployeeModel>(emp, model);

    // Note: model.EmployeeFirstNameChanged should be true.
}

static MapperConfiguration ConfigureAutoMapper()
{
    return new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeModel>());
}
