using Microsoft.EntityFrameworkCore;
using RPGSheet2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPGSheet2.Models
{
    #region Games
    public class SearchGame
    {
        public int GameID { get; set; }
        public string SearchID { get; set; }
        public string GameName { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string Description { get; set; }
        
        //public string SheetID { get; set; }
        public string SheetName { get; set; }
        public string SheetOwner { get; set; }

        public IEnumerable<string> CurrentUsers { get; set; }

        //Done like this because it needs to do the .Include 's
        public static async Task<SearchGame> Generate(ApplicationDbContext _context, Game game)
        {
            return await Generate(_context, game.ID);
        }
        public static async Task<SearchGame> Generate(ApplicationDbContext _context, int GameID)
        {
            return await _gen(_context.Games.Include(g => g.Accesses).Include(g => g.gameSheet).ThenInclude(g => g.originalSheet).FirstOrDefault(g => g.ID == GameID));
        }
        public static async Task<SearchGame> Generate(ApplicationDbContext _context, string GameID)
        {
            return await _gen(_context.Games.Include(g => g.Accesses).Include(g => g.gameSheet).ThenInclude(g => g.originalSheet).FirstOrDefault(g => g.SearchID == GameID));
        }
        public static IEnumerable<SearchGame> GenerateMany(ApplicationDbContext _context, params int[] GameIDs)
        {
            List<Game> games = (_context.Games.Include(g => g.Accesses).Include(g => g.gameSheet).ThenInclude(g => g.originalSheet)).Where(g => GameIDs.Contains(g.ID)).ToList();
            List<SearchGame> results = new List<SearchGame>();
            List<Task> tasks = new List<Task>();
            foreach(Game game in games)
            {
                tasks.Add(_gen(game,results));
            }
            Task.WaitAll(tasks.ToArray());
            return results;
        }
        public static IEnumerable<SearchGame> GenerateMany(ApplicationDbContext _context, params Game[] games)
        {
            List<SearchGame> results = new List<SearchGame>();
            List<Task> tasks = new List<Task>();
            foreach (Game game in games)
            {
                tasks.Add(_gen(game, results));
            }
            Task.WaitAll(tasks.ToArray());
            return results;
        }
        private static async Task<SearchGame> _gen(Game game, List<SearchGame> output=null)
        {
            if (game == null) return null;
            SearchGame ret = new SearchGame();
            

            ret.GameID = game.ID;
            ret.SearchID = game.SearchID;
            ret.GameName = game.DisplayName;
            ret.OwnerID = game.OwnerID;
            ret.OwnerName = (await Extensions.GetUserNameAsync(game.OwnerID));
            ret.Description = game.Description;

            if (game.HasSheet())
            {
                ret.SheetName = game.gameSheet.DisplayName;
                ret.SheetOwner = await Extensions.GetUserNameAsync(game.gameSheet.originalSheet.OwnerID);
            }
            else
            {
                ret.SheetName = "NO SHEET";
                ret.SheetOwner = "NO SHEET";
            }

            Task<IEnumerable<string>> nameTask = game.GetUserNamesForGame();
            nameTask.Wait();
            ret.CurrentUsers = nameTask.Result;

            if (output != null)
            {
                output.Add(ret);
            }
            return ret;
        }
        public string CollapseName(string suffix="")
        {
            return GameID + suffix;
        }
    }

    public class CreateGame
    {
        [Required]
        [Display(Name = "Game Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Password { get; set; }

        public Game ToGame(string UserID)
        {
            Game ret = new Game();
            ret.DisplayName = this.Name;
            ret.Description = this.Description;
            ret.Password = this.Password;
            ret.OwnerID = UserID;
            return ret;
        }
    }

    public class EditGame
    {
        public int GameID { get; set; }

        public string SearchID { get; set; }

        [Required]
        [Display(Name = "Game Name")]
        public string DisplayName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<string> CurrentUsers { get; set; }

        public Game ToGame(ApplicationDbContext _context)
        {
            Game ret = _context.Games.Find(this.GameID);
            ret.DisplayName = this.DisplayName;
            ret.Description = this.Description;
            ret.Password = this.Password;
            return ret;
        }

        public static async Task<EditGame> GenerateAsync(ApplicationDbContext _context, Game game)
        {
            EditGame ret = new EditGame();

            ret.GameID = game.ID;
            ret.SearchID = game.SearchID;
            ret.DisplayName = game.DisplayName;
            ret.Description = game.Description;

            /*
            if (game.HasSheet())
            {
                ret.SheetName = game.gameSheet.originalSheet.SheetName;
                ret.SheetOwner = Extensions.GetUser(game.gameSheet.originalSheet.OwnerID).UserName;
            }
            else
            {
                ret.SheetName = "NO SHEET";
                ret.SheetOwner = "NO SHEET";
            }*/

            ret.CurrentUsers = await game.GetUserNamesForGame();

            return ret;
        }
    }

    public class DeleteGame
    {
        public int GameID { get; set; }
        public string SearchID { get; set; }
        public string GameName { get; set; }

        public static DeleteGame Generate(ApplicationDbContext _context, Game game)
        {
            DeleteGame ret = new DeleteGame();

            ret.GameID = game.ID;
            ret.SearchID = game.SearchID;
            ret.GameName = game.DisplayName;

            return ret;
        }
    }

    public class JoinGame
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public string OwnerName { get; set; }

        public string UserID { get; set; }
        
        public string password { get; set; }

        public static JoinGame FromSearchGame(SearchGame sgame, string UserID)
        {
            if (sgame == null) return null;

            JoinGame jgame = new JoinGame();

            jgame.GameID = sgame.GameID;
            jgame.GameName = sgame.GameName;
            jgame.OwnerName = sgame.OwnerName;
            jgame.UserID = UserID;

            return jgame;
        }
    }
    #endregion
}
