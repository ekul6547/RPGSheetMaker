﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPGSheet2.Models
{
    public class TutorialSection
    {
        [Key]
        public int ID { get; set; }

        public string Header { get; set; }

        public string HTMLContent { get; set; }

        public virtual TutorialPage Page { get; set; }
    }
    public class AddTutorialSection : TutorialSection
    {
        public int PageID { get; set; }

        public AddTutorialSection()
        {

        }
        public AddTutorialSection(TutorialSection section)
        {
            this.ID = section.ID;
            this.Header = section.Header;
            this.HTMLContent = section.HTMLContent;
            this.Page = section.Page;
            if(Page != null)
            {
                PageID = Page.ID;
            }
        }
    }

    public class TutorialPage
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }
        public string Category { get; set; }

        public virtual ICollection<TutorialSection> Sections { get; set; }
    }
}
