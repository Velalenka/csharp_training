﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredntials()
        {
            app.Auth.Logout();

            AccountData account =  new AccountData("admin", "secret");
            app.Auth.Login(account);
                Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidCredntials()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "123456");
            app.Auth.Login(account);
                Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
