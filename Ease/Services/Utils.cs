namespace Ease.Services
{
    public static class Utils
    {
        /// <summary>
        /// Validates if a string id is a GUIDs formatted as 32 hexadecimal characters, all uppercase 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if valid</returns>
        public static bool IsValidGuid(string id)
        {
            return Guid.TryParse(id, out Guid guid) && guid.ToString("N").ToUpper() == id;
        }
    }
}
