using MS.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CommonTests
{
    public class EnumExtensionTest
    {

        [Fact]
        [Trait("GetEnum","itemName")]
        private void GetEnum_EnumName_ResultCorrespondEnum() {

            // AAA
            //Arrange
            StatusCode statusCode = StatusCode.Deleted;
            //Act
            string actual = statusCode.ToString();
            //Assert
            Assert.Equal(statusCode, actual.GetEnum<StatusCode>());
        }
    }
}
