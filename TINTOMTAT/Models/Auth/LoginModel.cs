using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.Auth
{
    public class LoginModel
    {
        [DisplayName("Tài khoản")]
        public string TaiKhoan { get; set; }

        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }
    }
}