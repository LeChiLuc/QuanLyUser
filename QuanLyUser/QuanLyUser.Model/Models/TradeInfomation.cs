using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.Model.Models
{
    [Table("TradeInfomations")]
    public class TradeInfomation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int Amount { set; get; }     

        public int UserID { set; get; }

        public DateTime? CreatedDate { set; get; }

        [ForeignKey("UserID")]
        public virtual User User { set; get; }
    }
}
