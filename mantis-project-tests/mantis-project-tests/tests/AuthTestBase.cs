﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_project_tests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        public void SetUpLogin()
        {
            app.Auth.Login(new AccountData("administrator", "root"));
        }
    }
}
