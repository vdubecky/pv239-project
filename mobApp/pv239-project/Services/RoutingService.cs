using pv239_project.Models;
using pv239_project.Pages;
using pv239_project.Services.Interfaces;

namespace pv239_project.Services;

public class RoutingService : IRoutingService
{
    public const string ConversationDetailPage = "conversation/detail";
    public const string CreateNewUserPage = "anonymous/new-user";
    
    public IEnumerable<RouteModel> Routes => new List<RouteModel>
    {
        new(ConversationDetailPage, typeof(ConversationDetailPage)),
        new(CreateNewUserPage, typeof(CreateNewUserPage)),
    };
}
