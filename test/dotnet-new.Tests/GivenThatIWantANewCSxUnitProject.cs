// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.DotNet.Tools.Test.Utilities;
using Xunit;
using FluentAssertions;

namespace Microsoft.DotNet.Tests
{
    [Collection("'dotnet test' collection")]
    public class GivenThatIWantANewCSxUnitProject : TestBase
    {
        
        [Fact]
        public void When_xUnit_project_created_Then_project_restores()
        {
            var rootPath = Temp.CreateDirectory().Path;

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("new --type xunittest")
                .Should()
                .Pass();
            
            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("restore")
                .Should().Pass();
            
        }

        [Fact]
        public void When_dotnet_test_is_invoked_Then_tests_run_without_errors()
        {
            var rootPath = Temp.CreateDirectory().Path;

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("new --type xunittest");

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("restore");

            var buildResult = new TestCommand("dotnet")
                .WithWorkingDirectory(rootPath)
                .ExecuteWithCapturedOutput("test")
                .Should()
                .Pass()
                .And
                .NotHaveStdErr();
        }
    }
}
