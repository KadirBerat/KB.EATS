namespace KB.EATS.WebUI.Models.ViewModels
{
    public class DashboardWiewModel
    {
        public List<EmployeeData> Employees { get; set; } //çalışan listesi
    }

    //çalışan veri modeli
    public class EmployeeData
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
