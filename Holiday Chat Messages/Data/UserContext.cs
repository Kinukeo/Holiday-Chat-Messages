namespace Holiday_Chat_Messages.Data
{
    public class UserContext
    {
        public string? UserName { get; set; }
        public string? Category { get; private set; }
        public double MaxPrice { get; private set; }

        public QuestionSteps CurrentQuestionStep { get; private set; } = QuestionSteps.Category;

        public bool ProcessCategorySelection(string categoryResponseMessage)
        {
            if (categoryResponseMessage.Contains("lazy", StringComparison.OrdinalIgnoreCase))
            {
                Category = "lazy";
                return true;
            }
            else if (categoryResponseMessage.Contains("active", StringComparison.OrdinalIgnoreCase))
            {
                Category = "active";
                return true;
            }

            return false;
        }

        public bool ProcessMaxPriceSelection(string maxPriceResponseMessage)
        {
            if (maxPriceResponseMessage.StartsWith('£'))
                maxPriceResponseMessage = maxPriceResponseMessage.Substring(1);

            bool validParse = double.TryParse(maxPriceResponseMessage, out double maxPrice);
            if (!validParse || maxPrice <= 0)
            {
                return false;
            }

            MaxPrice = maxPrice;
            return true;
        }

        public void MoveToNextQuestionStep()
        {
            switch (CurrentQuestionStep)
            {
                case QuestionSteps.Category:
                    CurrentQuestionStep = QuestionSteps.MaxPrice;
                    break;
                case QuestionSteps.MaxPrice:
                    CurrentQuestionStep = QuestionSteps.Result;
                    break;
            }
        }
    }
}
