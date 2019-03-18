using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RPGSheet2.Data;
using static RPGSheet2.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGSheet2.Models
{
    #region GAMEDATA

    public class Game
    {
        [Key]
        public int ID { get; set; }
        
        public string SearchID { get; set; }

        [Required]
        public string OwnerID { get; set; }

        [Required]
        [Display(Name ="Game Name")]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        [Required]
        public string Password { get; set; }

        public void AssignID(bool redo=false)
        {
            if(this.ID > 0 && (redo || String.IsNullOrWhiteSpace(SearchID)))
            {
                this.SearchID = HashID.GenHash(this.ID);
            }
        }
        
        public virtual GameSheet gameSheet { get; set; }

        public bool HasSheet() { return this.gameSheet != null; }

        public virtual ICollection<GameAccess> Accesses { get; set; }

        public virtual ICollection<GameMessage> Messages { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        
    }

    public class GameAccess
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public virtual Game game { get; set; }

        [Required]
        public string UserID { get; set; }

        public DateTime EndTime { get; set; }
        
    }

    public class GameMessage
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public virtual Game game { get; set; }

        public string Message { get; set; }

        public string SenderID { get; set; }

        public DateTime SentTime { get; set; }
    }

    #endregion


    #region CHARACTERDATA

    public class Character
    {
        [Key]
        public int ID { get; set; }

        public virtual Game game { get; set; }

        [Required]
        public string OwnerID { get; set; }

        public string Name { get; set; }

        public bool IsNPC { get; set; }

        public bool IsHidden { get; set; }

        public virtual ICollection<UnhideChar> UnHideFor { get; set; }

        public virtual ICollection<CharacterValue> Values { get; set; }
    }

    public class UnhideChar
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public virtual Character character { get; set; }

        [Required]
        public string UserID { get; set; }
    }

    public class CharacterValue
    {
        [Key]
        public int ID { get; set; }

        public virtual GameSheetField gameSheetField { get; set; }

        public virtual Character character { get; set; }

        public string value_string { get; set; }
        public int value_int { get; set; }
        public float value_float { get; set; }
        public bool value_bool { get; set; }

        public int value_stat { get; set; }

        public byte value_dropdown { get; set; }
    }

    #endregion

    #region SHEETDATA

    public class Sheet
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string SheetName { get; set; }

        public string Description { get; set; }

        public string OwnerID { get; set; }

        public double Version { get; set; }

        public virtual ICollection<SheetField> Fields { get; set; }
    }

    public class SheetField
    {
        [Key]
        public int ID { get; set; }

        public virtual Sheet sheet { get; set; }

        public string DisplayName { get; set; }

        #region FIELDVALUES
        public string value_string { get; set; }
        public int value_int { get; set; }
        public float value_float { get; set; }
        public bool value_bool { get; set; }

        public int value_stat { get; set; }
        public int value_stat_midpoint { get; set; }
        public int value_stat_divisor { get; set; }

        public int calculate_stat()
        {
            return Convert.ToInt32(Math.Floor((this.value_stat - this.value_stat_midpoint) / Convert.ToDouble(this.value_stat_divisor)));
        }

        public bool IsDropdown { get; set; }
        #endregion

        public float xpos { get; set; }
        public float ypos { get; set; }
        public float height { get; set; }
        public float width { get; set; }

        public string hexColour { get; set; }
        
        public string ImagePath { get; set; }
    }

    //Duplicates so if original sheet changes / edited, the game isn't affected
    public class GameSheet
    {
        [Key]
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public virtual Sheet originalSheet { get; set; }

        public double Version { get; set; }

        public virtual ICollection<GameSheetField> Fields { get; set; }
    }

    public class GameSheetField
    {
        [Key]
        public int ID { get; set; }

        public virtual SheetField OriginalField { get; set; }

        public virtual GameSheet gameSheet { get; set; }

        public string DisplayName { get; set; }

        #region FIELDVALUES
        public string value_string { get; set; }
        public int value_int { get; set; }
        public float value_float { get; set; }
        public bool value_bool { get; set; }

        public int value_stat { get; set; }
        public int value_stat_midpoint { get; set; }
        public int value_stat_divisor { get; set; }

        public int calculate_stat()
        {
            return Convert.ToInt32(Math.Floor((this.value_stat - this.value_stat_midpoint) / Convert.ToDouble(this.value_stat_divisor)));
        }

        public bool IsDropdown { get; set; }
        public virtual ICollection<DropdownValue> DropdownValues { get; set; }
        #endregion

        public float xpos { get; set; }
        public float ypos { get; set; }
        public float height { get; set; }
        public float width { get; set; }

        public string hexColour { get; set; }

        public string ImagePath { get; set; }
    }

    public class DropdownValue
    {
        [Key]
        public int ID { get; set; }

        public virtual GameSheetField gameSheetField { get; set; }

        //Unique PER dropdown, not unique in db
        public byte Key { get; set; }

        public string Value { get; set; }
    }

    #endregion
}
