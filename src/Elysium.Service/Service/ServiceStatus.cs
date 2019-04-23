namespace Elysium
{
    public class ServiceStatus
    {
        public ServiceState State { get; set; } = ServiceState.NotStarted;

        public string Description { get; set; }

        public void Report(ServiceState state, string description)
        {
            State = state;
            Description = description;
        }
    }


    public enum ServiceState
    {
        NotStarted, 
        Starting,
        Started,
        Error
    }


}