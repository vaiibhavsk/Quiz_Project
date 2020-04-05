using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Online_Quiz_App.Controllers;
using System.Web.Mvc;
using Online_Quiz_App.Models;
//using System.Web.UI.WebControls;

namespace Online_Quiz_App.Tests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void Tbl_Admin_CompareTwoAsserts_AreEqual()
        {
            var actual = new Tbl_Admin{ Ad_ID =1, Ad_Name ="Admin"};
            Assert.AreEqual(1, actual.Ad_ID);
            Assert.AreEqual("Admin", actual.Ad_Name);
        }

        [TestMethod]
        public void Tbl_Student_CompareTwoAsserts_AreEqual()
        {
            var actual = new Tbl_Student { S_ID = 1, S_UserName = "abc", S_Password = "xyz", S_FirstName = "abc", S_LastName = "lmn", S_EmailID = "abc@gmail.com", S_Age = 22 };
            Assert.AreEqual(1, actual.S_ID);
            Assert.AreEqual("abc", actual.S_UserName);
            Assert.AreEqual("xyz", actual.S_Password);
            Assert.AreEqual("abc", actual.S_FirstName);
            Assert.AreEqual("lmn", actual.S_LastName);
            Assert.AreEqual("abc@gmail.com", actual.S_EmailID);
            Assert.AreEqual(22, actual.S_Age);

        }

        [TestMethod]
        public void Tbl_Category_CompareTwoAsserts_AreEqual()
        {

            var actual = new Tbl_Category { Cat_ID = 1, Cat_Name = "C#" };
            Assert.AreEqual(1, actual.Cat_ID);
            Assert.AreEqual("C#", actual.Cat_Name);
        }

        [TestMethod]
        public void Tbl_Questions_CompareTwoAsserts_AreEqual()
        {

            var actual = new Tbl_Questions { Q_ID = 1, Q_Text = "What is c#?",Op_A="OOP Language",Op_B="Pqr",Op_C="abc",Op_D="lmn",Correct_Op="A" };
            Assert.AreEqual(1, actual.Q_ID);
            Assert.AreEqual("What is c#?", actual.Q_Text);
            Assert.AreEqual("OOP Language", actual.Op_A);
            Assert.AreEqual("Pqr", actual.Op_B);
            Assert.AreEqual("abc", actual.Op_C);
            Assert.AreEqual("lmn", actual.Op_D);
            Assert.AreEqual("A", actual.Correct_Op);
        }

    }
}
