using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using FYP_Project.Models;
using Dapper;
using FYP_Project.Helpers;

namespace FYP_Project.ViewModels
{
    public class PlayerViewModel
    {
        public PlayerViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditablePlayer = new Player();
                this.Players = db.Query<Player>("Select * FROM Players ORDER BY PlayerID DESC").ToList();
            }
        }

        public List<Player> Players { get; set; }

        public Player EditablePlayer { get; set; }
    }
}
