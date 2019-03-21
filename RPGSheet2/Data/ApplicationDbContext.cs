using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RPGSheet2;
using RPGSheet2.Models;

namespace RPGSheet2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameAccess> GameAccesses { get; set; }
        public DbSet<GameMessage> GameMessages { get; set; }

        public DbSet<Character> Characters { get; set; }
        public DbSet<UnhideChar> UnHideFor { get; set; }
        public DbSet<CharacterValue> CharacterValues { get; set; }

        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SheetField> SheetFields { get; set; }

        public DbSet<GameSheet> GameSheet { get; set; }
        public DbSet<GameSheetField> GameSheetFields { get; set; }
        public DbSet<DropdownValue> DropdownValues { get; set; }

        public DbSet<TutorialPage> TutorialPages { get; set; }
        public DbSet<TutorialSection> TutorialSections { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Extensions._context = this;
            Extensions.DropDownLists._context = this;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GameMessage>()
                .Property((S) => S.SentTime).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
