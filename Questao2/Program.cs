using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questao2;
using Questao2.Models;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

public class Program
{
    public async static Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public async static Task<int> getTotalScoredGoals(string team, int year)
    {
        var totalGoalsHost = await getTotalScoredGoalsByTypeTeam(team, TYPE_TEAM.HOST, year);
        var totalGoalsVisitor = await getTotalScoredGoalsByTypeTeam(team, TYPE_TEAM.VISITOR, year);
        
        return totalGoalsHost + totalGoalsVisitor;
    }

    private async static Task<int> getTotalScoredGoalsByTypeTeam(string team, TYPE_TEAM typeTeam, int year)
    {
        var total = 0;

        try
        {
            using (var testClient = new ServiceAPI(new MediaTypeWithQualityHeaderValue("application/json")))
            {
                var page = 1;
                var total_pages = 1;

                while (page <= total_pages)
                {
                    var url = getUrlComplete(team, typeTeam, year, page);
                    var result = JsonConvert.DeserializeObject<ResponseAPI>(await testClient.GetAsync(url));

                    total_pages = result.total_pages;
                    total += result.data.Sum(x => typeTeam.Equals(TYPE_TEAM.HOST) ? x.team1goals : x.team2goals);
                    page++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Error calculating number of goals - error: {0}", ex.Message));
        }
        
        return total;
    }

    private static string getUrlComplete(string team, TYPE_TEAM typeTeam, int year, int page)
    {
        var urlBase = "https://jsonmock.hackerrank.com/api/football_matches";
                        
        return string.Format("{0}?year={1}&page={2}&team{3}={4}", urlBase, year, 
                                       page, (int)typeTeam, team);
    }

}