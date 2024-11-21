namespace Domain.Utilities
{
    public static class Validation
    {
        public static bool ValidateSearchField(string value, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrEmpty(value))
            {
                error = "Field is Empty!";
                return false;
            }

            if (!value.Contains(":"))
            {
                error = "Incorrect Pattern! See ToolTip!";
                return false;
            }

            var arr = value.Split(':');

            if (arr.Length != 2)
            {
                error = "Incorrect Pattern! See ToolTip!";
                return false;
            }

            if (arr[1].Length == 0)
            {
                error = "Incorrect Pattern! See ToolTip!";
                return false;
            }

            return true;    

        }
    }
}
