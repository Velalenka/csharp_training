﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void setUpConfig()
        {
            app.FtpHelper.BackupFile("config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                //app.FtpHelper.Upload("C:\\xampp\\htdocs\\mantisbt-2.24.0\\config\\config_inc.php", localFile);
                app.FtpHelper.Upload("config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser09",
                Password = "password",
                Email = "testuser09@localhost.localdomain"
            };

            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x=>x.Name == account.Name);
            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.FtpHelper.RestoreBackupFile("config/config_inc.php");
        }
    }
}
