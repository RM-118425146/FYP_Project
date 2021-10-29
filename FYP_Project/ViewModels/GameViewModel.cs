using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using FYP_Project.Models;
using Dapper;
using FYP_Project.Helpers;

namespace FYP_Project.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableGame = new Game();
                this.Games = db.Query<Game>("Select * FROM Games ORDER BY GameID DESC").ToList();
            }
        }

        public List<Game> Games { get; set; }

        public Game EditableGame { get; set; }
    }
}
