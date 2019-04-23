namespace Elysium
{
    public interface IService
    {
        ServiceOptions Options { get; } 

        ServiceStatus Status { get; } 
    }

}