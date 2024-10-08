﻿using NetArchTest.Rules;
using Xunit.Abstractions;

namespace Solar.Heliac.Architecture.UnitTests.Common;

public static class TestResultExtensions
{
    public static void DumpFailingTypes(this TestResult result, ITestOutputHelper outputHelper)
    {
        if (result.IsSuccessful)
            return;

        outputHelper.WriteLine("Failing Types:");

        foreach (var type in result.FailingTypes)
            outputHelper.WriteLine(type.FullName);
    }
}