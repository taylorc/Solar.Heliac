using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solar.Heliac.Application.Features.Teams.Commands.AddHeroToTeam;
using Solar.Heliac.Application.Features.Teams.Commands.CompleteMission;
using Solar.Heliac.Application.Features.Teams.Commands.CreateTeam;
using Solar.Heliac.Application.Features.Teams.Commands.ExecuteMission;
using Solar.Heliac.Application.Features.Teams.Queries.GetAllTeams;
using Solar.Heliac.Application.Features.Teams.Queries.GetTeam;
using Solar.Heliac.Domain.Heroes;
using Solar.Heliac.Domain.Teams;
using Solar.Heliac.WebApi.Extensions;
using TeamDto = Solar.Heliac.Application.Features.Teams.Queries.GetAllTeams.TeamDto;

namespace Solar.Heliac.WebApi.Features;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this WebApplication app)
    {
        var group = app.MapApiGroup("teams");

        group
            .MapPost("/", async (ISender sender, CreateTeamCommand command, CancellationToken ct) =>
            {
                await sender.Send(command, ct);
                return Results.Created();
            })
            .WithName("CreateTeam")
            .ProducesPost();

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) =>
            {
                var results = await sender.Send(new GetAllTeamsQuery(), ct);
                return Results.Ok(results);
            })
            .WithName("GetAllTeams")
            .ProducesGet<TeamDto[]>();

        group
            .MapPost("/{teamId:guid}/heroes/{heroId:guid}",
                async (ISender sender, Guid teamId, Guid heroId, CancellationToken ct) =>
                {
                    var command = new AddHeroToTeamCommand(teamId, heroId);
                    await sender.Send(command, ct);
                    return Results.Ok();
                })
            .WithName("AddHeroToTeam")
            .ProducesPost();

        group
            .MapGet("/{teamId:guid}",
                async (ISender sender, Guid teamId, CancellationToken ct) =>
                {
                    var query = new GetTeamQuery(teamId);
                    var results = await sender.Send(query, ct);
                    return Results.Ok(results);
                })
            .WithName("GetTeam")
            .ProducesGet<TeamDto>();

        group
            .MapPost("/{teamId:guid}/execute-mission",
                async (ISender sender, Guid teamId, [FromBody] ExcuteMissionRequest request, CancellationToken ct) =>
                {
                    var command = new ExecuteMissionCommand(teamId, request.Description); await sender.Send(command, ct);
                    return Results.Ok();
                })
            .WithName("ExecuteMission")
            .ProducesPost();

        group
            .MapPost("/{teamId:guid}/complete-mission",
                async (ISender sender, Guid teamId, CancellationToken ct) =>
                {
                    var command = new CompleteMissionCommand(teamId);
                    await sender.Send(command, ct);
                    return Results.Ok();
                })
            .WithName("CompleteMission")
            .ProducesPost();
    }
}

public record ExcuteMissionRequest(string Description);