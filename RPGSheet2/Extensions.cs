using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using RPGSheet2.Data;
using RPGSheet2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RPGSheet2
{

    public static class Extensions
    {
        public static ApplicationDbContext _context { get; set; }

        public static string GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return "";
            return claim.Value;
        }
        public static bool IsLoggedIn(this IPrincipal principal)
        {
            string UserID = principal.GetUserId();
            return !String.IsNullOrEmpty(UserID);
        }
        public static IdentityUser GetUser(string UserID)
        {
            return _context.Users.Find(UserID);
        }
        public static string GetUserName(string UserID)
        {
            return _context.Users.Find(UserID).UserName;
        }

        public static IEnumerable<IdentityUser> GetUsersForGame(this Game game)
        {
            foreach(GameAccess acc in game.Accesses)
            {
                yield return GetUser(acc.UserID);
            }
        }
        public static IEnumerable<string> GetUserIDsForGame(this Game game)
        {
            foreach (GameAccess acc in game.Accesses)
            {
                yield return acc.UserID;
            }
        }
        public static async Task<IEnumerable<string>> GetUserNamesForGame(this Game game)
        {
            if (game.Accesses.Count == 0) return new string[0];
            string[] userIDS = game.Accesses.Select((a) => a.UserID).ToArray();
            if (userIDS != null && userIDS.Length > 0){
                string[] names = _context.Users.Where((u) => userIDS.Contains(u.Id)).Select((u) => u.UserName).ToArray();
                if(names != null && names.Length > 0)
                {
                    return names;
                }
            }
            return new string[0];
        }
        public static int UserCount(this Game game)
        {
            return game.Accesses.Count;
        }


        public static IEnumerable<GameAccess> GetGameAccessForUser(this ApplicationDbContext _context, string UserID)
        {
            return _context.GameAccesses.Where(a => a.UserID == UserID).Include(a => a.game).ThenInclude(g => g.Accesses).ToList();
        }

        public static IEnumerable<Game> GetGamesForUser(this ApplicationDbContext _context, string UserID)
        {
            foreach(GameAccess acc in _context.GetGameAccessForUser(UserID))
            {
                yield return acc.game;
            }
        }
        public static async Task<IEnumerable<Game>> GetGamesForUserAsync(this ApplicationDbContext _context, string UserID)
        {
            return await Task.Run(() => _context.GetGamesForUser(UserID));
        }

        public static async Task<List<SearchGame>> GetSearchGamesForUserAsync(this ApplicationDbContext _context, string UserID)
        {
            return await Task.Run(() => _context.GetSearchGamesForUser(UserID));
        }
        public static List<SearchGame> GetSearchGamesForUser(this ApplicationDbContext _context, string UserID)
        {
            IEnumerable<GameAccess> accesses = _context.GetGameAccessForUser(UserID);


            return (SearchGame.GenerateMany(_context,accesses.Select(g => g.game).ToArray())).ToList();
        }

        
        public static byte GameScore(Game game, string query)
        {
            if (game.SearchID.Contains(query)) return 0;
            if (game.DisplayName.Contains(query)) return 1;
            if (game.Description.Contains(query)) return 2;
            if (game.gameSheet.DisplayName.Contains(query)) return 3;
            if (GetUserName(game.OwnerID).Contains(query)) return 4;
            return 255;
        }

        public static async Task<List<int>> SearchGames(this ApplicationDbContext _context, string searchQuery="",byte searchLimit=20,int pageNumber=0)
        {
            IQueryable<int> query;
            var IQuery = _context.Games.Include((g) => g.Characters).Include((g) => g.gameSheet);
            var oQuery = IQuery.OrderBy((g) => GameScore(g,searchQuery));
            query = oQuery.Select(g => g.ID);
            query = query.Skip(pageNumber * searchLimit);
            query = query.Take(searchLimit);

            Debug.WriteLine(query.ToString());

            return query.ToList();
        }

        public static async Task GrantUserAccessToGame(this ApplicationDbContext _context, Game game, string UserID, bool AutoSave=false)
        {
            GameAccess newAccess = new GameAccess();

            newAccess.game = game;
            newAccess.UserID = UserID;
            //newAccess.UserName = GetUserName(UserID);
            //newAccess.EndTime = DateTime.UtcNow + new TimeSpan();

            _context.Add(newAccess);

            if (AutoSave)
            {
                await _context.SaveChangesAsync();
            }
        }




        public static IHtmlContent DisplayDescriptionFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DisplayMetadataFor(expression, (m) => m.Description) as HtmlString;
        }

        public static IHtmlContent DisplayMetadataFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Func<ModelMetadata, string> GetMetadata)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (GetMetadata == null) throw new ArgumentNullException("GetMetadata Func<ModelMetadata, string>");

            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
            if (modelExplorer == null) throw new InvalidOperationException($"Failed to get model explorer for {ExpressionHelper.GetExpressionText(expression)}");

            return new HtmlString(GetMetadata(modelExplorer.Metadata));
        }

        public static IEnumerable<T> CastTo<F, T>(this IEnumerable<F> array)
        {
            foreach (object o in array)
            {
                yield return (T)o;
            }
        }

        public static IEnumerable<F> ForEach<T, F>(this IEnumerable<T> list, Func<T, F> func)
        {
            foreach (T item in list)
            {
                yield return func(item);
            }
        }

        public static IEnumerable<V> GetValues<K, V>(this IEnumerable<KeyValuePair<K, V>> self)
        {
            foreach (var v in self)
            {
                yield return v.Value;
            }
        }
        public static IEnumerable<K> GetKeys<K, V>(this IEnumerable<KeyValuePair<K, V>> self)
        {
            foreach (var v in self)
            {
                yield return v.Key;
            }
        }

        public static class HashID
        {
            const int HashLength = 8;
            const string HashDict = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            public static string GenHash(int ID)
            {
                StringBuilder ret = new StringBuilder();
                Random rand = new Random(ID);
                for (int i = 0; i < HashLength; i++)
                {
                    ret.Append(HashDict[rand.Next(0,HashDict.Length-1)]);
                }
                return ret.ToString();
            }
        }


        public static class DropDownLists
        {
            public static ApplicationDbContext _context { get; set; }
            
            public static IEnumerable<SelectListItem> CreateDropDownList<T>(IEnumerable<T> list, Func<T, string> GetValue, Func<T, string> GetText, Func<T, SelectListGroup> GetGroup = null)
            {
                foreach (T item in list)
                {
                    var ret = new SelectListItem()
                    {
                        Value = GetValue(item),
                        Text = GetText(item)
                    };
                    if (GetGroup != null)
                    {
                        ret.Group = GetGroup(item);
                    }
                    yield return ret;
                }
            }

            public class GroupGenerator
            {
                List<SelectListGroup> groups;

                public GroupGenerator(params string[] startingGroups)
                {
                    groups = new List<SelectListGroup>();
                    foreach (string s in startingGroups)
                    {
                        NewGroup(s);
                    }
                }
                SelectListGroup StringToGroup(string name)
                {
                    return new SelectListGroup()
                    {
                        Name = name
                    };
                }
                SelectListGroup NewGroup(string name)
                {
                    SelectListGroup ret = StringToGroup(name);
                    groups.Add(ret);
                    return ret;
                }
                bool ContainsGroup(string name)
                {
                    return groups.Any((s) => s.Name.ToUpper() == name.ToUpper());
                }
                SelectListGroup GetFirst(string name)
                {
                    return groups.FirstOrDefault((s) => s.Name.ToUpper() == name.ToUpper());
                }
                public SelectListGroup GetGroup(string name)
                {
                    SelectListGroup ret = GetFirst(name);
                    if (ret == null)
                    {
                        return NewGroup(name);
                    }
                    else
                    {
                        return ret;
                    }
                }
            }
        }
    }
}
