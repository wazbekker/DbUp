using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUp.Oracle;
using Shouldly;
using Xunit;

namespace DbUp.Tests.Support.Oracle
{
    public class OracleConnectionManagerTests
    {
        [Fact]
        public void CanParseSingleLineScript()
        {
            const string singleCommand = "create table FOO (myid INT NOT NULL);";

            var connectionManager = new OracleConnectionManager("connectionstring");
            var result = connectionManager.SplitScriptIntoCommands(singleCommand);

            result.Count().ShouldBe(1);
        }

        [Fact]
        public void CanParseMultilineScript()
        {
            var multiCommand = "create table FOO (myid INT NOT NULL);/";
            multiCommand += Environment.NewLine;
            multiCommand += "create table BAR (myid INT NOT NULL);/";
            multiCommand += Environment.NewLine;
            multiCommand += "create table TEST (myid INT NOT NULL);";
            multiCommand += Environment.NewLine;
            multiCommand += "/";

            var connectionManager = new OracleConnectionManager("connectionstring");
            var result = connectionManager.SplitScriptIntoCommands(multiCommand);

            result.Count().ShouldBe(3);
        }

        //[Fact]
        //public void CanParseMultilinePackageScriptWithDivideByOperator()
        //{
        //    var multiCommand = "create or replace package some_package is";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "procedure divide_two_numbers(p_first_number  in number, p_second_number in number,  p_result out number);";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "end some_package;";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "/";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "create or replace package body some_package is";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "procedure divide_two_numbers(p_first_number  in number, p_second_number in number,  p_result out number) is";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "begin";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "p_result := p_first_number / p_second_number;";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "end;";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "end some_package;";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "/";

        //    var connectionManager = new OracleConnectionManager("connectionstring");
        //    var result = connectionManager.SplitScriptIntoCommands(multiCommand);

        //    result.Count().ShouldBe(2);
        //}

        [Fact]
        public void CanParseMultilineProcedureScriptWithDivideByOperator()
        {
            var multiCommand = "create or replace procedure divide_two_numbers(p_first_number  in number, ";
            multiCommand += Environment.NewLine;
            multiCommand += "p_second_number in number, ";
            multiCommand += Environment.NewLine;
            multiCommand += "p_result out number) is ";
            multiCommand += Environment.NewLine;
            multiCommand += "begin";
            multiCommand += Environment.NewLine;
            multiCommand += "p_result := p_first_number / p_second_number;";
            multiCommand += Environment.NewLine;
            multiCommand += "end;";
            multiCommand += Environment.NewLine;
            multiCommand += "/";

            var connectionManager = new OracleConnectionManager("connectionstring");
            var result = connectionManager.SplitScriptIntoCommands(multiCommand);

            result.Count().ShouldBe(1);
        }

        //[Fact]
        //public void CanParseMultilineProcedureScriptWithDivideByOperatorOnSeparateLine()
        //{
        //    var multiCommand = "create or replace procedure divide_two_numbers(p_first_number  in number, ";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "p_second_number in number, ";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "p_result out number) is ";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "begin";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "p_result := p_first_number";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "/";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "p_second_number;";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "end;";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "/";

        //    var connectionManager = new OracleConnectionManager("connectionstring");
        //    var result = connectionManager.SplitScriptIntoCommands(multiCommand);

        //    result.Count().ShouldBe(1);
        //}

        //[Fact]
        //public void CanParseMultilineComments()
        //{
        //    var multiCommand = "/*";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "some multiline comment text";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "some more multiline comment text";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "*/";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "create table FOO (myid INT NOT NULL)";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "/";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "/* even more multiline comment text";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "and some more text";
        //    multiCommand += Environment.NewLine;
        //    multiCommand += "and even more text */";

        //    var connectionManager = new OracleConnectionManager("connectionstring");
        //    var result = connectionManager.SplitScriptIntoCommands(multiCommand);

        //    result.Count().ShouldBe(1);
        //}
    }
}
