using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Data.Entites
{
    [Table("User")]
    public class User
    {
        public long Id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
    }
}