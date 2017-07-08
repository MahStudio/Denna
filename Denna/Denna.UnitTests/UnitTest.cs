using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreDenna;
using CoreDenna.Types;
using CoreDenna.Models;
using System.Diagnostics;

namespace Denna.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MakeDatabase()
        {
            var a = DBAccess.CreateDB();
            
        }
        [TestMethod]
        public void AddUser()
        {
          UserModel.AddUser(new User()
            {
                FirstName = "Mohsen",
                LastName = "Seifi",
                Email = "Mohsens22@outlook.com",
                Password = "ljnlskfdnv"
            });
            if (DBAccess.QueryMaker("Type", "User")[0].ToDictionary() == null)
                throw new Exception();

           

        }
        
        [TestMethod]
        public void AddDocument()
        {
            var dic = new System.Collections.Generic.Dictionary<string, object>();
            dic.Add("Test", "Something");
            dic.Add("Type", "UnitTest");
            DBAccess.CreateDoc(dic);

            //Debug.WriteLine(_doc.Id + "-" + _doc.ToDictionary()["ID"])
        }
        [TestMethod]
        public void CheckIDSenerio()
        {
          var list =  DBAccess.QueryMaker("Type", "UnitTest");
            if (list[0]["ID"].Value.ToString() != list[0].Id)
                throw new Exception();
           
        }
    }
}
