using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPILearning.ModelValidationExample.Models
{
    public class User
    {
        [Required]
        [StringLength(50,ErrorMessage ="{0} alanı en fazla {1} karakter olabilir",MinimumLength =3)]
        public string firstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir", MinimumLength = 3)]
        public string  lastName{ get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="{0} alanının formatı uygun değil")]
        public string email { get; set; }

        [Required]
        [StringLength(20,ErrorMessage ="{0} alanı en fazla {1} karakter olabilir",MinimumLength =6)]
        [RegularExpression(@"/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&.])[A-Za-z\d$@$!%*?&.]{6, 20}/",
            ErrorMessage ="En az 1 büyük,1 küçük,1 özel karakter ve 6 ile 20 arasında bir karakter giriniz")]
        public string password { get; set; }

        [Required]
        [Compare("password",ErrorMessage ="Şifre bilgilerinizi kontrol ediniz")]
        public string repassword { get; set; }

        [Required]
        [Range(18,100,ErrorMessage ="Yaş aralığı 18 ile 100 arasındadır")]
        public int birthYear { get; set; }

        [CreditCard]
        public string creditCard { get; set; }

        [Url] 
        public string personelUrl { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }



    }
}