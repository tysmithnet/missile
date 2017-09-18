﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.Client;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems.WindowItems;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class Integration_Tests
    {
        [Fact]
        public void Input_Should_Have_Focus_On_Start()
        {   
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            var applicationPath = Path.Combine(dirPath, "Missile.Client.exe");

            using (var application = Application.Launch(applicationPath))
            {
                var windows = application.GetWindows();
                var window = windows.First(x => x.Id == "Main");
                var textBox = window.Items.First(x => x.Id == "Input");
                textBox.IsFocussed.Should().BeTrue("the input text box should start with focus");
            }                                                            
        }
    }
}
