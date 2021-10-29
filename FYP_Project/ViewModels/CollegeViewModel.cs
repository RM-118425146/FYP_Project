using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using FYP_Project.Models;
using Dapper;
using FYP_Project.Helpers;

namespace FYP_Project.ViewModels
{
    public class CollegeViewModel
    {
        public CollegeViewModel()
        {
            using(var db = DbHelper.GetConnection())
            {
                this.EditableCollege = new College();
                this.Colleges = db.Query<College>("Select * FROM Colleges ORDER BY CollegeID DESC").ToList();
            }
        }

        public List<College> Colleges { get; set; }

        public College EditableCollege { get; set; }
    }
}
