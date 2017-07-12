using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyUser.Model.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(20)]
        public string Name { set; get; }

        [StringLength(50)]
        public string Email { set; get; }

        [StringLength(32)]
        public string UserName { get; set; }

        [StringLength(32)]
        public string Password { set; get; }

        public int Amount { set; get; }

        public DateTime? CreatedDate { set; get; }

        [StringLength(50)]
        public string Phone { set; get; }

        public bool? Status { get; set; }

        public virtual IEnumerable<TradeInfomation> TradeInfomations { set; get; }

    }
}
