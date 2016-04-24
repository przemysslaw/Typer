using System;
using NUnit.Framework;

namespace SDK.Test
{
    [TestFixture]
    [Category("UnitTests")]
    public class RequireTests
    {
        [TestFixture]
        [Category("UnitTests")]
        public class NotNull
        {
            [Test]
            public void When_Passed_Parameter_Is_Null_Exception_Should_Be_Thrown()
            {
                //Act
                Assert.Throws<ArgumentNullException>(() => Require.NotNull(null, String.Empty));
            }

            [Test]
            public void When_Passed_Parameter_Is_Not_Null_No_Exception_Should_Be_Thrown()
            {
                //Act
                Require.NotNull(new Object(), String.Empty);
            }

            [Test]
            public void When_Passed_Parameter_Is_Null_Exception_Should_Be_Thrown_With_Passed_Message()
            {
                //Arrange
                var parameterName = "custom param name";

                //Act
                try
                {
                    Require.NotNull(null, parameterName);
                }
                catch (ArgumentNullException e)
                {
                    Assert.IsTrue(e.Message.Contains(parameterName));
                }
            }
        }

        [TestFixture]
        [Category("UnitTests")]
        public class NotNullNotEmpty
        {
            [Test]
            public void When_Passed_Parameter_Is_Null_Exception_Should_Be_Thrown()
            {
                //Act
                Assert.Throws<ArgumentNullException>(() => Require.NotNullNotEmpty(null, String.Empty));
            }

            [Test]
            public void When_Passed_Parameter_Is_Empty_Exception_Should_Be_Thrown()
            {
                //Act
                Assert.Throws<ArgumentNullException>(() => Require.NotNullNotEmpty(String.Empty, String.Empty));
            }

            [Test]
            public void When_Passed_Parameter_Is_Not_Null_And_Not_Empty_Exception_Should_Not_Be_Thrown()
            {
                //Arrange
                var parameter = "not null";

                //Act
                Require.NotNullNotEmpty(parameter, String.Empty);
            }

            [Test]
            public void When_Passed_Parameter_Is_Null_Exception_Should_Be_Thrown_Containing_Parameter_Name_In_Message()
            {
                //Arrange
                var parameterName = "parameter";

                //Act
                try
                {
                    Require.NotNullNotEmpty(null, parameterName);
                }
                catch (ArgumentNullException e)
                {
                    Assert.IsTrue(e.Message.Contains(parameterName));
                }
            }

            [Test]
            public void When_Passed_Parameter_Is_Empty_Exception_Should_Be_Thrown_Containing_Parameter_Name_In_Message()
            {
                //Arrange
                var parameterName = "parameter";

                //Act
                try
                {
                    Require.NotNullNotEmpty(String.Empty, parameterName);
                }
                catch (ArgumentNullException e)
                {
                    Assert.IsTrue(e.Message.Contains(parameterName));
                }
            }
        }

        [TestFixture]
        [Category("UnitTests")]
        public class GreaterEqualDateTime
        {
            [Test]
            public void When_right_operand_is_greater_then_left_operand_Then_no_exception_is_thrown()
            {
                var leftOperand = new DateTime(2015, 07, 19, 12, 00, 01);
                var rightOperand = new DateTime(2015, 07, 19, 12, 00, 00);

                Assert.DoesNotThrow(() => Require.GreaterEqual<ArgumentException>(leftOperand, rightOperand));
            }

            [Test]
            public void When_right_operand_is_equal_to_the_left_operand_Then_no_exception_is_thrown()
            {
                var leftOperand = new DateTime(2015, 07, 19, 12, 00, 00);
                var rightOperand = new DateTime(2015, 07, 19, 12, 00, 00);

                Assert.DoesNotThrow(() => Require.GreaterEqual<ArgumentException>(leftOperand, rightOperand));
            }

            [Test]
            public void When_left_operand_is_not_greater_equal_to_the_right_operand_Then_an_exception_is_thrown()
            {
                var leftOperand = new DateTime(2015, 07, 19, 12, 00, 00);
                var rightOperand = new DateTime(2015, 07, 19, 12, 00, 01);

                Assert.Throws<ArgumentException>(() => Require.GreaterEqual<ArgumentException>(leftOperand, rightOperand));
            }
        }

        [TestFixture]
        [Category("UnitTests")]
        public class GreaterEqualInt
        {
            [Test]
            public void When_right_operand_is_smaller_then_left_operand_Then_no_exception_is_thrown()
            {
                var leftOperand = 1;
                var rightOperand = 0;

                Assert.DoesNotThrow(() => Require.GreaterEqual<ArgumentException>(leftOperand, rightOperand));
            }

            [Test]
            public void When_right_operand_is_equal_to_the_left_operand_Then_no_exception_is_thrown()
            {
                var leftOperand = 1;
                var rightOperand = 1;

                Assert.DoesNotThrow(() => Require.GreaterEqual<ArgumentException>(leftOperand, rightOperand));
            }

            [Test]
            public void When_left_operand_is_smaller_then_the_right_operand_Then_an_exception_is_thrown()
            {
                var leftOperand = 0;
                var rightOperand = 1;

                Assert.Throws<ArgumentException>(() => Require.GreaterEqual<ArgumentException>(leftOperand, rightOperand));
            }
        }

        [TestFixture]
        [Category("UnitTests")]
        public class Greater
        {
            [Test]
            public void When_right_operand_is_greater_then_left_operand_Then_no_exception_is_thrown()
            {
                var leftOperand = new DateTime(2015, 07, 19, 12, 00, 01);
                var rightOperand = new DateTime(2015, 07, 19, 12, 00, 00);

                Assert.DoesNotThrow(() => Require.Greater<ArgumentException>(leftOperand, rightOperand));
            }

            [Test]
            public void When_left_operand_is_not_greater_than_the_right_operand_Then_an_exception_is_thrown()
            {
                var leftOperand = new DateTime(2015, 07, 19, 12, 00, 00);
                var rightOperand = new DateTime(2015, 07, 19, 12, 00, 01);

                Assert.Throws<ArgumentException>(() => Require.Greater<ArgumentException>(leftOperand, rightOperand));
            }
        }
    }
}
