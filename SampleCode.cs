public class Employee : INotifyPropertyChanged
{
    private string firstName;
    public int Id { get; set; }
    public string FirstName
    {
        get { return firstName; }
        set
        {
            SetField(ref firstName, value, nameof(FirstName));
        }
    }
    public DateTime DoB { get; set; }
    public long AnnualSalary { get; set; }

    public bool IsDirty { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;


    protected void SetField<T>(ref T field, T value, string propertyName)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;
            IsDirty = true;
            OnPropertyChanged(propertyName);
        }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


// client code:
Employee emp = new Employee();
emp.FirstName = "Yawar"; // event fires...


emp.FirstName = "Yawar2"; // event fires...
emp.FirstName = "Yawar2"; // no event fires
