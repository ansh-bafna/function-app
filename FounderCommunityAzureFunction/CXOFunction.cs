using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Azure;

namespace FounderCommunityAzureFunction
{
    public class CXOFunction
    {
        [FunctionName("GetCXOByAreaOfExpertise")]
        public static async Task<IActionResult> GetCXOByAreaOfExpertise(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cxo/{areaofexpertise}")] HttpRequest req, ILogger log, string areaofexpertise)
        {
            List<CXOModel> cxolist = new List<CXOModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {

                    connection.Open();
                    var query = @"Select * from [dbo].[CXO] INNER JOIN [dbo].[CXOExperience] ON [dbo].[CXOExperience].AccountId=[dbo].[CXO].AccountId WHERE [dbo].[CXOExperience].AreaOfExpertise=@areaofexpertise";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@areaofexpertise", areaofexpertise);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        CXOModel cxo = new CXOModel()
                        {
                            AccountId = reader["AccountId"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            HoursContributed = (int)reader["HoursContributed"],
                            Company= reader["Company"].ToString(),
                            profilePic= reader["profilePic"].ToString(),
                            coverPic= reader["coverPic"].ToString(),
                            education= reader["education"].ToString(),
                            FHDoj= (DateTime)reader["FHDoj"],
                            yearsOfExperience = (int)reader["yearsOfExperience"],
                            numStartupsHelped= (int)reader["numStartupsHelped"],
                            investmentDone= (int)reader["investmentDone"],
                            responseTimeInMin= (int)reader["responseTimeInMin"],
                            email = reader["email"].ToString(),
                            linkedin= reader["linkedin"].ToString(),
                            cxoDescription= reader["cxoDescription"].ToString()
                        };
                        cxolist.Add(cxo);
                    }
                    
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (cxolist.Count > 0)
            {
                return new OkObjectResult(cxolist);
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
    
}
