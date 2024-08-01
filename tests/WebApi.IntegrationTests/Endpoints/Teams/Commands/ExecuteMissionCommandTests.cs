using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Solar.Heliac.Domain.Teams;
using Solar.Heliac.WebApi.Features;
using System.Net;
using System.Net.Http.Json;
using WebApi.IntegrationTests.Common.Factories;
using WebApi.IntegrationTests.Common.Fixtures;

namespace WebApi.IntegrationTests.Endpoints.Teams.Commands;

public class ExecuteMissionCommandTests(TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task Command_ShouldExecuteMission()
    {
        // Arrange
        var hero = HeroFactory.Generate();
        var team = TeamFactory.Generate();
        team.AddHero(hero);
        await AddEntityAsync(team);
        var teamId = team.Id.Value;
        var client = GetAnonymousClient();
        var request = new ExcuteMissionRequest("Save the world");

        // Act
        var result = await client.PostAsJsonAsync($"/api/teams/{teamId}/execute-mission", request);

        // Assert
        var updatedTeam = await GetQueryable<Team>()
            .WithSpecification(new TeamByIdSpec(team.Id))
            .FirstOrDefaultAsync();
        var mission = updatedTeam!.Missions.First();

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedTeam!.Missions.Should().HaveCount(1);
        updatedTeam.Status.Should().Be(TeamStatus.OnMission);
        mission.Status.Should().Be(MissionStatus.InProgress);
    }
}