//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Quiz_App.Models
{
    using System;
    using System.Collections.Generic;
    //using System.ComponentModel.DataAnnotations;

    public partial class Tbl_Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Student()
        {
            this.Tbl_SetExam = new HashSet<Tbl_SetExam>();
        }

        public int S_ID { get; set; }


        //[Required(ErrorMessage = "Field caant be empty")]
        public string S_UserName { get; set; }



      //  [Required(ErrorMessage = "Field caant be empty")]
        public string S_Password { get; set; }



        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Name should only contain alphabets")]
        //[Required(ErrorMessage = "Field caant be empty")]
        public string S_FirstName { get; set; }


        //[Required(ErrorMessage = "Field caant be empty")]
        public string S_LastName { get; set; }


       // [Required(ErrorMessage = "Field caant be empty")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string S_EmailID { get; set; }


        //[MinLength(10)]
        //[MaxLength(10)]
       // [Required(ErrorMessage = "Field caant be empty")]
        public Nullable<int> S_ContactNo { get; set; }
        public Nullable<System.DateTime> S_Birthdate { get; set; }


      //  [Range(5, 100, ErrorMessage = "Age should be between 5-100")]
        public Nullable<int> S_Age { get; set; }

        public string S_img { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_SetExam> Tbl_SetExam { get; set; }
    }

}
