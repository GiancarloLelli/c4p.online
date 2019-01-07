namespace cfp.online.Shared.Validation
{
    public class AreaValidator
    {
        public static bool Validate(string area)
        {
            var result = false;

            switch (area)
            {
                case "NA":
                case "SA":
                case "EU":
                case "AF":
                case "AUS":
                    result = true;
                    break;
            }

            return result;
        }
    }
}
