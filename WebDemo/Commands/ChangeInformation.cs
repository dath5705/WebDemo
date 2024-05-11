namespace WebDemo.Commands

{
    public class ChangeInformation : AddInformation
    {
        public int Id { get; set; } = 0;
        public int SexId { get; set; } = 1;
        public DateTime DateTime { get; set; }= DateTime.Today;
    }
}
