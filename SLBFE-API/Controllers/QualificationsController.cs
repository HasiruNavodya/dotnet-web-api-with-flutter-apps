﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace SLBFE_API.Controllers
{
    [Route("jobseekers/")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public QualificationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet, Route("search")]
        public ActionResult SearchByQualifications(bool filterOn, string? olEnglish, string? olScience, string? olMaths, string? alStream, string? alResults, string? hEdu, string? hEduField)
        {
            string query = @"SELECT [NIC]
                      ,[Email]
                      ,[FirstName]
                      ,[LastName]
                      ,[DOB]
                      ,[Address]
                      ,[Profession]
                      ,[Affiliation]
                      ,[Gender]
                      ,[Nationality]
                      ,[MaritalStatus]
                      ,[Validity]
                      ,[PrimaryPhone]
                      ,[OLScience]
                      ,[OLMaths]
                      ,[OLEnglish]
                      ,[ALStream]
                      ,[ALResults]
                      ,[ALEnglish]
                      ,[HigherEducation]
                      ,[HigherEducationField]
                  FROM [dbo].[JSUserQualifications]";

            if (filterOn) {

                query = query + " WHERE 1=1";

                if (olEnglish != "Any") { query = query + $" AND [OLEnglish] = '{olEnglish}'"; }
                if (olScience != "Any") { query = query + $" AND [OLScience] = '{olScience}'"; }
                if (olMaths != "Any") { query = query + $" AND [OLMaths] = '{olMaths}'"; }
                if (alStream != "Any") { query = query + $" AND [ALStream] = '{alStream}'"; }
                if (alResults != "Any") { query = query + $" AND [ALResults] = '{alResults}'"; }
                if (hEdu != "Any") { query = query + $" AND [HigherEducation] = '{hEdu}'"; }
                if (hEduField != "Any") { query = query + $" AND [HigherEducationField] = '{hEduField}'"; }
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SLBFEDB");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok(table);
        }

    }
}
