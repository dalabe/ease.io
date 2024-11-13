using Ease.Data;
using Ease.Services;

namespace TestProjectEase
{
    public class EaseValidatorsTests
    {
        //[Fact]
        //public void ValidateGuidformat()
        //{
        //    //// Arrange   
        //    string id = "43B56A5297824C08AC2C04C7E1EAD36C";

        //    ///Act
        //    bool isValid = Utils.IsValidGuid(id);

        //    ///Assert
        //    Assert.True(isValid);
        //}

        //[Fact]
        //public void InValidateGuidformat()
        //{
        //    //// Arrange   
        //    string id = "1234567890123456789012";

        //    ///Act
        //    bool isValid = Utils.IsValidGuid(id);

        //    ///Assert
        //    Assert.False(isValid);
        //}

        [Theory]
        [InlineData("43B56A5297824C08AC2C04C7E1EAD36C", true)]
        [InlineData("1234567890123456789012", false)]
        public void ValidateGuidformat(string id, bool expected)
        {
            //// Arrange   
            ///Act
            bool isValid = Utils.IsValidGuid(id);

            ///Assert
            Assert.Equal(expected, isValid);
        }
    }
}