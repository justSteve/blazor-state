﻿namespace BlazorState.Features.Routing
{
  using System.Threading;
  using System.Threading.Tasks;
  using BlazorState;
  using Microsoft.AspNetCore.Blazor.Services;

  internal class ChangeRouteHandler : RequestHandler<ChangeRouteRequest, RouteState>
  {
    public ChangeRouteHandler(
      IStore aStore,
      IUriHelper aUriHelper
      ) : base(aStore)
    {
      UriHelper = aUriHelper;
    }

    private RouteState RouteState => Store.GetState<RouteState>();

    private IUriHelper UriHelper { get; }

    public override Task<RouteState> Handle(ChangeRouteRequest aChangeRouteRequest, CancellationToken aCancellationToken)
    {
      string newAbsoluteUri = UriHelper.ToAbsoluteUri(aChangeRouteRequest.NewRoute).ToString();
      if (RouteState.Route != newAbsoluteUri)
      {
        RouteState.Route = newAbsoluteUri;
        UriHelper.NavigateTo(newAbsoluteUri);
      }
      return Task.FromResult(RouteState);
    }
  }
}