using pv239_project.Models;

namespace pv239_project.Services.Interfaces;

public interface IRoutingService
{
    IEnumerable<RouteModel> Routes { get; }
}
