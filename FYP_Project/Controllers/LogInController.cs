using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using FYP_Project.ViewModels;
using FYP_Project.Helpers;
using FYP_Project.Models;
/* Code to hash password found here: https://jasonwatmore.com/post/2020/07/16/aspnet-core-3-hash-and-verify-passwords-with-bcrypt?fbclid=IwAR25zwXUqLc-P2SSsXzHgI3nOv86eoCQHLlRV0SCgNYvqvU6XNilhhhsM4Q  */
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;

namespace FYP_Project.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        public IActionResult CreateUpdate(PlayerViewModel viewModel)
        {
            using (var db = DbHelper.GetConnection())
            {
                if (viewModel.EditablePlayer.PlayerID == null)
                {
                    viewModel.EditablePlayer.PlayerID = viewModel.Players.Count;
                    viewModel.EditablePlayer.password = BC.HashPassword(viewModel.EditablePlayer.password);
                    db.Insert<Player>(viewModel.EditablePlayer);
                    HttpContext.Session.SetString("emailAddress", viewModel.EditablePlayer.emailAddress);
                }
                else
                {
                    Player dbItem = db.Get<Player>(viewModel.EditablePlayer.PlayerID);
                    TryUpdateModelAsync<Player>(dbItem, "EditablePlayer");
                    db.Update<Player>(dbItem);
                }
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult LogIn(PlayerViewModel viewModel)
        {
            foreach(var player in viewModel.Players)
            {
                if(player.emailAddress == viewModel.EditablePlayer.emailAddress)
                {
                    if (BC.Verify(viewModel.EditablePlayer.password, player.password) == true)
                    {
                        HttpContext.Session.SetString("emailAddress", viewModel.EditablePlayer.emailAddress);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
            }
            return View("Index");
        }
    }
}
