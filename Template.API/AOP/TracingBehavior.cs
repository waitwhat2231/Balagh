
using MediatR;
using System.Diagnostics;
using Template.Application.Users;
namespace Template.API.AOP;
public class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ActivitySource _activitySource;
    private readonly IUserContext _userContext;

    public TracingBehavior(ActivitySource activitySource, IUserContext userContext)
    {
        _activitySource = activitySource;
        _userContext = userContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        using var activity = _activitySource.StartActivity(requestName);

        activity?.SetTag("request_type", requestName);
        activity?.SetTag("request_namespace", typeof(TRequest).Namespace);
        if (_userContext.GetCurrentUser() != null)
        {
            activity.SetTag("currentUser.Id", _userContext.GetCurrentUser()!.Id);
        }
        activity?.SetTag("timestamp.start", DateTime.UtcNow);
        activity?.AddEvent(new ActivityEvent("HandlerStarted"));


        try
        {
            var response = await next();
            activity?.SetTag("status", "success");
            return response;
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.AddException(ex);
            throw;
        }
        finally
        {
            activity?.SetTag("timestamp.end", DateTime.UtcNow);
        }
    }
}
