using RCBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBot.Helpers
{
    public class DbHelper
    {

        //private RCDbContext dbContext;

        public  MemberProfile search(ulong userId)
        {
            try {
                using (var dbContext = new RCDbContext())
                {

                    var member = dbContext.members.Where(x => x.UserId == userId).FirstOrDefault();
                    return member;
                }
            } catch(Exception ex) { Console.WriteLine(ex.ToString());
                return null;
            }
            



        }

        public  Task updateProfile(ulong userId, string ign, string link)
        {
            try
            {
                using (var dbContext = new RCDbContext())
                {


                    if (dbContext.members.Where(x => x.UserId == userId).Count() < 1)
                    {
                        var member = new MemberProfile()
                        {
                            inGameName = ign,
                            inviteID = link,
                            UserId = userId,
                            forTrade = "",
                            stock = ""
                        };
                        dbContext.members.Add(member);
                    }
                    else
                    {
                        var member = dbContext.members.Where(x => x.UserId == userId).FirstOrDefault();
                        member.inviteID = (link == "n/a") ? "" : link;
                        member.inGameName = (ign == "n/a") ? "" : ign;
                        dbContext.members.Update(member);
                    }
                    dbContext.SaveChangesAsync();
                }

                return Task.CompletedTask;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Task<bool> reset(ulong userId)
        {
            try
            {
                using (var dbContext = new RCDbContext())
                {

                    if (dbContext.members.Where(x => x.UserId == userId).Count() < 1)
                    {
                        return Task.FromResult(false);
                    }
                    else
                    {
                        var member = dbContext.members.Where(x => x.UserId == userId).FirstOrDefault();
                        member.inviteID = "";
                        member.inGameName = "";
                        dbContext.members.Update(member);
                    }
                    dbContext.SaveChangesAsync();
                }
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Task.FromResult(false);
            }
        }

        public Task addToList( ulong userId, string type, string ingredients = "")
        {
            try
            {
                using (var dbContext = new RCDbContext())
                {

                    if (dbContext.members.Where(x => x.UserId == userId).Count() < 1)
                    {
                        var member = new MemberProfile()
                        {
                            inGameName = "",
                            inviteID = "",
                            UserId = userId,
                            forTrade = (type == "n") ? ingredients : "",
                            stock = (type == "h") ? ingredients : "",
                        };
                        dbContext.members.Add(member);
                    }
                    else
                    {
                        var member = dbContext.members.Where(x => x.UserId == userId).FirstOrDefault();
                        if (type == "n")
                        {
                            member.forTrade = member.forTrade + " " + ingredients;
                        }
                        else if (type == "h")
                        {
                            member.stock = member.stock + " " + ingredients;
                        }


                        dbContext.members.Update(member);
                    }
                    dbContext.SaveChangesAsync();
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Task updateIngredient(ulong userId, string ingredients,string type)
        {
            try
            {
                using (var dbContext = new RCDbContext())
                {
                    if (dbContext.members.Where(x => x.UserId == userId).Count() < 1)
                    {
                        return Task.FromResult(false);
                    }
                    else
                    {
                        var member = dbContext.members.Where(x => x.UserId == userId).FirstOrDefault();
                        if (type == "h")
                        {
                            member.stock = member.stock.ToString().Replace(ingredients + " ", "");
                        }
                        else if (type == "n")
                        {
                            member.forTrade = member.forTrade.ToString().Replace(ingredients + " ", "");
                        }
                        //Console.WriteLine(member.n.ToString());
                        dbContext.members.Update(member);
                    }
                    dbContext.SaveChangesAsync();
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Task.FromResult(false); ;
            }
        }

        public Task resetList(ulong userId)
        {
            try
            {
                using (var dbContext = new RCDbContext())
                {
                    if (dbContext.members.Where(x => x.UserId == userId).Count() < 1)
                    {
                        return Task.FromResult(false);
                    }
                    else
                    {
                        var member = dbContext.members.Where(x => x.UserId == userId).FirstOrDefault();
                        member.stock = "";
                        member.forTrade = "";
                        dbContext.members.Update(member);
                    }
                    dbContext.SaveChangesAsync();
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Task.FromResult(false); ;
            }
        }
    }
}
