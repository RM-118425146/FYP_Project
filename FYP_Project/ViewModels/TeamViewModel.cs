using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using FYP_Project.Models;
using Dapper;
using FYP_Project.Helpers;

namespace FYP_Project.ViewModels
{
    public class TeamViewModel
    {
        public TeamViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableTeam = new Team();
                this.Teams = db.Query<Team>("Select * FROM Teams ORDER BY TeamID DESC").ToList();
            }
        }

        public List<Team> Teams { get; set; }

        public Team EditableTeam { get; set; }
    }
}
