namespace Elysium
{
    public abstract class ServiceBase : IService
    {
        public ServiceOptions Options { get; set; } = new ServiceOptions();

        public ServiceStatus Status { get; set; } = new ServiceStatus();

    }


}